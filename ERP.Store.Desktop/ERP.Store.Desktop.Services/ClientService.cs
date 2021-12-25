using System;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;
using ERP.Store.Desktop.Entities.JSON.Response;

namespace ERP.Store.Desktop.Services
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;

        public ClientService()
        {
            _clientRepository = new ClientRepository();
        }

        public int Post(ClientRequest client, UserResponse user)
        {
            try
            {
                return _clientRepository.Post(client, user);
            }
            catch (Exception) { throw; }
        }

        public int Put(ClientRequest client, UserResponse user)
        {
            try
            {
                return _clientRepository.Put(client, user);
            }
            catch (Exception) { throw; }
        }

        public ClientResponse Get(string identification, UserResponse user)
        {
            try
            {
                return _clientRepository.Get(identification, user);
            }
            catch (Exception) { throw; }
        }

        public int Delete(string identification, UserResponse user)
        {
            try
            {
                return _clientRepository.Delete(identification, user);
            }
            catch (Exception) { throw; }
        }
    }
}
