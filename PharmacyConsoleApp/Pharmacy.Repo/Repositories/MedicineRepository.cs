using Repo.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repo.Repositories
{
    public class MedicineRepository : MsSqlDbAccess
    {
        public int AddToDB(Medicine medicine)
        {
            int result = 0;
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "insert into Medicines(Name, Manufacturer, Price, WithPrescription) " +
                    "values(@Name, @Manufacturer, @Price, @WithPrescription); " +
                    "select SCOPE_identity()", _connection))
                {
                    SqlParameter sqlName = new SqlParameter("@Name", medicine.Name);
                    sqlCommand.Parameters.Add(sqlName);
                    SqlParameter sqlManufacturer = new SqlParameter("@Manufacturer", medicine.Manufacturer);
                    sqlCommand.Parameters.Add(sqlManufacturer);
                    SqlParameter sqlPrice = new SqlParameter("@Price", medicine.Price);
                    sqlCommand.Parameters.Add(sqlPrice);
                    SqlParameter sqlWithPrescription = new SqlParameter("@WithPrescription", medicine.WithPrescription);
                    sqlCommand.Parameters.Add(sqlWithPrescription);
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
        public void RemoveFromDB(int MedicineId)
        {
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "delete from Medicines where MedicineId = @MedicineId", _connection))
                {
                    SqlParameter sqlId = new SqlParameter("@MedicineId", MedicineId);
                    sqlCommand.Parameters.Add(sqlId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
        }
        public void UpdateObjectInDB(Medicine medicine)
        {
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "update Medicines set " +
                    "Name = '@Name'," +
                    "Manufacturer = '@Manufacturer'," +
                    "Price = '@Price'," +
                    "WithPrescription = '@WithPrescription'" +
                    "where MedicineId = @MedicineId", _connection))
                {
                    SqlParameter sqlName = new SqlParameter("@Name", medicine.Name);
                    sqlCommand.Parameters.Add(sqlName);
                    SqlParameter sqlManufacturer = new SqlParameter("@Manufacturer", medicine.Manufacturer);
                    sqlCommand.Parameters.Add(sqlManufacturer);
                    SqlParameter sqlPrice = new SqlParameter("@Price", (decimal)medicine.Price);
                    sqlCommand.Parameters.Add(sqlPrice);
                    SqlParameter sqlWithPrescription = new SqlParameter("@WithPrescription", (bool)medicine.WithPrescription);
                    sqlCommand.Parameters.Add(sqlWithPrescription);
                    SqlParameter sqlMedicineId = new SqlParameter("@MedicineId", (int)medicine.MedicineId);
                    sqlCommand.Parameters.Add(sqlMedicineId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
        }
        public Medicine LoadFromDB(int id)
        {
            Medicine medicine = null;
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Medicines where MedicineId = @MedicineId";
                    sqlCommand.Connection = _connection;
                    SqlParameter sqlId = new SqlParameter("@MedicineId", id);
                    sqlCommand.Parameters.Add(sqlId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int Id = (int)sqlDataReader["MedicineId"];
                        string Name = sqlDataReader["Name"].ToString();
                        string Manufacturer = sqlDataReader["Manufacturer"].ToString();
                        decimal Price = (decimal)sqlDataReader["Price"];
                        bool WithPrescription = (bool)sqlDataReader["WithPrescription"];
                        medicine = new Medicine(Id, Name, Manufacturer, Price, WithPrescription);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return medicine;
        }
        public List<Medicine> LoadAllFromDB()
        {
            List<Medicine> list = new List<Medicine>();
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText =
                        "SELECT MedicineId, Name, Manufacturer, Price, WithPrescription FROM Medicines";
                    sqlCommand.Connection = _connection;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int Id = (int)sqlDataReader["MedicineId"];
                        string Name = sqlDataReader["Name"].ToString();
                        string Manufacturer = sqlDataReader["Manufacturer"].ToString();
                        decimal Price = (decimal)sqlDataReader["Price"];
                        bool WithPrescription = (bool)sqlDataReader["WithPrescription"];
                        list.Add(new Medicine(Id, Name, Manufacturer, Price, WithPrescription));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return list;
        }
        public List<Medicine> LoadMedicinesFromPrescription(int prescriptionId)
        {
            var result = new List<Medicine>();
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "select * from Orders " +
                        "join Prescriptions on Orders.PrescriptionId = Prescriptions.PrescriptionId " +
                        "join Medicines on Orders.MedicineId = Medicines.MedicineId " +
                        "join Customers on Orders.CustomerId = Customers.CustomerId " +
                        "where Prescriptions.PrescriptionId = @PrescriptionId";
                    sqlCommand.Connection = _connection;
                    SqlParameter sqlPrescriptionId = new SqlParameter("@PrescriptionId", prescriptionId);
                    sqlCommand.Parameters.Add(sqlPrescriptionId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int Id = (int)sqlDataReader["MedicineId"];
                        string Name = sqlDataReader["Name"].ToString();
                        string Manufacturer = sqlDataReader["Manufacturer"].ToString();
                        decimal Price = (decimal)sqlDataReader["Price"];
                        bool WithPrescription = (bool)sqlDataReader["WithPrescription"];
                        result.Add(new Medicine(Id, Name, Manufacturer, Price, WithPrescription));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return result;
        }
    }
}

