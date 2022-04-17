using System;

namespace trpo_rgz
{
    class Program
    {
        static void Main(string[] args)
        {
            //var poly1 = new TPoly();
            ////3x^2+2x^4+1x^0
            //poly1.Add(2, new TMember(3, 2));
            //poly1.Add(4, new TMember(2, 4));
            //poly1.Add(0, new TMember(1, 0));
            ////-7x^2+0x^4+0x^0-1x^1-5x^3
            //var poly2 = new TPoly();
            //poly2.Add(2, new TMember(7, 2));
            //poly2.Add(4, new TMember(5, 4));
            //poly2.Add(0, new TMember(10, 0));
            //poly2.Add(1, new TMember(5, 1));
            //poly2.Add(3, new TMember(5, 3));
            //var res = poly1 - poly2;
            //Console.WriteLine(res.ToString());

            var poly1 = new TPoly();
            //3x^2+2x^4+1x^0
            poly1.Add(2, new TMember(3, 2));
            poly1.Add(4, new TMember(2, 4));
            poly1.Add(0, new TMember(1, 0));
            //3x^2+2x^4+1x^0
            var poly2 = new TPoly();
            poly2.Add(2, new TMember(3, 2));
            poly2.Add(4, new TMember(2, 4));
            poly2.Add(0, new TMember(1, 0));
            var res = poly1 - poly2;
            Console.WriteLine(res.ToString());

            return;
        }
    }
}
