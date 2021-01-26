using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using ModelTests.TestData;
using ModelTests.TestLogic;

namespace ModelTests
{
    [TestClass]
    public class ModelTest
    {
        private DataContext _dataContext;

        private ITestDataService _testDataService;
        private TestLocalDataContext _tldc;

        [TestInitialize]
        public void TestInitialize()
        {
            _tldc = new TestLocalDataContext();
            TestDataFiller.Fill(_tldc);
            _testDataService = new TestDataService(_tldc);
            _dataContext = new DataContext(_testDataService);
        }

        [TestMethod]
        public void GetDepartmentFromISerializable()
        {
            short ID = 1;
            var serializableDepartment = _testDataService.GetDepartmentById(ID);
            var department = _dataContext.GetDepartmentFromISerializable(serializableDepartment);
            Assert.AreEqual(ID, department.DepartmentID);
        }

        [TestMethod]
        public void GetAllDepartmentsTest()
        {
            var tdsDepartments = _testDataService.GetAllDepartments();
            var dataContextDepartments = _dataContext.GetAllDepartments();

            var tdsDepartmentsList = tdsDepartments.ToList();

            for (var i = 0; i < dataContextDepartments.Count; i++)
                Assert.AreEqual(_dataContext.GetDepartmentFromISerializable(tdsDepartmentsList[i]),
                    dataContextDepartments[i]);
        }

        [TestMethod]
        public void RemoveDepartmentTest()
        {
            short ID = 1;
            var departmentsNum = _dataContext.GetAllDepartments().Count;
            Assert.IsTrue(_dataContext.GetAllDepartments().Any(d => d.DepartmentID == 1));

            _dataContext.RemoveDepartment(ID);

            Assert.AreEqual(departmentsNum - 1, _dataContext.GetAllDepartments().Count);
            Assert.IsFalse(_dataContext.GetAllDepartments().Any(d => d.DepartmentID == 1));
        }

        [TestMethod]
        public void UpdateDepartmentTest()
        {
            short ID = 1;
            var department = _dataContext.GetAllDepartments().First(d => d.DepartmentID == 1);

            var testDepartmentName = "testDepartmentName";

            Assert.AreNotEqual(department.Name, testDepartmentName);

            var testDepartment = new Department(department.DepartmentID, testDepartmentName,
                department.GroupName,
                department.ModifiedDate);

            _dataContext.UpdateDepartment(ID, testDepartment);

            var updatedDepartment = _dataContext.GetAllDepartments().First(d => d.DepartmentID == 1);
            Assert.AreNotEqual(department, updatedDepartment);
            Assert.AreEqual(testDepartmentName, updatedDepartment.Name);
        }

        [TestMethod]
        public void AddDepartmentTest()
        {
            var departments = _dataContext.GetAllDepartments();
            var departmentsNum = departments.Count;

            var testDepartment = new Department(20, "testDep", "testDepGroup", DateTime.Now);

            Assert.IsFalse(departments.Contains(testDepartment));

            _dataContext.AddDepartment(testDepartment);

            departments = _dataContext.GetAllDepartments();

            Assert.AreEqual(departmentsNum + 1, departments.Count);
            Assert.IsTrue(departments.Contains(testDepartment));
        }
    }
}