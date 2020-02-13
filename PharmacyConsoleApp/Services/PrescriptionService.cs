using System;
using System.Collections.Generic;
using System.Text;
using Repo.Tables;
using Repo.Repositories;

namespace Services
{
    public class PrescriptionService
    {
        PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
        MedicineRepository medicineRepository = new MedicineRepository();
        public List<Prescription> GetPrescriptions()
        {
            Console.WriteLine("Dostępne recepty: ");
            var result = prescriptionRepository.LoadAllFromDB();
            foreach (Prescription prescription in result)
            {
                Console.WriteLine("\t" + prescription.ToString());
            }
            Console.WriteLine("Koniec listy recept  ============================================");
            return result;
        }
        public string GetPESEL()
        {
            while (true)
            {
                Console.WriteLine("Podaj 11 cyfrowy numer PESEL: ");
                string candidate = Console.ReadLine();
                if (candidate.Length == 11)
                {
                    return candidate; // poprawna implementacja sprawdzenia pesel innym razem
                }
            }
        }
        public int AddPrescriptionToDB()
        {
            Console.WriteLine("Podaj oznaczenie recepty: ");
            string signature = Console.ReadLine();
            string pesel = GetPESEL();
            Prescription prescription = new Prescription(signature, pesel);
            int prescriptionId = prescriptionRepository.AddToDB(prescription);
            return prescriptionId;
        }
        public void ShowPrescriptionWithMedicines(int prescriptionId)
        {
            Prescription selectedPrescription = prescriptionRepository.LoadFromDB(prescriptionId);
            Console.WriteLine($"Dane recepty o id: {prescriptionId}:");
            Console.WriteLine(selectedPrescription.ToString());
            List<Medicine> medicines = medicineRepository.LoadMedicinesFromPrescription(prescriptionId);
            Console.WriteLine($"Leki z recepty o id: {prescriptionId} i sygnaturze {selectedPrescription.PrescriptionSignature}:");
            foreach (Medicine m in medicines)
            {
                Console.WriteLine(m.ToString());
            }
        }
    }
}
