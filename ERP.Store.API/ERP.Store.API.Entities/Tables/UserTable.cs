namespace ERP.Store.API.Entities.Tables
{
    public class UserTable
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Identification { get; set; }

        public string Description { get; set; }

        public int Access_LevelID { get; set; }
    }
}
