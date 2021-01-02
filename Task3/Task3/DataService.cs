using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class DataService
    {
        private static LocalDataContext dataContext = new LocalDataContext();
        public List<Product> firstMethod()
        {
            return dataContext.GetTable<Product>().ToList();
        }
    }
}
