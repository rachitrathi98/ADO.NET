using ADO.NetDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeePayrollServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UC3_CompareUpdatedSalary()
        {
            SalaryDetailModel model = new SalaryDetailModel()
            {
                EmployeeId = 1,
                SalaryId = 1021,
                Salary = 3000000,
                Month = "Jan",
            };
            EmployeeRepository repo = new EmployeeRepository();
            int actual = repo.UpdateEmployee(model);
            Assert.AreEqual(model.Salary, actual);

        }
    }
}
