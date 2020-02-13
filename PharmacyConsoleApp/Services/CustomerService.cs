using System;
using System.Collections.Generic;
using System.Text;
using Repo.Tables;
using Repo.Repositories;

namespace Services
{
    public class CustomerService
    {
        CustomerRepository customerRepository = new CustomerRepository();
        public List<Customer> GetCustomers()
        {
            List<Customer> list = customerRepository.LoadAllFromDB();
            Console.WriteLine("Klienci w bazie danych: ");
            foreach (Customer c in list)
            {
                Console.WriteLine("\t" + c.ToString());
            }
            Console.WriteLine("Koniec listy klientów.");
            return list;
        }
        public int GetCustomerIdFromUser(List<Customer> customers)
        {
            while (true)
            {
                bool idOnList = false;
                Console.WriteLine("Podaj id klienta: ");
                try
                {
                    int selectedCustomerId = Int32.Parse(Console.ReadLine());
                    foreach (Customer c in customers)
                    {
                        if (c.CustomerId == selectedCustomerId) { idOnList = true; }
                    }
                    if (idOnList == true) { return selectedCustomerId; }
                    else { Console.WriteLine("Numeru nie ma na liście klientów. Spróbuj ponownie."); }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}
