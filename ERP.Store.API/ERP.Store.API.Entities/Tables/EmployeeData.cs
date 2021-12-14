namespace ERP.Store.API.Entities.Tables
{
    public class EmployeeData
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public int Access_LevelID { get; set; }

        public int User_InfoID { get; set; }

        public int ContactID { get; set; }

        public int AddressID { get; set; }

        public double Salary { get; set; }

        public int JobID { get; set; }

        public string Description { get; set; }
    }
}
