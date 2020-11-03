using System;

namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository repository = new EmployeeRepository();
            repository.GetAllEmployees();
            // Console.WriteLine(repository.AddEmployee(model) ? "Record inserted successfully " : "Failed");
            Console.ReadLine();
        }
    }
}