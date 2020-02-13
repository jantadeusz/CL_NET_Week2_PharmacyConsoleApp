using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Tables
{
    public class Medicine : PharmacyObject
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public bool WithPrescription { get; set; }
        public Medicine(int id, string name, string manufacturer, decimal price, bool withPrescription)
        {
            MedicineId = id;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            WithPrescription = withPrescription;
        }
        public Medicine(string name, string manufacturer, decimal price, bool withPrescription)
        {
            MedicineId = 0;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            WithPrescription = withPrescription;
        }
        public override string ToString()
        {
            int s = 15;
            return $"{MedicineId.ToString().PadLeft(s, ' ')} | " +
                   $"{Name.PadLeft(s, ' ')} | " +
                   $"{Manufacturer.PadLeft(s, ' ')} | " +
                   $"{Price.ToString().PadLeft(s, ' ')} | " +
                   $"{WithPrescription.ToString().PadLeft(s, ' ')}.";
        }
    }
}
