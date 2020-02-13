using Repo.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repo.Repositories
{
    public class CustomerRepository : MsSqlDbAccess
    {
        public int AddToDB(Customer customer)
        {
            return 0;
        }
        public void RemoveFromDB(int id)
        {

        }
        public void UpdateObjectInDB(Customer customer)
        {

        }
        public Customer LoadFromDB(int id)
        {
            Customer customer = null;
            return customer;

        }
        public List<Customer> LoadAllFromDB()
        {
            List<Customer> list = new List<Customer>();
            try
            {
                _connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText =
                        "select * from Customers";
                    sqlCommand.Connection = _connection;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int CustomerId = (int)sqlDataReader["CustomerId"];
                        string Name = sqlDataReader["Name"].ToString();
                        list.Add(new Customer(CustomerId, Name));
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { _connection.Close(); }
            return list;
        }

    }
}
