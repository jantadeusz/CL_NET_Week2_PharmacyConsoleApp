using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Repo.Tables;

namespace Repo.Repositories
{
    public class PrescriptionRepository : MsSqlDbAccess
    {
        public int AddToDB(Prescription prescription)
        {
            int result = 0;
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "insert into Prescriptions(PrescriptionSignature, PESEL) " +
                    "values(@PrescriptionSignature, @PESEL)" +
                    "select SCOPE_identity()", _connection))
                {
                    SqlParameter sqlPrescriptionSignature = new SqlParameter("@PrescriptionSignature", prescription.PrescriptionSignature);
                    sqlCommand.Parameters.Add(sqlPrescriptionSignature);
                    SqlParameter sqlPESEL = new SqlParameter("@PESEL", prescription.PESEL);
                    sqlCommand.Parameters.Add(sqlPESEL);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        result = Int32.Parse(sqlDataReader[0].ToString());
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
            return result;
        }
        public void RemoveFromDB(int id)
        {
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "delete from Prescriptions where PrescriptionId = @PrescriptionId", _connection))
                {
                    SqlParameter sqlPrescriptionId = new SqlParameter("@PrescriptionId", id);
                    sqlCommand.Parameters.Add(sqlPrescriptionId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
        }
        public void UpdateObjectInDB(Prescription prescription)
        {
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "update Prescriptions set " +
                    "PrescriptionSignature = @PrescriptionSignature, " +
                    "PESEL = @PESEL " +
                    "where PrescriptionId = @PrescriptionId", _connection))
                {
                    SqlParameter sqlPrescriptionSignature = new SqlParameter("@PrescriptionSignature", prescription.PrescriptionSignature);
                    sqlCommand.Parameters.Add(sqlPrescriptionSignature);
                    SqlParameter sqlPESEL = new SqlParameter("@PESEL", prescription.PESEL);
                    sqlCommand.Parameters.Add(sqlPESEL);
                    SqlParameter sqlPrescriptionId = new SqlParameter("@PrescriptionId", prescription.PrescriptionId);
                    sqlCommand.Parameters.Add(sqlPrescriptionId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
        }
        public Prescription LoadFromDB(int id)
        {
            Prescription prescription = null;
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Prescriptions where PrescriptionId = @PrescriptionId";
                    sqlCommand.Connection = _connection;
                    SqlParameter sqlPrescriptionId = new SqlParameter("@PrescriptionId", id);
                    sqlCommand.Parameters.Add(sqlPrescriptionId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int PrescriptionId = (int)sqlDataReader["PrescriptionId"];
                        string PrescriptionSignature = sqlDataReader["PrescriptionSignature"].ToString();
                        string PESEL = sqlDataReader["PESEL"].ToString();
                        prescription = new Prescription(PrescriptionId, PrescriptionSignature, PESEL);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return prescription;
        }
        public List<Prescription> LoadAllFromDB()
        {
            List<Prescription> result = new List<Prescription>();
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Prescriptions";
                    sqlCommand.Connection = _connection;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int PrescriptionId = (int)sqlDataReader["PrescriptionId"];
                        string PrescriptionSignature = sqlDataReader["PrescriptionSignature"].ToString();
                        string PESEL = sqlDataReader["PESEL"].ToString();
                        result.Add(new Prescription(PrescriptionId, PrescriptionSignature, PESEL));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return result;
        }
        public List<Prescription> LoadPrescriptionsFromMedicineId(int MedicineId)
        {
            var result = new List<Prescription>();
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from Orders " +
                        "join Prescriptions on Orders.PrescriptionId = Prescriptions.PrescriptionId " +
                        "join Medicines on Orders.MedicineId = Medicines.MedicineId " +
                        "join Customers on Orders.CustomerId = Customers.CustomerId " +
                        "where Medicines.MedicineId = @MedicineId";
                    sqlCommand.Connection= _connection;
                    SqlParameter sqlMedicineId = new SqlParameter("@MedicineId", MedicineId);
                    sqlCommand.Parameters.Add(sqlMedicineId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int PrescriptionId = (int)sqlDataReader["PrescriptionId"];
                        string PrescriptionSignature = sqlDataReader["PrescriptionSignature"].ToString();
                        string PESEL = sqlDataReader["PESEL"].ToString();
                        result.Add(new Prescription(PrescriptionId, PrescriptionSignature, PESEL));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return result;
        }
    }
}
