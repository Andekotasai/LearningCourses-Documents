using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    class Practices
    {
        public void Name()
        {
            Console.WriteLine("Practices Class ==> Name Mehod");
        }
        public void Age()
        {
            Console.WriteLine("Practices Class ==> Age Mehod");
        }
        public virtual void Education()
        {
            Console.WriteLine("Edication under Practice");
        }
    }
    class Data:Practices
    {
        [ObsoleteAttribute("Data we wil remove")]
        public void Gender()
        {
            Console.WriteLine( "Data Class ==> Gender");
        }
        public new void Name()
        {
            Console.WriteLine("Data Class ==> Name Method");
        }
        public override void Education()
        {
            Console.WriteLine("Education under Data");
        }
    }
     static class Extensionstringmethod
    {
        public static void upper(this string s)
        {
            Console.WriteLine(s);
            
        }

        public static void NewUpper(this String s)
        {
            
            Console.WriteLine(s.ToUpper());
        }
    }

    #region RegularExpressionPracticeClass

    //Platform : Regex101
    public class  RegularexpressionPractice
    {
        public void FindthePattern()
        {
            List<int> intList = new List<int> { 1, 2, 3, 4, 5 };
            var strlist = string.Join("", intList);
            Console.WriteLine($"list data {strlist}");

            string[] strs = new string[] { "Ande", "Kota", "Sai" };

            var kt = string.Join(strlist, strs);
            Console.WriteLine($"String data {kt}");
            string pattern = @"[a-zA-Z]+\d+";

            MatchCollection matches = Regex.Matches(kt, pattern);  //Global search all matched data it will return
            Match match1 = Regex.Match(kt, pattern);                //it will search if any thing find then it will return first match only
            Console.WriteLine("First Match: " + match1.Value);

            foreach (Match match in matches)
            {
                Console.WriteLine("Found number: " + match.Value);
            }
        }
    }
    #endregion



}
