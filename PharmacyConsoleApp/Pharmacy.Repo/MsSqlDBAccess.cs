using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Repo
{
    public abstract class MsSqlDbAccess : IDisposable
    {
        protected SqlConnection _connection;

        public MsSqlDbAccess()
        {
            string connectionString =
                "Integrated Security=SSPI;" + "Data Source=.\\SQLEXPRESS01;" + "Initial Catalog=Pharmacy;";
            _connection = new SqlConnection();
            _connection.ConnectionString = connectionString;
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
        //public abstract int AddToDB(PharmacyObject pharmacyObject);
        //public abstract void RemoveFromDB(int id);
        //public abstract void UpdateObjectInDB(PharmacyObject pharmacyObject);
        //public abstract PharmacyObject LoadFromDB(int id);
        //public abstract List<PharmacyObject> LoadAllFromDB();
    }
}
