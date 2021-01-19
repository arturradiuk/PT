using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelListener
    {
        private DataContext DataContext = new DataContext(); //todo should it be interface ???
        public ICommand UpdateDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand AddWindowCommand { get; private set; }
        public ICommand RefreshWindowCommand { get; private set; }

        private void Refresh()
        {
            Departments = DataContext.GetAllDepartments();
        }

        public List<Department> Departments { get; set; }

        public MainWindowViewModel()
        {
            this.DataContext = new DataContext();
            List<Department> depart = this.DataContext.GetAllDepartments();
            // Department department = dataContext.GetDepartmentById(5);
            // this.Departments = new ObservableCollection<Department>(depart);
            this.Refresh();
            int f = 6;
        }


        public void UpdateDepartment()
        {
            Task.Run(() => { });
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