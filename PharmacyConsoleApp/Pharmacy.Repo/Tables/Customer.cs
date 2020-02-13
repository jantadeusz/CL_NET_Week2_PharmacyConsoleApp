using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Tables
{
    public class Customer : PharmacyObject
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public Customer(int customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }
        public Customer(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            int s = 15;
            return $"{CustomerId.ToString().PadLeft(s, ' ')} | " +
                   $"{Name.PadLeft(s, ' ')} | ";
        }
    }
}
