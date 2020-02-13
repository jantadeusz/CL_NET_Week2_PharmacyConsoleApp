using System;
using Services;
using Repo.Tables;
using Repo.Repositories;
using System.Collections.Generic;

namespace ConsoleLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerService = new CustomerService();
            var medicineService = new MedicineService();
            var orderService = new OrderService();
            var prescriptionService = new PrescriptionService();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Witaj w programie do prowadzenia apteki.");
                Console.WriteLine("Co chcesz zrobić. Podaj komendę i zatwierdź enter: ");
                Console.WriteLine("1 - Dodaj zamówienie");
                Console.WriteLine("2 - Zarządzaj apteką");
                Console.WriteLine("exit - wyjście z programu");

                string mainCommand = Console.ReadLine();
                if (mainCommand == "1")
                {
                    while (true)
                    {
                        //bool pass = false;
                        Console.Clear();
                        Console.WriteLine("Wyswietlanie ekranu logowania. ");
                        var customers = customerService.GetCustomers();
                        Console.WriteLine("Podaj numer klienta: ");
                        int selectedCustomerId = customerService.GetCustomerIdFromUser(customers);
                        Console.WriteLine("Dodawanie nowego zamówienia.");
                        Console.WriteLine("Wybierz: 1 - bez recepty, 2 - na receptę.");
                        string commandNewOrder = Console.ReadLine();
                        if (commandNewOrder == "1")
                        {
                            Console.WriteLine("Dodawanie zamówienia bez recepty.");
                            bool withPrescription = false;
                            List<Medicine> medicines = medicineService.GetMedicines(withPrescription);
                            orderService.AddOrderWithoutPrescrToDB(medicineService, medicines, selectedCustomerId);
                        }
                        else if (commandNewOrder == "2")
                        {
                            Console.WriteLine("Dodawanie zamówienia na receptę.");
                            int prescriptionId = prescriptionService.AddPrescriptionToDB();
                            string nextMedicine = "t";
                            while (nextMedicine == "t")
                            {
                                List<Medicine> medicinesWithPresc = medicineService.GetMedicines(true);
                                Console.WriteLine("Dodawanie zamówienia na receptę.");
                                orderService.AddOrderWithPrescrToDB(medicineService, medicinesWithPresc, selectedCustomerId, prescriptionId);
                                Console.WriteLine("Czy chcesz dodać kolejną pozycję na recepcie [t/n]?");
                                nextMedicine = Console.ReadLine();
                            }
                            Console.WriteLine("Zakończono dodawanie recepty i przypisanych leków.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Niewłaściwy numer czynności.");
                        }
                    }
                }
                else if (mainCommand == "2")
                {
                    while (true)
                    {

                        Console.WriteLine("Podaj czynność:");
                        Console.WriteLine("1 - Edytuj stan leków bez recepty.");
                        Console.WriteLine("2 - Edytuj stan leków na receptę.");
                        Console.WriteLine("3 - Pokaż wszystkie leki na danej recepcie.");
                        Console.WriteLine("4 - Pokaż wszystkie recepty dla danego leku.");
                        Console.WriteLine("5 - Usun lek");
                        Console.WriteLine("6 - Dodaj lek");
                        Console.WriteLine("up - wyjdź z menu");
                        string manageCommand = Console.ReadLine();
                        if (manageCommand == "1" || manageCommand == "2")
                        {
                            if (manageCommand == "1")
                            {
                                medicineService.GetMedicines(false);
                            }
                            else if (manageCommand == "2")
                            {
                                medicineService.GetMedicines(true);
                            }
                            Console.WriteLine("Podaj numer leku: ");
                            int medId = Int32.Parse(Console.ReadLine());
                            medicineService.UpdateMedicine(medId);
                            // nie aktualizuje sie cena leku prawidlowo
                        }
                        else if (manageCommand == "3")
                        {
                            prescriptionService.GetPrescriptions();
                            Console.WriteLine("Podaj numer recepty: ");
                            int prescriptionId = Int32.Parse(Console.ReadLine());
                            prescriptionService.ShowPrescriptionWithMedicines(prescriptionId);
                        }
                        else if (manageCommand == "4")
                        {
                            medicineService.GetMedicines(true);
                            Console.WriteLine("Podaj numer leku: ");
                            int medId = Int32.Parse(Console.ReadLine());
                            medicineService.showMedicineWithPrescriptions(medId);
                        }
                        else if (manageCommand == "5") { Console.WriteLine("todo: Delete"); }
                        else if (manageCommand == "6") { Console.WriteLine("todo: Add"); }
                        else if (manageCommand == "up")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Błędna komenda. Spróbuj ponownie.");
                        }
                    }
                }
                else if (mainCommand == "exit")
                {
                    Console.WriteLine("Do widzenia.");
                    break;
                }
                else
                {
                    Console.WriteLine("Zła komenda. Spróbuj ponownie.");
                }
            }
        }
    }
}



/*
 alter table Shelf 
add constraint FK_MedicineId_MedicineId
foreign key (MedicineId)
references Medicine(Id)

insert into Shelf(MedicineId, Amount, Location) 
values(3,30,'C5')
insert into Shelf(MedicineId, Amount, Location) 
values(1,10,'A1')
insert into Shelf(MedicineId, Amount, Location) 
values(2,5,'A2')

select * from Medicine join Shelf on Medicine.Id = Shelf.MedicineId

insert into Customer(Name, Pesel) values('Anna', '84100510197')

insert into Prescriptions(PrescriptionSignature, CustomerId) 
values('recepta3', 1)

-- z recepta
insert into Orders ( MedicineId, PrescriptionId, Date, Amount)
values(2, 1, '2019-12-30',2)
insert into Orders ( MedicineId, PrescriptionId, Date, Amount)
values(6, 2, '2019-12-31',1)
-- bez recepty
insert into Orders ( MedicineId, Date, Amount)
values(3,  '2019-12-30',8)

-- pokazanie wszystkich zamowien wraz z lekami i receptami(lub bez)
select * from Orders
full join Prescriptions on Orders.PrescriptionId = Prescriptions.PrescriptionId
join Medicines on Orders.MedicineId = Medicines.MedicineId
join Customers on Orders.CustomerId = Customers.CustomerId

-- pokazanie wszystkich lekow z danej recepty: wyszukiwanie po nazwie recepty
select * from Orders
join Prescriptions on Orders.PrescriptionId = Prescriptions.PrescriptionId
join Medicines on Orders.MedicineId = Medicines.MedicineId
join Customers on Orders.CustomerId = Customers.CustomerId
--where Prescriptions.PrescriptionSignature = 'recepta1'
where Prescriptions.PrescriptionSignature = 'recepta2'
--where Medicines.MedicineId = 1

-- pokazanie wszystkich recept na ktorych widnieje dany lek: wyszukiwanie po numerze leku
select * from Orders
join Prescriptions on Orders.PrescriptionId = Prescriptions.PrescriptionId
join Medicines on Orders.MedicineId = Medicines.MedicineId
join Customers on Orders.CustomerId = Customers.CustomerId
where Medicines.MedicineId = 7

alter table Prescriptions 
drop column CustomerId

update Orders set CustomerId = 2 where OrderId >3

update Orders 
set Orders.MedicineId = 6
where PrescriptionId <2 

Update Prescriptions set PESEL = '99945678901' where PrescriptionId >2

     
     */


