using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelListener
    {
        private IDataContext _dataContext; //todo should it be interface ???
        public ICommand UpdateDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand AddWindowCommand { get; private set; }
        public ICommand RefreshWindowCommand { get; private set; }

        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        private void Refresh()
        {
            Departments = _dataContext.GetAllDepartments();
        }

        public List<Department> Departments { get; set; }

        public MainWindowViewModel()
        {
            this._dataContext = new DataContext();
            this.Refresh(); 
            
            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddWindowCommand = new Command(AddWindow);
            RefreshWindowCommand = new Command(RefreshWindow);
            
            // ModifiedDate = DateTime.Now;
            // Department department = dataContext.GetDepartmentById(5);
            // this.Departments = new ObservableCollection<Department>(depart);

            // this.Refresh();
            // this.DataContext.UpdateDepartment(this.Departments.First().DepartmentID, new Department(113, "temp", "temp_group", ModifiedDate));
            // int f = 6;
        }


        public void UpdateDepartment()
        {
            Task.Run(() =>
            {
                Department department = new Department();
                department.Name = Name;
                department.GroupName = GroupName;
                department.ModifiedDate = ModifiedDate;
            });
            
            
        }

        public void DeleteDepartment()
        {
            Task.Run(() => { });
        }


        public void AddWindow()
        {
            Task.Run(() => { });
        }

        public void RefreshWindow()
        {
            Task.Run(() => { });
        }
    }
}