using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    class EnumConcept
    {
        enum DateOfWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        public void PrintEnum()
        {
            Console.WriteLine(EnumConcept.DateOfWeek.Monday);
            Console.WriteLine(typeof(EnumConcept.DateOfWeek));
            Console.WriteLine((int)DateOfWeek.Monday);

            Console.WriteLine(Enum.GetValues(typeof(DateOfWeek)));
            Console.WriteLine(Enum.GetNames(typeof(DateOfWeek)));
        }
    }
}
