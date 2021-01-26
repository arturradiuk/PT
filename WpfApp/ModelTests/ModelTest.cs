using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model;
using ModelTest.TestData;
using ModelTest.TestLogic;

namespace ModelTest
{
    [TestClass]
    public class ModelTest
    {
        private DataContext _dataContext;
        
        private ITestDataService _testDataService;
        private TestLocalDataContext _tdc;

        [TestInitialize]
        public void TestInitialize()
        {
            _tdc = new TestLocalDataContext();
            TestDataFiller.Fill(_tdc);
            _testDataService = new TestDataService(_tdc);
            _dataContext = new DataContext(_testDataService);
        }
        
        [TestMethod]
        public void GetAllDepartmentsTest()
        {
            IEnumerable<ISerializable> tdsDepartments = _testDataService.GetAllDepartments();
            List<Department> dataContextDepartments = _dataContext.GetAllDepartments();

            List<ISerializable> tdsDepartmentsList = tdsDepartments.ToList();
            
            for (int i = 0; i < dataContextDepartments.Count; i++)
            {
                Assert.AreEqual(_testDataService.GetDepartmentFromISerializable(tdsDepartmentsList[i]), dataContextDepartments[i]);
            }
        }
    }
}
