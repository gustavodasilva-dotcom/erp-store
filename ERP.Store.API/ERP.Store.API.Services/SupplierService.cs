using System;
using System.Threading.Tasks;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IContactRepository _contactRepository;

        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(IAddressRepository addressRepository, IContactRepository contactRepository, ISupplierRepository supplierRepository)
        {
            _addressRepository = addressRepository;

            _contactRepository = contactRepository;

            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierViewModel> GetSupplierAsync(string identification)
        {
            try
            {
                var supplierData = await _supplierRepository.GetSupplierAsync(identification);

                if (supplierData == null)
                    throw new NotFoundException($"There's no supplier registered with the {identification} identification number.");

                var addressData = await _addressRepository.GetAddressAsync(supplierData.AddressID);

                if (addressData == null)
                    throw new NotFoundException($"There's no address registered for the supplier.");

                var contactData = await _contactRepository.GetContactAsync(supplierData.ContactID);

                if (contactData == null)
                    throw new NotFoundException($"There's no contact data registered for the supplier.");

                return new SupplierViewModel
                {
                    ID = supplierData.SupplierID,
                    Name = supplierData.Name,
                    Identification = supplierData.Identification,
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
                    Contact = new ContactViewModel
                    {
                        Email = contactData.Email,
                        Cellphone = contactData.Cellphone,
                        Phone = contactData.Phone
                    }
                };
            }
            catch (Exception) { throw; }
        }

        public async Task RegisterSupplierAsync(SupplierInputModel input)
        {
            try
            {
                if (await _supplierRepository.GetSupplierAsync(input.Identification) != null)
                    throw new ConflictException($"There's alredy a supplier registered with the identification number {input.Identification}.");

                var supplierInput = new Supplier
                {
                    Name = input.Name,
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
                    Contact = new Contact
                    {
                        Email = input.Contact.Email,
                        Cellphone = input.Contact.Cellphone,
                        Phone = input.Contact.Phone
                    }
                };

                var addressID = await _addressRepository.InsertAddressAsync(supplierInput.Address);

                if (addressID == 0)
                    throw new Exception("An error ocurred while inserting the supplier's address data.");

                var contactID = await _contactRepository.InsertContactAsync(supplierInput.Contact);

                if (contactID == 0)
                    throw new Exception("An error ocurred while inserting the supplier's contact data.");

                supplierInput.Address.ID = addressID;
                supplierInput.Contact.ID = contactID;

                await _supplierRepository.InsertSupplierAsync(supplierInput);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateSupplierAsync(SupplierInputModel input)
        {
            try
            {
                var supplierInput = new Supplier
                {
                    Name = input.Name,
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
                    Contact = new Contact
                    {
                        Email = input.Contact.Email,
                        Cellphone = input.Contact.Cellphone,
                        Phone = input.Contact.Phone
                    }
                };

                var supplierData = await _supplierRepository.GetSupplierAsync(input.Identification);

                if (supplierData == null)
                    throw new NotFoundException($"There's no supplier registered with the {supplierData.Identification} identification number.");

                supplierInput.ID = supplierData.SupplierID;
                supplierInput.Address.ID = supplierData.AddressID;
                supplierInput.Contact.ID = supplierData.ContactID;

                await _addressRepository.UpdateAddressAsync(supplierInput.Address);

                await _contactRepository.UpdateContactAsync(supplierInput.Contact);

                await _supplierRepository.UpdateSupplierAsync(supplierInput);
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> DeleteSupplierAsync(string identification)
        {
            try
            {
                var supplierData = await _supplierRepository.GetSupplierAsync(identification);

                if (supplierData == null)
                    throw new NotFoundException($"There's no supplier registered with the {identification} identification number.");

                await _addressRepository.DeleteAddress(supplierData.AddressID);

                await _contactRepository.DeleteContactAsync(supplierData.ContactID);

                await _supplierRepository.DeleteSupplierAsync(supplierData.SupplierID);

                supplierData = await _supplierRepository.GetSupplierAsync(identification);

                if (supplierData == null)
                    return true;

                return false;
            }
            catch (Exception) { throw; }
        }
    }
}
