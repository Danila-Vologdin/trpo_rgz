using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpo_rgz
{
    public class TMember
    {
        //коэффицент одночлена
        private int _coef;
        //степень одночлена
        private int _power;

        public TMember()
        {
            _coef = 0;
            _power = 0;
        }

        public TMember(int coef, int power)
        {
            _coef = coef;
            _power = power;
        }

        public void SetCoef(int newCoef) => _coef = newCoef;
        public int GetCoef() => _coef;

        public void SetPower(int newPower) => _power = newPower;
        public int GetPower() => _power;

        // сравнить одночлены
        public bool IsEqual(TMember val)
        {
            if (GetCoef() == val.GetCoef() && GetPower() == val.GetPower())
            {
                return true;
            }
            return false;
        }

        //Создаёт одночлен, являющийся производной одночлена и возвращает его.
        public TMember Diff()
        {
            var result = new TMember(_coef * _power, _power - 1);
            return result;
        }

        //Вычисляет значение одночлена в точке x и возвращает его.
        public double Calculate(double x) => _coef * Math.Pow(x, _power);

        //Формирует строковое представление одночлена.
        public override string ToString()
        {
            if (_power == 0)
                return _coef.ToString();
            
            return $"{_coef}x^{_power}";
        }

        //сложение одночленов 
        public static TMember operator +(TMember q1, TMember q2)
            => new TMember(q1.GetCoef() + q2.GetCoef(), q1.GetPower());

        //вычитание одночленов
        public static TMember operator -(TMember q1, TMember q2)
            => new TMember(q1.GetCoef() - q2.GetCoef(), q1.GetPower());

        //умножение одночленов
        public static TMember operator *(TMember q1, TMember q2)
            => new TMember(q1.GetCoef() * q2.GetCoef(),
                q1.GetPower() + q2.GetPower());

    }
}
