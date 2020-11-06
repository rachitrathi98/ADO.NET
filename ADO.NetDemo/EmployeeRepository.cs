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
        /// <summary>
        /// UC1: Gets deatils of all employees.
        /// </summary>
        /// <exception cref="Exception"></exception>
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
                            Console.WriteLine("{0},{1},{2},{3}", model.EmployeeID, model.EmployeeName, model.PhoneNumber, model.Address);
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
        /// UC2: Add Employee Using Stored Procedures
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
        /// <summary>
        ///UC3: Updates the employee using Stored Procedure.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int UpdateEmployee(SalaryDetailModel model)
        {
            int salary = 0;
            try
            {
                using (connection)
                {
                    connection = new SqlConnection(connectionString);
                    SalaryDetailModel displayModel = new SalaryDetailModel();

                    SqlCommand command = new SqlCommand("Sp_UpdateEmployee_Payroll", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@S_id", model.SalaryId);
                    command.Parameters.AddWithValue("@salary", model.Salary);
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@E_id", model.EmployeeId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            displayModel.EmployeeId = reader.GetInt32(0);
                            displayModel.EmployeeName = reader.GetString(1);
                            displayModel.Designation = reader.GetString(2);
                            displayModel.Month = reader.GetString(3);
                            displayModel.SalaryId = reader.GetInt32(4);
                            displayModel.Salary = reader.GetInt32(5);

                            salary = displayModel.Salary;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
                    return salary;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// UC5: Retrieves the employee based on start date using Stored Procedure.
        /// </summary>
        /// <param name="model">The model.</param>
        public void RetrieveEmployeeBasedOnStartDate(EmployeeModel model)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("Sp_RetrieveEmployeeBasedOnStartDate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StartDate", model.StartDate);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.EmployeeID = reader.GetInt32(0);
                        model.EmployeeName = reader.GetString(1);
                        model.Address = reader.GetString(2);
                        model.Department = reader.GetString(3);
                        model.BasicPay = reader.GetInt32(4);
                        model.StartDate = reader.GetDateTime(5);
                        Console.WriteLine("{0},{1},{2},{3},{4},{5}", model.EmployeeID, model.EmployeeName, model.Address, model.Department, model.BasicPay, model.StartDate);
                    }
                }
                else
                {
                    Console.WriteLine("No rows Found");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// UC6: FindSumAvgMinMaxSalaryOfEmployee
        /// </summary>
        /// <param name="model"></param>
        public void FindSumAvgMinMaxSalaryOfEmployee(EmployeeModel model)
        {
            try
            {
                FindSumAvgMinMaxSalary salary = new FindSumAvgMinMaxSalary();

                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("Sp_FindSumAvgMinMaxSalaryOfEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Gender", model.Gender);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        salary.gender = Convert.ToChar(reader["Gender"]);
                        salary.count = Convert.ToInt32(reader["TotalEmp"]);
                        salary.totalSum = Convert.ToDecimal(reader["Sum"]);
                        salary.avg = Convert.ToDecimal(reader["AvgSalary"]);
                        salary.min = Convert.ToDecimal(reader["MinSalary"]);
                        salary.max = Convert.ToDecimal(reader["MaxSalary"]);
                        Console.WriteLine("Gender: {0}, TotalCount: {1}, TotalSalary: {2}, AvgSalary:  {3}, MinSalary:  {4}, MinSalary:  {5}", salary.gender, salary.count, salary.totalSum, salary.avg, salary.min, salary.max);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
            Console.WriteLine();
        }
    }
}
