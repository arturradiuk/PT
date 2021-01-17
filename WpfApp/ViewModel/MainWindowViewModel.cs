using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CustomData;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelListener
    {
        private DataRepository DepartmentRepository;
        private ObservableCollection<DepartmentEntity> _departments;
        private Department _department;
        private string _name;
        private string _groupName;
        private DateTime _modifiedDate;

        public ObservableCollection<DepartmentEntity> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                NotifyPropertyChanged();
            }
        }

        public Department Department
        {
            get => _department;
            set
            {
                if (value != null)
                {
                    Name = value.Name;
                    GroupName = value.GroupName;
                    ModifiedDate = value.ModifiedDate;
                }

                _department = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                _modifiedDate = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand UpdateDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand AddWindowCommand { get; private set; }

        public Action<string> MessageBoxShowDelegate { get; set; } = x =>
            throw new ArgumentOutOfRangeException(
                $"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");


        public MainWindowViewModel()
        {
            DepartmentRepository = new DataRepository();
            ModifiedDate = DateTime.Now;
            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddWindowCommand = new Command(AddWindow);
        }


        public void UpdateDepartment()
        {
            Task.Run(() =>
            {
                DepartmentEntity dep = new DepartmentEntity();
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

        public void ShowMessagePopup(string message)
        {
            MessageBoxShowDelegate(message);
        }
    }
}