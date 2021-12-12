namespace ERP.Store.API.Entities.Models.InputModel
{
    public class EmployeeInputModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressInputModel Address { get; set; }

        public UserInfoInputModel UserInfo { get; set; }

        public ContactInputModel Contact { get; set; }

        public ExtraInfoInputModel ExtraInfo { get; set; }

        public ImageInputModel Image { get; set; }
    }
}
