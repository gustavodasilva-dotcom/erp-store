using System;

namespace ERP.Store.Desktop.Entities.JSON.Response
{
    public class UserResponse
    {
        public int EmployeeID { get; set; }
        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public TokenResponse Token { get; set; }
    }
}
