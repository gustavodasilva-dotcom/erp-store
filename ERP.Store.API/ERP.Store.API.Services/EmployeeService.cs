using System;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Services.CustomExceptions;

namespace ERP.Store.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUserRepository _userRepository;

        private readonly IImageRepository _imageRepository;

        private readonly IAddressRepository _addressRepository;

        private readonly IContactRepository _contactRepository;

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService
        (
            IUserRepository userRepository,
            IImageRepository imageRepository,
            IAddressRepository addressRepository,
            IContactRepository contactRepository,
            IEmployeeRepository employeeRepository
        )
        {
            _userRepository = userRepository;

            _imageRepository = imageRepository;

            _addressRepository = addressRepository;

            _contactRepository = contactRepository;

            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeViewModel> GetEmployeeAsync(string identification)
        {
            try
            {
                var employeeData = await _employeeRepository.GetEmployeeAsync(identification);

                if (employeeData == null)
                    throw new NotFoundException($"There's no employee registered with the {identification} identification number.");

                var addressData = await _addressRepository.GetAddressAsync(employeeData.AddressID);

                if (addressData == null)
                    throw new NotFoundException($"There's no address registered for the employee.");

                var userData = await _userRepository.GetUserInfoAsync(employeeData.User_InfoID);

                if (userData == null)
                    throw new NotFoundException($"There's no user info registered for the employee.");

                var contactData = await _contactRepository.GetContactAsync(employeeData.ContactID);

                if (contactData == null)
                    throw new NotFoundException($"There's no contact data registered for the employee.");

                var imageData = await _imageRepository.GetEmployeesImage(employeeData.EmployeeID);

                return new EmployeeViewModel
                {
                    ID = employeeData.EmployeeID,
                    FirstName = employeeData.FirstName,
                    MiddleName = employeeData.MiddleName,
                    LastName = employeeData.LastName,
                    Identification = employeeData.Identification,
                    Address = new AddressViewModel
                    {
                        Zip = addressData.Zip,
                        Street = addressData.Street,
                        Number = addressData.Number,
                        Comment = addressData.Comment,
                        Neighborhood = addressData.Neighborhood,
                        City = addressData.City,
                        State = addressData.State,
                        Country = addressData.Country
                    },
                    UserInfo = new UserInfoViewModel
                    {
                        Username = userData.Username,
                        Password = userData.Password
                    },
                    Contact = new ContactViewModel
                    {
                        Email = contactData.Email,
                        Cellphone = contactData.Cellphone,
                        Phone = contactData.Phone
                    },
                    Image = new ImageViewModel
                    {
                        IsImage = !string.IsNullOrEmpty(imageData.Base64),
                        Base64 = imageData.Base64
                    },
                    ExtraInfo = new ExtraInfoViewModel
                    {
                        AccessLevelID = employeeData.Access_LevelID,
                        Salary = employeeData.Salary,
                        JobID = employeeData.JobID
                    }
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RegisterEmployeeAsync(EmployeeInputModel input)
        {
            try
            {
                if (await _employeeRepository.GetEmployeeAsync(input.Identification) != null)
                    throw new ConflictException($"There's alredy an employee registered with the identification number {input.Identification}.");

                int imageID = 0;

                var employeeInput = new Employee
                {
                    FirstName = input.FirstName,
                    MiddleName = input.MiddleName,
                    LastName = input.LastName,
                    Identification = input.Identification,
                    Address = new Address
                    {
                        Zip = input.Address.Zip,
                        Street = input.Address.Street,
                        Number = input.Address.Number,
                        Comment = input.Address.Comment,
                        Neighborhood = input.Address.Neighborhood,
                        City = input.Address.City,
                        State = input.Address.State,
                        Country = input.Address.Country
                    },
                    User = new User
                    {
                        Username = input.UserInfo.Username,
                        Password = input.UserInfo.Password
                    },
                    Contact = new Contact
                    {
                        Email = input.Contact.Email,
                        Cellphone = input.Contact.Cellphone,
                        Phone = input.Contact.Phone
                    },
                    ExtraInfo = new ExtraInfo
                    {
                        AccessLevelID = input.ExtraInfo.AccessLevelID,
                        Salary = input.ExtraInfo.Salary,
                        JobID = input.ExtraInfo.JobID
                    },
                    Image = new Image
                    {
                        IsImage = input.Image.IsImage,
                        Base64 = input.Image.Base64
                    }
                };

                var addressID = await _addressRepository.InsertAddressAsync(employeeInput.Address);

                if (addressID != 0)
                    throw new Exception("An error ocurred while inserting the employee's address data.");

                var contactID = await _contactRepository.InsertContactAsync(employeeInput.Contact);

                if (contactID != 0)
                    throw new Exception("An error ocurred while inserting the employee's contact data.");

                if (input.Image.IsImage)
                    imageID = await _imageRepository.InsertImageAsync(employeeInput.Image.Base64);

                employeeInput.Address.ID = addressID;
                employeeInput.Contact.ID = contactID;
                employeeInput.Image.ID = imageID;

                await _employeeRepository.InsertEmployeeAsync(employeeInput);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
