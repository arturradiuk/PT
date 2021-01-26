using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelListener
    {
        private IDepartment m_Department;


        private short m_DepartmentID;

        private string m_GroupName;


        private DateTime m_ModifiedDate;

        private string m_Name;


        public MainWindowViewModel()
        {
            _dataContext = new DataContext();
            RefreshData();

            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddDepartmentCommand = new Command(AddDepartment);
            BufferedDepartment = new Department();
        }


        public MainWindowViewModel(IDataContext dataContext, IDepartment department)
        {
            _dataContext = dataContext;
            RefreshData();

            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddDepartmentCommand = new Command(AddDepartment);
            BufferedDepartment = department;
        }

        private IDataContext _dataContext { get; }
        public ICommand UpdateDepartmentCommand { get; }
        public ICommand DeleteDepartmentCommand { get; }
        public ICommand AddDepartmentCommand { get; }
        public IDepartment BufferedDepartment { get; set; }

        public IDepartment Department
        {
            get => m_Department;
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
                OnPropertyChanged();
            }
        }

        public short DepartmentID
        {
            get => m_DepartmentID;
            set
            {
                m_DepartmentID = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => m_Name;
            set
            {
                m_Name = value;
                OnPropertyChanged();
            }
        }

        public string GroupName
        {
            get => m_GroupName;
            set
            {
                m_GroupName = value;
                OnPropertyChanged();
            }
        }

        public DateTime ModifiedDate
        {
            get => m_ModifiedDate;

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


        public void UpdateDepartment()
        {
            Task.Run(() =>
            {
                if (Department.DepartmentID != null)
                {
                    BufferedDepartment.Name = Name;
                    BufferedDepartment.GroupName = GroupName;
                    BufferedDepartment.ModifiedDate = DateTime.Now; 
                    _dataContext.UpdateDepartment(Department.DepartmentID, BufferedDepartment);
                    RefreshData();
                }
            });
        }

        public void DeleteDepartment()
        {
            Task.Run(() =>
            {
                _dataContext.RemoveDepartment(Department.DepartmentID);
                RefreshData();
            });
        }


        public void AddDepartment()
        {
            Task.Run(() =>
            {
                BufferedDepartment.Name = Name;
                BufferedDepartment.GroupName = GroupName;
                BufferedDepartment.ModifiedDate = DateTime.Now;
                _dataContext.AddDepartment(BufferedDepartment);
                RefreshData();
            });
        }
    }
}