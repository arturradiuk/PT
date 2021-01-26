using System.Collections.ObjectModel;
using Model;

namespace ViewModelTests.TestData
{
    public class TestLocalDataContext
    {
        public ObservableCollection<IDepartment> Departments = new ObservableCollection<IDepartment>();
    }
}