using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO.NetDemo
{
    public class EmployeeRepository
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_service; User ID = racrathi; Password=Rachit123*";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployees()
        {
            // EmployeeModel model = new EmployeeModel();
            try
            {
                using (connection)
                {

                    this.connection.Open();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
               

            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
