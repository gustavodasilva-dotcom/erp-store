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
    public class ClientService : IClientService
    {
        private readonly IImageRepository _imageRepository;

        private readonly IClientRepository _clientRepository;

        private readonly IAddressRepository _addressRepository;

        private readonly IContactRepository _contactRepository;

        public ClientService(IImageRepository imageRepository, IClientRepository clientRepository, IAddressRepository addressRepository, IContactRepository contactRepository)
        {
            _imageRepository = imageRepository;

            _clientRepository = clientRepository;

            _addressRepository = addressRepository;

            _contactRepository = contactRepository;
        }

        public async Task<ClientViewModel> GetClientAsync(string identification)
        {
            try
            {
                var clientData = await _clientRepository.GetClientAsync(identification);

                if (clientData == null)
                    throw new NotFoundException($"There's no client registered with the {identification} identification number.");

                var addressData = await _addressRepository.GetAddressAsync(clientData.AddressID);

                if (addressData == null)
                    throw new NotFoundException($"There's no address registered for the client.");

                var contactData = await _contactRepository.GetContactAsync(clientData.ContactID);

                if (contactData == null)
                    throw new NotFoundException($"There's no contact data registered for the client.");

                var imageData = await _imageRepository.GetClientsImage(clientData.ClientID);

                return new ClientViewModel
                {
                    ID = clientData.ClientID,
                    FirstName = clientData.FirstName,
                    MiddleName = clientData.MiddleName,
                    LastName = clientData.LastName,
                    Identification = clientData.Identification,
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
                    },
                    Image = new ImageViewModel
                    {
                        IsImage = imageData == null ? false : true,
                        Base64 = imageData == null ? string.Empty : imageData.Base64
                    }
                };
            }
            catch (Exception) { throw; }
        }

        public async Task RegisterClientAsync(ClientInputModel input)
        {
            try
            {
                if (await _clientRepository.GetClientAsync(input.Identification) != null)
                    throw new ConflictException($"There's alredy an employee registered with the identification number {input.Identification}.");

                int imageID = 0;

                var client = new Client
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
                    Contact = new Contact
                    {
                        Email = input.Contact.Email,
                        Cellphone = input.Contact.Cellphone,
                        Phone = input.Contact.Phone
                    },
                    Image = new Image
                    {
                        IsImage = input.Image.IsImage,
                        Base64 = input.Image.Base64
                    }
                };

                var addressID = await _addressRepository.InsertAddressAsync(client.Address);

                if (addressID == 0)
                    throw new Exception("An error ocurred while inserting the client's address data.");

                var contactID = await _contactRepository.InsertContactAsync(client.Contact);

                if (contactID == 0)
                    throw new Exception("An error ocurred while inserting the client's contact data.");

                if (input.Image.IsImage)
                    imageID = await _imageRepository.InsertImageAsync(client.Image.Base64);

                client.Address.ID = addressID;
                client.Contact.ID = contactID;
                client.Image.ID = imageID;

                await _clientRepository.InsertClientAsync(client);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateClientAsync(ClientInputModel input)
        {
            try
            {
                var clientInput = new Client
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
                    Contact = new Contact
                    {
                        Email = input.Contact.Email,
                        Cellphone = input.Contact.Cellphone,
                        Phone = input.Contact.Phone
                    },
                    Image = new Image
                    {
                        IsImage = input.Image.IsImage,
                        Base64 = input.Image.Base64
                    }
                };

                var clientData = await _clientRepository.GetClientAsync(input.Identification);

                if (clientData == null)
                    throw new ConflictException($"There's alredy an employee registered with the identification number {input.Identification}.");

                clientInput.ID = clientData.ClientID;
                clientInput.Address.ID = clientData.AddressID;
                clientInput.Contact.ID = clientData.ContactID;

                await _addressRepository.UpdateAddressAsync(clientInput.Address);

                await _contactRepository.UpdateContactAsync(clientInput.Contact);

                var clientImage = await _imageRepository.GetClientsImage(clientInput.ID);

                if (clientImage != null)
                {
                    clientInput.Image.ID = clientImage.ImageID;

                    await _imageRepository.UpdateImageAsync(clientInput.Image);
                }

                await _clientRepository.UpdateClientAsync(clientInput);
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> DeleteClientAsync(string identification)
        {
            try
            {
                var clientData = await _clientRepository.GetClientAsync(identification);

                if (clientData == null)
                    throw new NotFoundException($"There's no client registered with the {identification} identification number.");

                await _addressRepository.DeleteAddress(clientData.AddressID);

                await _contactRepository.DeleteContactAsync(clientData.ContactID);

                var clientImage = await _imageRepository.GetClientsImage(clientData.ClientID);

                if (clientImage != null)
                    await _imageRepository.DeleteImageAsync(clientImage.ImageID);

                await _clientRepository.DeleteClientAsync(clientData.ClientID);

                clientData = await _clientRepository.GetClientAsync(identification);

                if (clientData == null)
                    return true;

                return false;
            }
            catch (Exception) { throw; }
        }
    }
}
