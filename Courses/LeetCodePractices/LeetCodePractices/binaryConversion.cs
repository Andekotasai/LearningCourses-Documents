using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    class binaryConversion
    {
        public binaryConversion()
        {
            
        }

        public void ConvertToBinary(int num)
        {
            string data = Convert.ToString(num, 2);
            Console.WriteLine(data);

            List<int> li=new List<int>();
            while (num != 0)
            {
                li.Add(num % 2);
                num = num / 2;
            }
            //li.ForEach(x =>
            //{
            //    Console.WriteLine(x);
            //});

            Console.WriteLine("Data after manual binary conversion");
            Console.WriteLine(String.Join("", li));

            
            
        }
    }
}
