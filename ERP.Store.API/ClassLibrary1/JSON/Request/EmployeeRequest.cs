namespace ERP.Store.Desktop.Entities.JSON.Request
{
    public class EmployeeRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressRequest Address { get; set; }

        public UserInfoRequest UserInfo { get; set; }

        public ContactRequest Contact { get; set; }

        public ExtraInfoRequest ExtraInfo { get; set; }

        public ImageRequest Image { get; set; }
    }
}
