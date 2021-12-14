namespace ERP.Store.API.Entities.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public AddressViewModel Address { get; set; }

        public UserInfoViewModel UserInfo { get; set; }

        public ContactViewModel Contact { get; set; }

        public ExtraInfoViewModel ExtraInfo { get; set; }

        public ImageViewModel Image { get; set; }
    }
}
