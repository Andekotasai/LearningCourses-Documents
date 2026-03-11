using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodePractices
{
    public interface ICustomer
    {
        void Details();
        void Discount();
    }

    public abstract class CustomerBase : ICustomer
    {
        public abstract int id { get; set; }
        public void Details()
        {
            Console.WriteLine("details from customer base");
        }

        public abstract void Discount();
    }
    class MainClasses : CustomerBase
    {
        public override int id { get; set; } = 1;
        public override void Discount()
        {
            Console.WriteLine("Discount from main class");
        }
    }


}
