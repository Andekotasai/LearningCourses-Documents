using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    class HackerRank1
    {

        #region Question1
        // Find the Smallest Missing Positive Integer
        //Given an unsorted array of integers, find the smallest positive integer not present in the array in O(n) time and O(1) extra space.

        // Example

        //Input

        //orderNumbers = [3, 4, -1, 1]
        //Output

        //2
        //Explanation

        //We want the smallest positive missing integer.

        //Start with[3, 4, -1, 1]
        //- i= 0: value 3 ⇒ swap with index 2 ⇒ [-1, 4, 3, 1]
        //- i= 0: value -1 is out of range ⇒ move on
        //- i= 1: value 4 ⇒ swap with index 3 ⇒ [-1, 1, 3, 4]
        //- i= 1: value 1 ⇒ swap with index 0 ⇒ [1, -1, 3, 4]
        //- now 1 at index 0, 3 at 2, 4 at 3; -1 remains at index 1

        //Scan from index 0:
        //index 0 has 1 (correct), index 1 has -1 (not 2) ⇒ missing positive is 2
        #endregion

        public static int findSmallestMissingPositive(List<int> orderNumbers)
        {
            // List<int> numbers=orderNumbers.OrderBy(x=>x).Select(x=>x>0).ToList();
            if(orderNumbers.Count == 0)
            {
                return 0;
            }
            else
            {
                List<int> numbers = orderNumbers.OrderBy(x => x).Where(x => x > 0).ToList();
                int n = numbers.Max();
                if(n== numbers.Count)
                {
                    return numbers.Count + 1;
                }
                int total = ((n * (n + 1)) / 2);
                int sum = numbers.Sum(x => x);
                return total - sum;
            }
               
        }

    }
}
