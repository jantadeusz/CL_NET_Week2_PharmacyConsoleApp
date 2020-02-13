using Repo.Tables;
using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Text;

namespace Repo.Repositories
{
    public class OrderRepository : MsSqlDbAccess
    {
        public int AddOrderWithoutPrescrToDB(Order order)
        {
            int result = 0;
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "insert into Orders(MedicineId, Date, Amount, CustomerId) " +
                    "values(@MedicineId, @Date, @Amount, @CustomerId)" +
                    "select SCOPE_identity()", _connection))
                {
                    SqlParameter sqlMedicineId = new SqlParameter("@MedicineId", order.MedicineId);
                    sqlCommand.Parameters.Add(sqlMedicineId);
                    SqlParameter sqlDate = new SqlParameter("@Date", order.Date);
                    sqlCommand.Parameters.Add(sqlDate);
                    SqlParameter sqlAmount = new SqlParameter("@Amount", order.Amount);
                    sqlCommand.Parameters.Add(sqlAmount);
                    SqlParameter sqlCustomerId = new SqlParameter("@CustomerId", order.CustomerId);
                    sqlCommand.Parameters.Add(sqlCustomerId);
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
        public int AddOrderWithPrescrToDB(Order order)
        {
            int result = 0;
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "insert into Orders(MedicineId, PrescriptionId, Date, Amount, CustomerId) " +
                    "values(@MedicineId, @PrescriptionId, @Date, @Amount, @CustomerId)" +
                    "select SCOPE_identity()", _connection))
                {
                    SqlParameter sqlMedicineId = new SqlParameter("@MedicineId", order.MedicineId);
                    sqlCommand.Parameters.Add(sqlMedicineId);
                    SqlParameter sqlPrescriptionId = new SqlParameter("@PrescriptionId", order.PrescriptionId);
                    sqlCommand.Parameters.Add(sqlPrescriptionId);
                    SqlParameter sqlDate = new SqlParameter("@Date", order.Date);
                    sqlCommand.Parameters.Add(sqlDate);
                    SqlParameter sqlAmount = new SqlParameter("@Amount", order.Amount);
                    sqlCommand.Parameters.Add(sqlAmount);
                    SqlParameter sqlCustomerId = new SqlParameter("@CustomerId", order.CustomerId);
                    sqlCommand.Parameters.Add(sqlCustomerId);
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
        public void RemoveFromDB(int OrderId)
        {
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "delete from Orders where OrderId = @OrderId", _connection))
                {
                    SqlParameter sqlId = new SqlParameter("@OrderId", OrderId);
                    sqlCommand.Parameters.Add(sqlId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
        }
        public void UpdateObjectInDB(Order order)
        {
            _connection.Open();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "update Orders set " +
                    "MedicineId = @MedicineId, " +
                    "PrescriptionId = @PrescriptionId, " +
                    "Date = @Date, " +
                    "Amount = @Amount, " +
                    "CustomerId = @CustomerId " +
                    "where OrderId = @OrderId", _connection))
                {
                    SqlParameter sqlMedicineId = new SqlParameter("@MedicineId", order.MedicineId);
                    sqlCommand.Parameters.Add(sqlMedicineId);
                    SqlParameter sqlPrescriptionId = new SqlParameter("@PrescriptionId", order.PrescriptionId);
                    sqlCommand.Parameters.Add(sqlPrescriptionId);
                    SqlParameter sqlDate = new SqlParameter("@Date", order.Date);
                    sqlCommand.Parameters.Add(sqlDate);
                    SqlParameter sqlAmount = new SqlParameter("@Amount", order.Amount);
                    sqlCommand.Parameters.Add(sqlAmount);
                    SqlParameter sqlCustomerId = new SqlParameter("@CustomerId", order.CustomerId);
                    sqlCommand.Parameters.Add(sqlCustomerId);
                    SqlParameter sqlOrderId = new SqlParameter("@OrderId", order.OrderId);
                    sqlCommand.Parameters.Add(sqlOrderId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { _connection.Close(); }
        }
        public Order LoadFromDB(int id)
        {
            Order order = null;
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Orders where OrderId = @OrderId";
                    sqlCommand.Connection = _connection;
                    SqlParameter sqlOrderId = new SqlParameter("@OrderId", id);
                    sqlCommand.Parameters.Add(sqlOrderId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int OrderId = (int)sqlDataReader["OrderId"];
                        int MedicineId = (int)sqlDataReader["MedicineId"];
                        int PrescriptionId = (int)sqlDataReader["PrescriptionId"];
                        DateTime Date = (DateTime)sqlDataReader["Date"];
                        int Amount = Int32.Parse(sqlDataReader["Amount"].ToString());
                        int CustomerId = (int)sqlDataReader["CustomerId"];
                        order = new Order(OrderId, MedicineId, PrescriptionId, Date, Amount, CustomerId);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return order;
        }
        public List<Order> LoadAllFromDB()
        {
            List<Order> list = new List<Order>();
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText =
                        "SELECT $ FROM Orders";
                    sqlCommand.Connection = _connection;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int OrderId = (int)sqlDataReader["OrderId"];
                        int MedicineId = (int)sqlDataReader["MedicineId"];
                        int PrescriptionId = (int)sqlDataReader["PrescriptionId"];
                        DateTime Date = (DateTime)sqlDataReader["Date"];
                        int Amount = Int32.Parse(sqlDataReader["Amount"].ToString());
                        int CustomerId = (int)sqlDataReader["CustomerId"];
                        list.Add(new Order(OrderId, MedicineId, PrescriptionId, Date, Amount, CustomerId));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return list;
        }
    }
}
