using System.Collections.ObjectModel;
using Model;

namespace ModelTests.TestData
{
    public class TestLocalDataContext
    {
        public ObservableCollection<IDepartment> Departments = new ObservableCollection<IDepartment>();
    }
}