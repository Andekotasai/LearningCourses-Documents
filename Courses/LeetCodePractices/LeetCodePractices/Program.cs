using LeetCodePractices;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace LeetCodePactices
{
    public static class Program
    {
        public static void Main(string[] args)
        {
           

            #region PalindromeCheck
            //Console.WriteLine("Data inside Leetcode");
            //var pal = new Palindrome();
            //pal.PrintPalindrome();
            #endregion

            #region DataClassInstance(ParentChildclass)
            //Practices p1 = new Practices();
            //p1.Education();

            //Practices p = new Data();
            //p.Name();
            //p.Age();
            //p.Education();



            //var d = new Data();
            //d.Name();
            //d.Gender();
            //d.Education();

            #endregion

            #region Array
            //String S = "Hello World";

            //Exception ex = new Exception();
            //ApplicationException ex2 = new ApplicationException();




            //List<int> li = new List<int>();

            //li.Add(1);
            ////li.Append(2);
            //Console.WriteLine(li[0]);

            //int[] arr = new int[2];
            //arr.Append(1);
            //arr.Append(2);
            //Console.WriteLine(arr[1]) ;
            #endregion

            #region Array/List to String Conversion
            //char[] arr2 = new char[] { 'a', 'b', 'c' };
            //var dt = string.Join("", arr2);
            //Console.WriteLine(dt);   //abc

            //int[] ints = new int[] { 1, 2, 3, 4, 5 };
            //Console.WriteLine(ints);  //type we will get
            //var strint = string.Join("", ints);
            //Console.WriteLine(strint); //12345


            //var si = new String(new char[] { 'a', 'b', 'c' });
            //Console.WriteLine(si); //abc

            #endregion

            #region regularExpressionPatternFindout


            //RegularexpressionPractice reg = new RegularexpressionPractice();
            //reg.FindthePattern();

            #endregion

            #region StringBuilderPractice
            //StringBuilder sb = new StringBuilder();

            //sb.Append("Hello");  //Append String
            //Console.WriteLine(sb);

            //sb.Append('w');       //Append Char
            //Console.WriteLine(sb); 

            //sb.Append(new char[] { 'a', 'b', 'c', }, 1, 2);  //Append Char list

            //Console.WriteLine(sb);

            //sb.Append("Kota sai", 5, 3);
            //Console.WriteLine(sb);

            //sb.Insert(1, "Ande");  //Insert String
            //Console.WriteLine(sb);

            //sb.Replace("sai", "Sai");  //Replace String
            //Console.WriteLine(sb);
            #endregion


            #region StringBuilderPractice1

            //StringBuilderConcept sbc = new StringBuilderConcept("kotasai");
            //sbc.PrintStringBuilder();
            #endregion

            //List<int> li=new List<int> { 1, 2, 3, 4, 5 };

            //Console.WriteLine(li.Capacity);

            //List<int> list1 = new List<int>();
            //list1.Add(10);
            //Console.WriteLine(list1);

            #region EnumConcept
            //EnumConcept enumcon = new EnumConcept();
            //enumcon.PrintEnum();

            #endregion

            #region Binary Conversion

            //binaryConversion bin = new binaryConversion();

            //bin.ConvertToBinary(int.Parse(Console.ReadLine()));

            #endregion

            #region HackerRank Question

            //List<int> orderNumbers = new List<int> { 3, 4, -1, 1 };
            //Console.WriteLine(HackerRank1.findSmallestMissingPositive([1]));

            #endregion

            #region Linq

            // Sample data
            //var list1 = new List<int> { 1, 2, 3, 4, 5, 5 };
            //var list2 = new List<int> { 4, 5, 6, 7, 8 };

            //var data = list1.Select(x => x * 2);
            //Console.WriteLine(string.Join(", ", data));

            //List<int> unionData = list1.Union(list2).ToList();
            //Console.WriteLine("Union: " + string.Join(", ", unionData));

            //var dist = unionData.Select(x => x).Distinct();
            //Console.WriteLine($"Distinct Data: {string.Join(",", dist)}");

            //var match = unionData.SingleOrDefault(x => x == 5); // Returns default value (0 for int) since 10 is not found
            //Console.WriteLine($"matched {match}");

            //var matched = unionData.FirstOrDefault(); // Returns default value (0 for int) since 10 is not found
            //Console.WriteLine($"matched {matched}");

            //var sum = list1.Aggregate(list1[0], (acc, val) => acc + val);
            //Console.WriteLine($"Sum value {sum}");


            //var people = new List<Person>
            //{
            //    new Person { Id = 1, Name = "Alice", Age = 25, City = "New York" },
            //    new Person { Id = 2, Name = "Bob", Age = 30, City = "London" },
            //    new Person { Id = 3, Name = "Charlie", Age = 25, City = "New York" },
            //    new Person { Id = 4, Name = "David", Age = 35, City = "Paris" }
            //};


            //int[] m = new int[] { 1, 2, 3, 4, 5,5 };
            //var data = m.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => x.Key).ToList();
            //foreach(var item in data)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine(string.Join("",data));

            #endregion

            #region ArrayMethods
            //int[] arr = new int[] { 5, 2, 8, 1, 3 };
            //int[] arr2 = new int[] { 10, 20, 30 };
            //Array.Sort(arr);
            //Console.WriteLine("Sorted Array: " + string.Join(", ", arr));

            //Array.ForEach(arr, (item) =>
            //{
            //    Console.WriteLine($"Element at index {item}");
            //});


            List<int> li = new List<int>() { 1, 2, 3, 4, 5 };
            List<string> li2 = new List<string>() { "kotasai", "Nagendra", "Leela" };

            List<int> templi = li.Slice(0, 4);
            Console.WriteLine("Data in the List");
            Console.WriteLine(string.Join(",", templi));
            Console.WriteLine(string.Join(",", li));

            //List<string> li3=li.Zip(li2, (a, b) => $"{a}=>{b}").ToList();
            //Console.WriteLine(string.Join(",",li3));


            //Add,Remove,AddRange,Insert,RemoveAt,IndexOf,LastIndexOf,first,find,filter,reduce,sum,avg,orderBy,sort,SequesnceEqual
            //,Intersect,union,Except,Contains,ElementAt,Slice

            //li.ForEach((item) =>
            //{
            //    Console.WriteLine($"Element at index  is {item}");
            //});

            //li.Reverse();
            //Console.WriteLine("After Reverse " + string.Join(",", li));
            //li.Remove(3);
            //Console.WriteLine("Remove value 3 " + string.Join(",", li));

            //li.RemoveAt(2);
            //Console.WriteLine("RemoveAt index 4 " + string.Join(",", li));

            //li.IndexOf(2);
            //Console.WriteLine("Index of 2 is " + li.IndexOf(2));

            //li.LastIndexOf(4);
            //Console.WriteLine("Last Index of 4 is " + li.LastIndexOf(4));

            //foreach (var Item in li)
            //{
            //    Console.WriteLine(Item);
            //}
            ////li.Slice(1, 3);
            ////Console.WriteLine(string.Join(",", li));

            //li.Append(97);
            //li.AddRange(new List<int> { 100, 200, 300 });
            //li.RemoveRange(1, 5);
            //li.Count();
            //List<int> li2 = new List<int>();
            //li.Intersect(li2);
            //li.Union(li2);
            //Console.WriteLine("data inside the l " + string.Join(",", li));


            #endregion

            #region TwoSum Method Calling
            //TwoSum([3, 2, 4], 6);
            #endregion

            #region ExceptSelfProd Method Calling
            //ExceptSelfProd([1, 2, 3, 4]);
            #endregion

            #region LengthOfLongestSubstring Method Calling
            //int n = LengthOfLongestSubstring("abcdefg");
            //Console.WriteLine(n);
            #endregion


            #region MedianofTwoSortedArrays

            //var median =FindMedianSortedArrays([1, 2], [3, 4]);
            //Console.WriteLine(median);

            #endregion

            #region Abstract & Interface Class Implementation
            //ICustomer cust = new MainClasses();   //with interface
            //CustomerBase customerBase = new MainClasses(); //with abstract class
            //cust.Details();
            //cust.Discount();
            //customerBase.Details();
            //customerBase.Discount();
            //var main = new MainClasses();
            //Console.WriteLine(customerBase.id);
            //Console.WriteLine(main.id);
            //Console.WriteLine($"{main.id}, {customerBase.id}");
            
            #endregion

        }
        #region TwoSum
        public static void TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int[] arr = new int[2];
            List<int> latest = nums.ToList();
            for (int i = 0; i < nums.Count(); i++)
            {
                int val = target - nums[i];
                if (dict.ContainsKey(val))
                {
                    arr[0] = dict[val];
                    arr[1] = i;
                }
                else
                {
                    //dict.Add(nums[i], i);
                    dict[nums[i]] = i;
                }

            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }
        #endregion

        #region ExceptSelfProd
        public static void ExceptSelfProd(List<int> li)
        {
            
            int n = li.Count();
            List<int> result = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int prod = 1;

                // Prefix: elements before i
                if (i > 0)
                {
                    var prefix = li.GetRange(0, i);
                    prefix.ForEach(x => prod *= x);
                }

                // Suffix: elements after i
                if (i < n - 1)
                {
                    var suffix = li.GetRange(i + 1, n - i - 1);
                    suffix.ForEach(x => prod *= x);
                }

                result.Add(prod);




            }
            Console.WriteLine(string.Join(",",result));

        }
        #endregion

        #region LengthOfLongestSubstring
        public static int LengthOfLongestSubstring(string s)
        { //aabbfc

            if (s.Count() > 0)
            {
                //  char[] ch_arr= s.ToCharArray();
                List<char> li = new List<char>();
                List<(string, int)> dict2 = new List<(string, int)>();
                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict.Add("name", 1);
                dict.Clear();
                int len = 0;
                int left = 0;
                int max = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    while (li.Contains(s[i]))
                    {

                        li.RemoveAt(left);
                        len--;
                    }

                    li.Add(s[i]);
                    len++;
                    max=Math.Max(max, len);


                }
                return max;
            }
            else
            {
                return 0;
            }
        }
        #endregion


        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            //int[] nums1 = [1, 2];
            //int[] nums2 = [3, 4];
            double median;
            int i;
            List<int> newList = nums1.Concat(nums2).ToList();
            newList.Sort();
            Console.WriteLine(string.Join(" ", newList));
            var dt = newList.Count();
            if (dt % 2 == 0)
            {
                 i = dt / 2;
                median = ((newList[i] + newList[i - 1]) / 2.0);
                return median;
            }
            
                 i = dt / 2;
                median = newList[i];
            return median;

            //Console.WriteLine(median);

        }

            //public class Person
            //{
            //    public int Id { get; set; }
            //    public string Name { get; set; }
            //    public int Age { get; set; }
            //    public string City { get; set; }
            //}

        }

}