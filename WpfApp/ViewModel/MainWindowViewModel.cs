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
        public ICommand AddDepartmentCommand { get; private set; }

        private Department m_Department;

        public Department Department
        {
            get { return m_Department; }
            set
            {
                if (value != null)
                {
                    DepartmentID = value.DepartmentID;
                    Name = value.Name;
                    GroupName = value.GroupName;
                    ModifiedDate = value.ModifiedDate;
                    // OnPropertyChanged("DepartmentID");
                    // OnPropertyChanged("Name");
                    // OnPropertyChanged("GroupName");
                    // OnPropertyChanged("ModifiedDate");
                }

                m_Department = value;
                this.OnPropertyChanged();
            }
        }


        private short m_DepartmentID;

        public short DepartmentID
        {
            get { return m_DepartmentID; }
            set
            {
                m_DepartmentID = value;
                OnPropertyChanged();
            }
        }

        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                OnPropertyChanged();
            }
        }

        private string m_GroupName;

        public string GroupName
        {
            get { return m_GroupName; }
            set
            {
                m_GroupName = value;
                OnPropertyChanged();
            }
        }


        private System.DateTime m_ModifiedDate;

        public System.DateTime ModifiedDate
        {
            get { return m_ModifiedDate; }

            set
            {
                m_ModifiedDate = value;
                OnPropertyChanged();
            }
        }


        
        
        public ObservableCollection<Department> Departments { get; set; }

        public void RefreshData()
        {
            Departments = new ObservableCollection<Department>(_dataContext.GetAllDepartments());
            OnPropertyChanged("Departments");
        }


        public MainWindowViewModel()
        {
            this._dataContext = new DataContext();
            this.RefreshData();

            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddDepartmentCommand = new Command(AddDepartment);

        }


        public void UpdateDepartment()
        {
            Task.Run(() =>
            {
                if (this.Department.DepartmentID != null)
                {

                    Department department = new Department();
                    department.Name = Name;
                    department.GroupName = GroupName;
                    department.ModifiedDate = ModifiedDate;
                    this._dataContext.UpdateDepartment(this.Department.DepartmentID, department); 
                    this.RefreshData();
                    
                }
            });
        }

        public void DeleteDepartment()
        {
            Task.Run(() =>
            {
                this._dataContext.RemoveDepartment(this.Department.DepartmentID);
                this.RefreshData();
            });
        }


        public void AddDepartment()
        {
            Task.Run(() =>
            {
                Department department = new Department();
                department.Name = this.Name;
                department.GroupName = this.GroupName;
                department.ModifiedDate = this.ModifiedDate;  // ModifiedDate should be correct, try to set it automaticly
                // department.DepartmentID = 234;
                this._dataContext.AddDepartment(department);
                this.RefreshData();
            });
        }
        
    }
}