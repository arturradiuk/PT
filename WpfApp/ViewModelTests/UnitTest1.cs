using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace ViewModelTests
{
    [TestClass]
    public class ViewModelTest
    {
        private MainWindowViewModel MainWindowVM = new MainWindowViewModel();
        
        [TestMethod]
        public void UpdateDepartmentTest()
        {
            DateTime currentDate = DateTime.Now;
            MainWindowVM.Name = "TestName";
            MainWindowVM.GroupName = "TestGroup";
            MainWindowVM.ModifiedDate = currentDate;
            MainWindowVM.DepartmentID = 1;
            MainWindowVM.UpdateDepartmentCommand.Execute(null);
            Assert.IsTrue(false);
        }
    }
}