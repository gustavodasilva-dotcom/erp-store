using System;
using System.Threading.Tasks;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IImageRepository _imageRepository;

        private readonly IAddressRepository _addressRepository;

        private readonly IContactRepository _contactRepository;

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IImageRepository imageRepository, IAddressRepository addressRepository, IContactRepository contactRepository, IEmployeeRepository employeeRepository)
        {
            _imageRepository = imageRepository;

            _addressRepository = addressRepository;

            _contactRepository = contactRepository;

            _employeeRepository = employeeRepository;
        }

        public async Task RegisterEmployeeAsync(EmployeeInputModel input)
        {
            try
            {
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
                {
                    var contactID = await _contactRepository.InsertContactAsync(employeeInput.Contact);

                    if (contactID != 0)
                    {
                        if (input.Image.IsImage)
                            imageID = await _imageRepository.InsertImageAsync(employeeInput.Image.Base64);

                        employeeInput.Address.ID = addressID;
                        employeeInput.Contact.ID = contactID;
                        employeeInput.Image.ID = imageID;

                        await _employeeRepository.InsertEmployeeAsync(employeeInput);
                    }
                    else
                    {
                        throw new Exception("An error ocurred while inserting the employee's contact data.");
                    }
                }
                else
                {
                    throw new Exception("An error ocurred while inserting the employee's address data.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
