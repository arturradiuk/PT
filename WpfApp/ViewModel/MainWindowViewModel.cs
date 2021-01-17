using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Windows.Input;

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