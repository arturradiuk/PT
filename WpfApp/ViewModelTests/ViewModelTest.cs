using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using ViewModel;
using ViewModelTests.TestData;
using ViewModelTests.TestLogic;
using ViewModelTests.TestModel;

namespace ViewModelTests
{
    [TestClass]
    public class ViewModelTest
    {
        private IDataContext _dataContext;
        private MainWindowViewModel _mainWindowViewModel;

        private ITestDataService _testDataService;
        private TestLocalDataContext _tldc;

        [TestInitialize]
        public void TestInitialize()
        {
            IDepartment department = new TestDepartment();

            var _tldc = new TestLocalDataContext();
            TestDataFiller.Fill(_tldc);

            ITestDataService _testDataService = new TestDataService(_tldc);
            IDataContext _dataContext = new DataContext(_testDataService);
            _mainWindowViewModel = new MainWindowViewModel(_dataContext, department);
        }

        [TestMethod]
        public void AddDepartmentTest()
        {
            var departmentsNum = _mainWindowViewModel.Departments.Count;

            IDepartment department = new TestDepartment(25, "testName", "testGroup", DateTime.Now);
            _mainWindowViewModel.Name = department.Name;
            _mainWindowViewModel.GroupName = department.GroupName;
            _mainWindowViewModel.DepartmentID = department.DepartmentID;
            _mainWindowViewModel.AddDepartmentCommand.Execute(null);

            Thread.Sleep(100);
            Assert.AreEqual(departmentsNum + 1, _mainWindowViewModel.Departments.Count);
        }

        [TestMethod]
        public void UpdateDepartmentTest()
        {
            var originalDepartment = _mainWindowViewModel.Departments.First(d => d.DepartmentID == 1);
            _mainWindowViewModel.Department = originalDepartment;
            _mainWindowViewModel.Name = "testName";
            _mainWindowViewModel.GroupName = "testGroupName";
            _mainWindowViewModel.ModifiedDate = DateTime.Now;
            _mainWindowViewModel.DepartmentID = 1;
            _mainWindowViewModel.UpdateDepartmentCommand.Execute(null);

            Thread.Sleep(100);
            var updatedDepartment = _mainWindowViewModel.Departments.First(d => d.DepartmentID == 1);

            Assert.AreNotEqual(originalDepartment, updatedDepartment);
            Assert.AreEqual("testName", updatedDepartment.Name);
        }

        [TestMethod]
        public void DeleteDepartmentTest()
        {
            var departmentsNum = _mainWindowViewModel.Departments.Count;
            var originalDepartment = _mainWindowViewModel.Departments.First(d => d.DepartmentID == 1);
            _mainWindowViewModel.Department = originalDepartment;

            _mainWindowViewModel.DeleteDepartmentCommand.Execute(null);

            Thread.Sleep(100);

            Assert.IsFalse(_mainWindowViewModel.Departments.Contains(originalDepartment));
            Assert.AreEqual(departmentsNum - 1, _mainWindowViewModel.Departments.Count);
        }
    }
}