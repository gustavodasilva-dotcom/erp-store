using System;
using System.Collections.Generic;
using ERP.Store.Desktop.Repositories;
using ERP.Store.Desktop.Entities.JSON.Request;

namespace ERP.Store.Desktop.Services
{
    public class InventoryService
    {
        private readonly InventoryRepository _inventoryRepository;

        public InventoryService()
        {
            _inventoryRepository = new InventoryRepository();
        }

        public List<dynamic> Get(dynamic user)
        {
            try
            {
                return _inventoryRepository.Get(user);
            }
            catch (Exception) { throw; }
        }

        public dynamic Get(string identification, dynamic user)
        {
            try
            {
                return _inventoryRepository.Get(identification, user);
            }
            catch (Exception) { throw; }
        }

        public int Post(ItemRequest item, dynamic user)
        {
            try
            {
                var newItem = _inventoryRepository.Post(item, user);

                return newItem.itemID;
            }
            catch (Exception) { throw; }
        }
    }
}
