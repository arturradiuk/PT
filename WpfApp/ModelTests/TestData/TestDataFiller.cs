using System;
using Model;

namespace ViewModelTest.TestData
{
    public static class TestDataFiller
    {
        public static void Fill(TestDataContext dataContext)
        {
            dataContext.Departments.Add(new Department(1, "Los Santos Hills", "LS", new DateTime(2021, 1, 20, 10, 10, 10)));
            dataContext.Departments.Add(new Department(2, "Miami Vice Department", "MV", new DateTime(2020, 2, 20, 1, 10, 10)));
            dataContext.Departments.Add(new Department(3, "Los Santos Police Department", "LS", new DateTime(2020, 3, 2, 10, 10, 10)));
            dataContext.Departments.Add(new Department(4, "Bałuty Drugs Control Department", "BL", new DateTime(2020, 4, 3, 10, 10, 10)));
            dataContext.Departments.Add(new Department(5, "Bałuty Murder Control Department", "BL", new DateTime(2020, 5, 12, 10, 10, 10)));
            dataContext.Departments.Add(new Department(6, "Miami Vice Tourism Department", "MV", new DateTime(2020, 6, 5, 10, 10, 10)));
            dataContext.Departments.Add(new Department(7, "Boat City Justice Department", "BC", new DateTime(2020, 7, 12, 10, 10, 10)));
            dataContext.Departments.Add(new Department(8, "Boat City Education Department", "BC", new DateTime(2020, 8, 5, 10, 10, 10)));
        }
    }
}