using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Windows.Input;
using Data;
using System.Data.Linq;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelListener
    {
        public ICommand UpdateDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand AddWindowCommand { get; private set; }
        public ICommand RefreshWindowCommand { get; private set; }

        private void Refresh()
        {
            // Department department = new Department();
        }

        public MainWindowViewModel()
        {
            this.Refresh();
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