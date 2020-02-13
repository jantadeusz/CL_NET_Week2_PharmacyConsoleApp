using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Tables
{
    public class Order : PharmacyObject
    {
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public int PrescriptionId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int CustomerId { get; set; }
        public Order(int orderId, int medicineId, int prescriptionId, DateTime date, int amount, int customerId)
        {
            OrderId = orderId;
            MedicineId = medicineId;
            PrescriptionId = prescriptionId;
            Date = date;
            Amount = amount;
            CustomerId = customerId;
        }
        public Order(int medicineId, int prescriptionId, DateTime date, int amount, int customerId)
        {
            MedicineId = medicineId;
            PrescriptionId = prescriptionId;
            Date = date;
            Amount = amount;
            CustomerId = customerId;
        }
        public Order(int medicineId, DateTime date, int amount, int customerId)
        {
            MedicineId = medicineId;
            Date = date;
            Amount = amount;
            CustomerId = customerId;
        }
        public override string ToString()
        {
            int s = 20;
            return $"{OrderId.ToString().PadLeft(s, ' ')} | " +
                   $"{MedicineId.ToString().PadLeft(s, ' ')} | " +
                   $"{PrescriptionId.ToString().PadLeft(s, ' ')} | " +
                   $"{Date.ToString().PadLeft(s, ' ')} | " +
                   $"{Amount.ToString().PadLeft(s, ' ')} | " +
                   $"{CustomerId.ToString().PadLeft(s, ' ')}";
        }
    }
}
