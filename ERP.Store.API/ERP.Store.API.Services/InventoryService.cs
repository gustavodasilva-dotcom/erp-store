using System;
using System.Threading.Tasks;
using ERP.Store.API.CustomExceptions;
using ERP.Store.API.Entities.Entities;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Services.CustomExceptions;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.ViewModel.ItemViewModels;
using ERP.Store.API.Entities.Models.InputModel.ItemInputModels;

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

        public async Task<ItemViewModel> GetItemAsync(int itemID)
        {
            try
            {
                var item = await _inventoryRepository.GetItemAsync(itemID);

                if (item == null)
                    throw new NotFoundException($"There is not item registered with the id {itemID}.");

                var supplier = await _supplierRepository.GetSupplierByIDAsync(item.SupplierID);

                var category = await _inventoryRepository.GetCategoryByIDAsync(item.CategoryID);

                var image = await _imageRepository.GetItemsImage(itemID);

                return new ItemViewModel
                {
                    ItemID = item.ItemID,
                    Name = item.Name,
                    Price = item.Price,
                    Supplier = new Entities.Models.ViewModel.ItemViewModels.SupplierViewModel
                    {
                        Name = string.IsNullOrEmpty(supplier.Name) ? "" : supplier.Name,
                        Identification = string.IsNullOrEmpty(supplier.Identification) ? "" : supplier.Identification
                    },
                    Category = new CategoryViewModel
                    {
                        CategoryID = category == null ? 0 : category.CategoryID,
                        Description = category == null ? "" : category.Description
                    },
                    Image = new ImageViewModel
                    {
                        IsImage = string.IsNullOrEmpty(image.Base64) ? false : true,
                        Base64 = string.IsNullOrEmpty(image.Base64) ? "" : image.Base64
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
                        Description = input.Category.Description
                    },
                    Supplier = new Supplier
                    {
                        Identification = input.Supplier.Identification
                    },
                    Image = new Image
                    {
                        ID = 0,
                        IsImage = string.IsNullOrEmpty(input.Image.Base64) ? false : true,
                        Base64 = string.IsNullOrEmpty(input.Image.Base64) ? "" : input.Image.Base64
                    }
                };

                item.Category.ID = await _inventoryRepository.GetCategoryIDAsync(item.Category.Description);

                if (item.Category.ID == 0)
                    throw new BadRequestException("The category informed is invalid.");

                if (await _supplierRepository.GetSupplierAsync(item.Supplier.Identification) == null)
                    throw new NotFoundException($"The identification {item.Supplier.Identification} does not correspond to a supplier.");

                if (input.Image.IsImage)
                    item.Image.ID = await _imageRepository.InsertImageAsync(item.Image.Base64);

                return await _inventoryRepository.InsertItemAsync(item);
            }
            catch (Exception) { throw; }
        }
    }
}
