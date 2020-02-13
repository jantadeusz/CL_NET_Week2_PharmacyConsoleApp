using System;
using System.Collections.Generic;
using System.Text;
using Repo.Tables;
using Repo.Repositories;
namespace Services
{
    public class MedicineService
    {
        MedicineRepository medicineRepository = new MedicineRepository();
        PrescriptionRepository prescriptionRepository = new PrescriptionRepository();

        public List<Medicine> GetMedicines(bool withPrescription)
        {
            Console.WriteLine("Dostępne leki:");
            var allMedicines = medicineRepository.LoadAllFromDB();
            var result = new List<Medicine>();
            foreach (Medicine medicine in allMedicines)
            {
                if (medicine.WithPrescription == withPrescription)
                {
                    result.Add(medicine);
                    Console.WriteLine("\t" + medicine.ToString());
                }
            }
            Console.WriteLine("Koniec listy leków  ============================================");
            return result;
        }
        public void AddMedicine()
        {
            Console.WriteLine("Podaj nazwę leku: ");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj producenta leku:");
            string manufacturer = Console.ReadLine();
            Console.WriteLine("Podaj cenę leku:");
            decimal price = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilość leku:");
            int amount = Int32.Parse(Console.ReadLine()); // todo: catch
            Console.WriteLine("Podaj czy lek jest na receptę. Wciśnij klawisz: [t]-tak [n]-nie:"); // todo: catch
            bool withPrescription = false;
            char key = char.Parse(Console.ReadLine());
            if (key == 't') { withPrescription = true; }
            Medicine newMedicine = new Medicine(name, manufacturer, price, withPrescription);
            var medicineRepository = new MedicineRepository();
            medicineRepository.AddToDB(newMedicine);
        }
        public int GetMedicineIdFromUser(List<Medicine> medicines)
        {
            while (true)
            {
                bool idOnList = false;
                Console.WriteLine("Podaj numer leku który chcesz wybrać:");
                try
                {
                    int selectedMedicineId = Int32.Parse(Console.ReadLine());
                    foreach (Medicine m in medicines)
                    {
                        if (m.MedicineId == selectedMedicineId) { idOnList = true; }
                    }
                    if (idOnList == true) { return selectedMedicineId; }
                    else { Console.WriteLine("Numeru nie ma na liście leków. Spróbuj ponownie."); }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        public int GetMedicineAmountFromUser()
        {
            while (true)
            {
                Console.WriteLine("Podaj ilość leku który chcesz wybrać:");
                try
                {
                    int selectedMedicineAmount = Int32.Parse(Console.ReadLine());
                    // todo: spr czy ilosc jest dostepna -> jak nie ma to throw exception
                    return selectedMedicineAmount;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Spróbuj jeszcze raz.");
                }
            }
        }

        public void UpdateMedicine(int id)
        {
            Console.WriteLine("Podaj nową cenę leku: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());
            Medicine oldMedicine = medicineRepository.LoadFromDB(id);
            Medicine newMedicine = 
                new Medicine(oldMedicine.Name, oldMedicine.Manufacturer, newPrice, oldMedicine.WithPrescription);

            //medToUpdate.Price = newPrice;
            //Console.WriteLine(updatedMedicine.ToString());
            medicineRepository.UpdateObjectInDB(newMedicine);
            // todo: do rozbudowy edycja lekow i ich ilosci
        }
        public void showMedicineWithPrescriptions(int medicineId)
        {
            Medicine selectedMedicine = medicineRepository.LoadFromDB(medicineId);
            Console.WriteLine($"Dane leku o id: {medicineId}:");
            Console.WriteLine(selectedMedicine.ToString());
            List<Prescription> prescriptions = prescriptionRepository.LoadPrescriptionsFromMedicineId(medicineId);
            Console.WriteLine($"Recepty wystawione do leku o id: {medicineId} i nazwie {selectedMedicine.Name}:");
            foreach (Prescription p in prescriptions)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
