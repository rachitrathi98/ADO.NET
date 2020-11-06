using System;

namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Service!");

            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Rachit";
            model.PhoneNumber = "9004025062";
            model.Address = "Bangalore";
            model.Department = "SD";
            model.Gender = "M";
            model.BasicPay = 53000;
            model.Deductions = 2000;
            model.TaxablePay = 500;
            model.Tax = 1000;
            model.NetPay = 50000;
            model.StartDate = DateTime.Now;
            EmployeeRepository repository = new EmployeeRepository();
            repository.GetAllEmployees();
            bool res = repository.AddEmployee(model);
            if (res)
            {
                Console.WriteLine("Employee Added Successfully");
                repository.GetAllEmployees();
            }
            else
            {
                Console.WriteLine("Employee isn't Added");
            }
            Console.WriteLine("Retrieving Employee from StartDate");
            EmployeeModel model1 = new EmployeeModel()
            {
                StartDate = DateTime.Parse("2019-11-13")
            };
            repository.RetrieveEmployeeBasedOnStartDate(model1);

            Console.WriteLine("Retrieving Sum Avg Min Max from Employee");
            EmployeeModel model2 = new EmployeeModel()
            {
                Gender = "F"
            };
            repository.FindSumAvgMinMaxSalaryOfEmployee(model2);

        }
    }
}