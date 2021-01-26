using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Model;


namespace ModelTests.TestData
{
    public class TestLocalDataContext
    {
        public ObservableCollection<IDepartment> Departments = new ObservableCollection<IDepartment>();
    }
}