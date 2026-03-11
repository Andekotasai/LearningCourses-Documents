using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    class StringBuilderConcept(string value)
    {
        public string value = value;

        //public StringBuilderConcept()
        //{
            
        //}

        public void PrintStringBuilder()
        {
            Console.WriteLine(value);
            Console.WriteLine("New change we hae in c#12");
            StringBuilder str = new StringBuilder();
            str.Append("Hello");
            Console.WriteLine(str.ToString());
            str.AppendLine(" New World");
            Console.WriteLine(str.ToString());
            str.Replace("World", "Universe", 2, 2);

            Console.WriteLine(str.ToString());
            str.AppendJoin(" ", "Welcome", "to", "CSharp");
            Console.WriteLine(str.ToString());

            str.Append(" .NET is awesome.",2,3);
            Console.WriteLine(str.ToString());

            
        }
    }
}
