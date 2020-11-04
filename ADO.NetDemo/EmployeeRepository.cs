using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace ADO.NetDemo
{
    public class EmployeeRepository
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_service; User ID = racrathi; Password=Rachit123*";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployees()
        {
           EmployeeModel model = new EmployeeModel();
            try
            {
                using (connection)
                {
                    string query = @"select * from dbo.emp_payroll";
                    SqlCommand command = new SqlCommand(query, connection);
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmployeeID = reader.GetInt32(0);
                            model.EmployeeName = reader.GetString(1);
                            model.PhoneNumber = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.Department = reader.GetString(4);
                            model.Gender = reader.GetString(5);
                            model.BasicPay = reader.GetDouble(6);
                            model.Deductions = reader.GetDouble(7);
                            model.TaxablePay = reader.GetDouble(8);
                            model.Tax = reader.GetDouble(9);
                            model.NetPay = reader.GetDouble(10);
                            model.StartDate = reader.GetDateTime(11);
                            Console.WriteLine("{0},{1},{2}.{3}", model.EmployeeID, model.EmployeeName, model.PhoneNumber, model.Address);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    //this.connection.Close();
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
        /// <summary>
        /// UC2 Add Employee Using Stored Procedures
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (connection)
                {
                    connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", connection);
                    connection.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpName", model.EmployeeName);
                    command.Parameters.AddWithValue("@EmpPhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@EmpAddress", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
       
                    var result = command.ExecuteNonQuery();
                    //this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
