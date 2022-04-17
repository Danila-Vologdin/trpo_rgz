using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpo_rgz
{
    //в словаре ключ - степень одночлена
    public class TPoly : Dictionary<int, TMember>
    {
        public TPoly()
        {

        }

        //можно также сделать :base() чтобы вызвать
        //конструктор родительского класса
        public TPoly(int coef, int power)
        {
            Add(power, new TMember(coef, power));
        }


        //наибольшая степень в словаре
        public int GetMaxPower()
        {
            int maxPower = -1;
            foreach (KeyValuePair<int, TMember> dictValue in this)
            {
                if (dictValue.Key > maxPower) maxPower = dictValue.Key;
            }

            return maxPower;
        }

        //Отыскивает коэффициент(c) при члене полинома со степенью n(c* x^n)
        public int GetCoefAtPower(int power)
        {
            if (ContainsKey(power))
                return this[power].GetCoef();
            return 0;
        }

        //Clear() метод реализован в родительском классе

        //сложение полиномов
        public static TPoly operator +(TPoly p1, TPoly p2)
        {
            var result = new TPoly();
            foreach (KeyValuePair<int, TMember> p1Elem in p1)
            {
                //суммируем те одночлены, степени которых равны
                //если степени присутствуют в обоих полиномах
                if (p2.ContainsKey(p1Elem.Key))
                {
                    TMember newMember = p1[p1Elem.Key] + p2[p1Elem.Key];
                    if (newMember.GetCoef() != 0)
                        result.Add(p1Elem.Key, newMember);
                }
                //добавляем одночлены, степеней которых нет в p1,
                //в результирующий полином
                else if (p1Elem.Value.GetCoef() != 0)
                {
                    result.Add(p1Elem.Key, p1Elem.Value);
                }
            }

            //добавляем значения которые есть в p2 и нет в p1
            foreach (KeyValuePair<int, TMember> p2Elem in p2)
            {
                if (!p1.ContainsKey(p2Elem.Key) && p2Elem.Value.GetCoef() != 0)
                    result.Add(p2Elem.Key, p2Elem.Value);
            }

            return result;
        }

        //вычитание полиномов
        public static TPoly operator -(TPoly p1, TPoly p2)
        {
            var result = new TPoly();
            foreach (KeyValuePair<int, TMember> p1Elem in p1)
            {
                //вычитаем те одночлены, степени которых равны
                //если степени присутствуют в обоих полиномах
                if (p2.ContainsKey(p1Elem.Key))
                {
                    TMember newMember = p1[p1Elem.Key] - p2[p1Elem.Key];
                    if (newMember.GetCoef() != 0)
                        result.Add(p1Elem.Key, newMember);
                }
                //добавляем одночлены, степеней которых нет в p1,
                //в результирующий полином
                else if (p1Elem.Value.GetCoef() != 0)
                {
                    result.Add(p1Elem.Key, p1Elem.Value);
                }
            }

            //добавляем значения которые есть в p2 и нет в p1 (с минусом)
            foreach (KeyValuePair<int, TMember> p2Elem in p2)
            {
                if (!p1.ContainsKey(p2Elem.Key) && p2Elem.Value.GetCoef() != 0)
                    result.Add(p2Elem.Key,
                        new TMember(-p2Elem.Value.GetCoef(), p2Elem.Value.GetPower()));
            }

            return result;
        }

        //умножение полиномов
        public static TPoly operator *(TPoly p1, TPoly p2)
        {
            var result = new TPoly();
            foreach (KeyValuePair<int, TMember> p1Elem in p1)
            {
                foreach (KeyValuePair<int, TMember> p2Elem in p2)
                {
                    //умножаем одночлены
                    var newValue = p1Elem.Value * p2Elem.Value;
                    if (newValue.GetCoef() != 0)
                    {
                        int currentKey = newValue.GetPower();
                        //если в результате уже есть одночлен с такой степенью
                        if (result.ContainsKey(currentKey))
                        {
                            //прибавляем к уже существующему одночлену
                            newValue += result[currentKey];
                            //удаляем прошлое значение в словаре
                            result.Remove(currentKey);
                        }

                        //обновляем значение в словаре
                        result.Add(currentKey, newValue);
                    }
                }
            }

            return result;
        }

        // инверсия знаков полинома
        public TPoly Reverse()
        {
            var result = new TPoly();
            foreach (KeyValuePair<int, TMember> pElem in this)
            {
                //добавляем элемент с обратным знаком
                if (pElem.Value.GetCoef() != 0)
                    result.Add(pElem.Key,
                        new TMember(pElem.Value.GetCoef() * (-1), pElem.Key));
            }

            return result;
        }

        //проверка равенства полиномов
        public bool IsEqual(TPoly p2)
        {
            if (this.Count == p2.Count)
            {
                foreach (KeyValuePair<int, TMember> pElem in this)
                {
                    //если не содержит полином такого ключа
                    //или не равны одночлены с данным ключом
                    if (!p2.ContainsKey(pElem.Key) ||
                        !pElem.Value.IsEqual(p2[pElem.Key]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //Производная полинома
        public TPoly Diff()
        {
            var result = new TPoly();
            foreach (KeyValuePair<int, TMember> pElem in this)
            {
                var newValue = pElem.Value.Diff();
                if (newValue.GetCoef() != 0)
                    result.Add(newValue.GetPower(), newValue);
            }

            return result;
        }

        //значение полинома в точке х
        public double Calculate(double x)
        {
            double result = 0;
            // проходим по элементам полинома
            foreach (KeyValuePair<int, TMember> pElem in this)
            {
                result += pElem.Value.Calculate(x);
            }

            return result;
        }

        //получить значение одночлена через полином
        //при помощи индекса
        public TMember GetValueByIndex(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new Exception("Index error: out of boundary.");
            //создаем массив для данных из словаря
            TMember[] valuesArr = new TMember[this.Count];
            //копируем values из словаря в массив,
            //вставка в массив будет происходить, начиная с нулевого индекса
            this.Values.CopyTo(valuesArr, 0);
            return valuesArr[index];
        }

        //записать полином в строку
        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<int, TMember> pElem in this)
            {
                if (pElem.Value.GetCoef() < 0 && result.Length > 0)
                {
                    //если одночлен отрицательный и не идет первым в строке
                    //убираем плюсик, чтобы был минус в выражении
                    result = result.TrimEnd('+');
                }
                result += pElem.Value.ToString() + "+";
            }
            if (result.Length == 0) return "0";
            //удаляем последний лишний плюсик
            return result.TrimEnd('+');
        }
    }
}
