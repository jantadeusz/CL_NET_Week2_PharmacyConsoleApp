using System;
using System.Collections.Generic;
using System.Text;
using Repo.Tables;
using Repo.Repositories;

namespace Services
{
    public class OrderService
    {
        OrderRepository orderRepository = new OrderRepository();
        public List<Order> GetOrders()
        {
            var orders = new List<Order>();


            // implement


            return orders;
        }
        public void AddOrderWithPrescrToDB(MedicineService medicineService, List<Medicine> medicines, int customerId, int prescriptionId)
        {
            int medicineId = medicineService.GetMedicineIdFromUser(medicines);
            int medicineAmount = medicineService.GetMedicineAmountFromUser();
            Order newOrder = new Order(medicineId, prescriptionId, DateTime.Now, medicineAmount, customerId);
            int result = orderRepository.AddOrderWithPrescrToDB(newOrder);
            if (result > 0) { Console.WriteLine("Dodano zamówienie do bazy danych."); }
            else { Console.WriteLine("Podczas dodawania zamówienia coś poszło źle."); }
            Console.WriteLine("Naciśnij aby przejść dalej.");
            Console.ReadLine();
        }
        public void AddOrderWithoutPrescrToDB(MedicineService medicineService, List<Medicine> medicines, int customerId)
        {
            int medicineId = medicineService.GetMedicineIdFromUser(medicines);
            int medicineAmount = medicineService.GetMedicineAmountFromUser();
            Order newOrder = new Order(medicineId, DateTime.Now, medicineAmount, customerId);
            int result = orderRepository.AddOrderWithoutPrescrToDB(newOrder);
            if (result > 0) { Console.WriteLine("Dodano zamówienie do bazy danych."); }
            else { Console.WriteLine("Podczas dodawania zamówienia coś poszło źle."); }
            Console.WriteLine("Naciśnij aby przejść dalej.");
            Console.ReadLine();
        }
    }
}
