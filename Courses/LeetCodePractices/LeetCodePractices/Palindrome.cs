using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    class Palindrome
    {
        public void PrintPalindrome()
        {
            Console.WriteLine("Enter string");
            int L = Int32.Parse(Console.ReadLine());
            //Ispalindrome(L);
            Console.WriteLine(Ispalindrome(L));
        }
        //public bool isPalindrome(string K)
        //{
        //    int n = 0;
        //    int p = K.Length-1;
        //    while (n<=p)
        //    {
        //        if (K[n] != K[p - n])
        //            return false;
        //        n++;
                 
        //    }
        //    return true;
        //}

        public bool Ispalindrome(int x)
        {
            string K = x.ToString();

            int n = 0;
            int p = K.Length - 1;
            while (n <= p)
            {
                if (K[n] != K[p - n])
                    return false;
                n++;

            }
            return true;
        }
    }
}
