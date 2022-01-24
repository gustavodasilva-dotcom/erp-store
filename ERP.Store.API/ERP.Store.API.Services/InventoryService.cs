using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ERP.Store.API.Entities.Tables;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IImageRepository _imageRepository;

        private readonly ISupplierRepository _supplierRepository;

        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IImageRepository imageRepository, ISupplierRepository supplierRepository, IInventoryRepository inventoryRepository)
        {
            _imageRepository = imageRepository;

            _supplierRepository = supplierRepository;

            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<dynamic>> GetCategories()
        {
            try
            {
                var categories = await _inventoryRepository.GetCategoryAsync();

                if (categories == null)
                    throw new NotFoundException("No categories were found.");

                return categories.Select(c => new
                {
                    c.CategoryID,
                    c.Description
                }).ToList();
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<dynamic>> GetShortListOfItemsAsync()
        {
            try
            {
                var items = await _inventoryRepository.GetShortListOfItemsAsync();

                if (items == null)
                    throw new NotFoundException("No items were found.");

                return items.Select(i => new
                {
                    i.ItemID,
                    i.Name,
                    i.Price,
                    i.Quantity
                }).ToList();
            }
            catch (Exception) { throw; }
        }

        public async Task<ItemViewModel> GetItemAsync(int itemID)
        {
            try
            {
                var item = await _inventoryRepository.GetItemAsync(itemID);

                if (item == null)
                    throw new NotFoundException($"There is not item registered with the id {itemID}.");

                var supplier = await _supplierRepository.GetSupplierAsync(item.SupplierID);

                var category = await _inventoryRepository.GetCategoryAsync(item.CategoryID);

                var inventory = await _inventoryRepository.GetInventoryAsync(item.ItemID);

                var image = await _imageRepository.GetItemsImageAsync(item.ItemID);

                return new ItemViewModel
                {
                    ItemID = item.ItemID,
                    Name = item.Name,
                    Price = item.Price,
                    Category = new CategoryViewModel
                    {
                        CategoryID = category == null ? 0 : category.CategoryID,
                        Description = category == null ? "" : category.Description
                    },
                    Inventory = new InventoryViewModel
                    {
                        Quantity = inventory.Quantity,
                        Supplier = new SupplierViewModel
                        {
                            ID = supplier.SupplierID,
                            Name = string.IsNullOrEmpty(supplier.Name) ? "" : supplier.Name,
                            Identification = string.IsNullOrEmpty(supplier.Identification) ? "" : supplier.Identification,
                            Address = null,
                            Contact = null
                        }
                    },
                    Image = new ImageViewModel
                    {
                        IsImage = image == null ? false : true,
                        Base64 = image == null ? "" : image.Base64
                    }
                };
            }
            catch (Exception) { throw; }
        }

        public async Task<int> RegisterItemAsync(ItemInputModel input)
        {
            try
            {
                var item = new Item
                {
                    Name = input.Name,
                    Price = input.Price,
                    Category = new Category
                    {
                        ID = input.Category.ID
                    },
                    Inventory = new Inventory
                    {
                        Quantity = input.Inventory.Quantity,
                        Supplier = new Supplier
                        {
                            Identification = input.Inventory.Supplier.Identification
                        }
                    },
                    Image = new Image
                    {
                        ID = 0,
                        IsImage = string.IsNullOrEmpty(input.Image.Base64) ? false : true,
                        Base64 = string.IsNullOrEmpty(input.Image.Base64) ? "" : input.Image.Base64
                    }
                };

                var category = await _inventoryRepository.GetCategoryAsync(item.Category.ID);

                if (category == null)
                    throw new BadRequestException("The category informed is invalid.");
                else
                    item.Category.ID = category.CategoryID;

                var supplier = await _supplierRepository.GetSupplierAsync(item.Inventory.Supplier.Identification);

                if (supplier == null)
                    throw new NotFoundException($"The identification {item.Inventory.Supplier.Identification} does not correspond to a supplier.");

                item.Inventory.Supplier.ID = supplier.SupplierID;

                if (input.Image.IsImage)
                    item.Image.ID = await _imageRepository.InsertImageAsync(item.Image.Base64);

                item.ID = await _inventoryRepository.InsertItemAsync(item);

                await _inventoryRepository.InsertInventoryAsync(item);

                return item.ID;
            }
            catch (Exception) { throw; }
        }

        public async Task<int> UpdateItemInventoryAsync(ItemInputModel input)
        {
            try
            {
                var item = await _inventoryRepository.GetItemAsync(input.ItemID);

                if (item == null)
                    throw new NotFoundException($"There is not item registered with the id {input.ItemID}.");

                var itemInput = new Item
                {
                    ID = input.ItemID,
                    Name = input.Name,
                    Price = input.Price,
                    Category = new Category
                    {
                        ID = input.Category.ID
                    },
                    Inventory = new Inventory
                    {
                        Quantity = input.Inventory.Quantity,
                        Supplier = new Supplier
                        {
                            Identification = input.Inventory.Supplier.Identification
                        }
                    },
                    Image = new Image
                    {
                        ID = 0,
                        IsImage = string.IsNullOrEmpty(input.Image.Base64) ? false : true,
                        Base64 = string.IsNullOrEmpty(input.Image.Base64) ? "" : input.Image.Base64
                    }
                };

                var category = await _inventoryRepository.GetCategoryAsync(itemInput.Category.ID);

                if (category == null)
                    throw new BadRequestException("The category informed is invalid.");
                else
                    itemInput.Category.ID = category.CategoryID;

                var supplier = await _supplierRepository.GetSupplierAsync(itemInput.Inventory.Supplier.Identification);

                if (supplier == null)
                    throw new NotFoundException($"The identification {itemInput.Inventory.Supplier.Identification} does not correspond to a supplier.");

                itemInput.Inventory.Supplier.ID = supplier.SupplierID;

                var image = await _imageRepository.GetItemsImageAsync(itemInput.ID);

                if (image != null) await _imageRepository.UpdateImageAsync(itemInput.Image);

                await _inventoryRepository.UpdateItemAsync(itemInput);

                await _inventoryRepository.UpdateInventoryAsync(itemInput);

                return itemInput.ID;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<string>> ValidateItemsAsync(List<Item> items)
        {
            try
            {
                var messages = new List<string>();

                foreach (var item in items)
                {
                    if (await _inventoryRepository.GetItemAsync(item.ID) == null)
                    {
                        messages.Add($"The id {item.ID} does not correspond to an actual item.");
                    }
                    else
                    {
                        if (!await IsAvailable(item)) messages.Add($"The item {item.ID} is not available.");
                    }
                }

                return messages;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<Order_ItemTable>> GetOrderItemsAsync(int orderID)
        {
            try
            {
                return await _inventoryRepository.GetOrderItemsAsync(orderID);
            }
            catch (Exception) { throw; }
        }

        private async Task<bool> IsAvailable(Item item)
        {
            try
            {
                return await _inventoryRepository.GetItemQuantityAsync(item.ID) > 0 ? true : false;
            }
            catch (Exception) { throw; }
        }
    }
}
