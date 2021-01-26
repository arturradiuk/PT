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
        private IDataContext _dataContext { get; }
        public ICommand UpdateDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand AddDepartmentCommand { get; private set; }

        private IDepartment m_Department;
        public IDepartment BufferedDepartment { get; set; }
        
        public IDepartment Department
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


        public ObservableCollection<IDepartment> Departments { get; set; }

        public void RefreshData()
        {
            Departments = new ObservableCollection<IDepartment>(_dataContext.GetAllDepartments());
            OnPropertyChanged("Departments");
        }


        public MainWindowViewModel()
        {
            this._dataContext = new DataContext();
            this.RefreshData();

            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddDepartmentCommand = new Command(AddDepartment);
            BufferedDepartment = new Department();
        }


        public MainWindowViewModel(IDataContext dataContext, IDepartment department)
        {
            this._dataContext = dataContext;
            this.RefreshData();

            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddDepartmentCommand = new Command(AddDepartment);
            BufferedDepartment = department;
        }


        public void UpdateDepartment()
        {
            Task.Run(() =>
            {
                if (this.Department.DepartmentID != null)
                {
                    BufferedDepartment.Name = Name;
                    BufferedDepartment.GroupName = GroupName;
                    // department.ModifiedDate = ModifiedDate; // ModifiedDate should be correct, try to set it automatically
                    BufferedDepartment.ModifiedDate =
                        DateTime.Now; // ModifiedDate should be correct, try to set it automatically
                    this._dataContext.UpdateDepartment(this.Department.DepartmentID, BufferedDepartment);
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
                BufferedDepartment.Name = this.Name;
                BufferedDepartment.GroupName = this.GroupName;
                BufferedDepartment.ModifiedDate = DateTime.Now;
                this._dataContext.AddDepartment(BufferedDepartment);
                this.RefreshData();
            });
        }
    }
}