using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Tables
{
    public class Prescription : PharmacyObject
    {
        public int PrescriptionId { get; set; }
        public string PrescriptionSignature { get; set; }
        public string PESEL { get; set; }
        public Prescription(int prescriptionId, string prescriptionSignature, string pesel)
        {
            PrescriptionId = prescriptionId;
            PrescriptionSignature = prescriptionSignature;
            PESEL = pesel;
        }
        public Prescription(string prescriptionSignature, string pesel)
        {
            PrescriptionSignature = prescriptionSignature;
            PESEL = pesel;
        }
        public override string ToString()
        {
            int s = 15;
            return $"{PrescriptionId.ToString().PadLeft(s, ' ')} | " +
                   $"{PrescriptionSignature.PadLeft(s, ' ')} | " +
                   $"{PESEL.PadLeft(s, ' ')}";
        }
    }
}
