using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ERP.Store.API.Services.Interfaces;
using ERP.Store.API.Entities.Entities.Enums;
using ERP.Store.API.Repositories.Interfaces;
using ERP.Store.API.Entities.Models.ViewModel;
using ERP.Store.API.Entities.Models.InputModel;
using ERP.Store.API.Entities.Models.InputModel.ItemInputModels;

namespace ERP.Store.API.Services
{
    public class ValidationService : IValidationService
    {
        private readonly ILogService _logService;

        private readonly IValidationRepository _validationRepository;

        public ValidationService(ILogService logService, IValidationRepository validationRepository)
        {
            _logService = logService;

            _validationRepository = validationRepository;
        }

        public async Task<ErrorViewModel> InitializingReturn(IEnumerable<string> validations, int statusCode)
        {
            try
            {
                return new ErrorViewModel
                {
                    StatusCode = statusCode,
                    Messages = validations.ToList()
                };
            }
            catch (Exception e)
            {
                await _logService.LogAsync(validations, e.Message, "InitializingReturn() : ValidationService");

                throw;
            }
        }

        public async Task<ErrorViewModel> InitializingReturn(string validation, int statusCode)
        {
            try
            {
                var errors = new List<string>
                {
                    validation
                };

                return new ErrorViewModel
                {
                    StatusCode = statusCode,
                    Messages = errors
                };
            }
            catch (Exception e)
            {
                await _logService.LogAsync(validation, e.Message, "InitializingReturn() : ValidationService");

                throw;
            }
        }

        public async Task<IEnumerable<string>> Validate(dynamic model, EntityType type)
        {
            try
            {
                var errors = new List<string>();

                if (type == EntityType.Clients)
                {
                    errors = ValidateClient(model);
                }
                else
                {
                    if (type == EntityType.Employees)
                    {
                        errors = await ValidateEmployee(model);
                    }
                    else
                    {
                        if (type == EntityType.Users)
                        {
                            errors = ValidateUser(model);
                        }
                        else
                        {
                            if (type == EntityType.Suppliers)
                            {
                                errors = ValidateSupplier(model);
                            }
                            else
                            {
                                if (type == EntityType.Items)
                                {
                                    errors = ValidateItems(model);
                                }
                            }
                        }
                    }
                }

                return errors;
            }
            catch (Exception e)
            {
                await _logService.LogAsync(model, e.Message, "Validate() : ValidationService");

                throw;
            }
        }

        private List<string> ValidateClient(dynamic model)
        {
            try
            {
                var errors = new List<string>();

                var addressValidation = ValidateAddress(model.Address);

                var contactValidation = ValidateContact(model.Contact);

                var imageValidation = ValidateImage(model.Image);

                foreach (var validation in addressValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in contactValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in imageValidation)
                {
                    errors.Add(validation);
                }

                return errors;
            }
            catch (Exception) { throw; }
        }

        private async Task<List<string>> ValidateEmployee(dynamic model)
        {
            try
            {
                var errors = new List<string>();

                var addressValidation = ValidateAddress(model.Address);

                var contactValidation = ValidateContact(model.Contact);

                var extraInfoValidation = await ValidateExtraInfo(model.ExtraInfo);

                var userInfoValidation = ValidateUserInfo(model.UserInfo);

                var imageValidation = ValidateImage(model.Image);

                foreach (var validation in addressValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in contactValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in extraInfoValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in userInfoValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in imageValidation)
                {
                    errors.Add(validation);
                }

                return errors;
            }
            catch (Exception) { throw; }
        }

        private List<string> ValidateUser(dynamic model)
        {
            try
            {
                var errors = new List<string>();

                var userInfoValidation = ValidateUserInfo(model.UserInfo);

                foreach (var validation in userInfoValidation)
                {
                    errors.Add(validation);
                }

                return errors;
            }
            catch (Exception) { throw; }
        }

        public List<string> ValidateSupplier(dynamic model)
        {
            try
            {
                var errors = new List<string>();

                var addressValidation = ValidateAddress(model.Address);

                var contactValidation = ValidateContact(model.Contact);

                foreach (var validation in addressValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in contactValidation)
                {
                    errors.Add(validation);
                }

                return errors;
            }
            catch (Exception) { throw; }
        }

        public List<string> ValidateItems(dynamic model)
        {
            try
            {
                var errors = new List<string>();

                var itemValidation = ValidateItem(model);

                var imageValidation = ValidateImage(model.Image);

                foreach (var validation in itemValidation)
                {
                    errors.Add(validation);
                }

                foreach (var validation in imageValidation)
                {
                    errors.Add(validation);
                }

                return errors;
            }
            catch (Exception) { throw; }
        }

        private static List<string> ValidateAddress(AddressInputModel address)
        {
            try
            {
                var messages = new List<string>();

                if (string.IsNullOrEmpty(address.Zip)) messages.Add("The zip code cannot be null or empty.");
                if (string.IsNullOrEmpty(address.Street)) messages.Add("The street name cannot be null or empty.");
                if (string.IsNullOrEmpty(address.Number)) messages.Add("The residential number cannot be null or empty.");
                if (string.IsNullOrEmpty(address.Neighborhood)) messages.Add("The neighborhood cannot be null or empty.");
                if (string.IsNullOrEmpty(address.City)) messages.Add("The city cannot be null or empty.");
                if (string.IsNullOrEmpty(address.State)) messages.Add("The state cannot be null or empty.");
                if (string.IsNullOrEmpty(address.Country)) messages.Add("The country cannot be null or empty.");

                if (!address.Zip.Length.Equals(8)) messages.Add("The zip code needs to have 8 numbers.");
                if (IsNumber(address.Zip)) messages.Add("The zip code needs to be numeric.");

                if (IsNumber(address.Number)) messages.Add("The residential number needs to be numeric.");

                if (!address.State.Length.Equals(2)) messages.Add("The state needs to have 2 characters.");

                return messages;
            }
            catch (Exception) { throw; }
        }

        private static List<string> ValidateContact(ContactInputModel contact)
        {
            try
            {
                var messages = new List<string>();

                if (string.IsNullOrEmpty(contact.Email)) messages.Add("The email cannot be null or empty.");
                if (string.IsNullOrEmpty(contact.Phone)) messages.Add("The phone cannot be null or empty.");

                if (contact.Email.Length > 50) messages.Add("The email's length cannot be greater than 50 characters.");
                if (contact.Phone.Length < 10 || contact.Cellphone.Length > 20) messages.Add("The phone's length cannot be fewer than 10 and greater than 20 characters.");

                if (IsNumber(contact.Phone)) messages.Add("The phone's number needs to be numeric.");

                if (!string.IsNullOrEmpty(contact.Cellphone))
                {
                    if (contact.Cellphone.Length < 11 || contact.Cellphone.Length > 20) messages.Add("The cell phone's length cannot be fewer than 11 and greater than 20 characters.");
                    if (IsNumber(contact.Cellphone)) messages.Add("The cell phone's number needs to be numeric.");
                }

                return messages;
            }
            catch (Exception) { throw; }
        }

        private async Task<List<string>> ValidateExtraInfo(ExtraInfoInputModel extraInfo)
        {
            try
            {
                var messages = new List<string>();

                if (!await _validationRepository.IsJob(extraInfo.JobID))
                    messages.Add($"{extraInfo.JobID} does not correspond to an actual job.");

                if (!await _validationRepository.IsAccessLevel(extraInfo.AccessLevelID))
                    messages.Add($"{extraInfo.AccessLevelID} does not correspond to an actual access level.");

                return messages;
            }
            catch (Exception) { throw; }
        }

        private static List<string> ValidateUserInfo(UserInfoInputModel userInfo)
        {
            try
            {
                var messages = new List<string>();

                if (string.IsNullOrEmpty(userInfo.Username)) messages.Add("The username cannot be null or empty.");
                if (string.IsNullOrEmpty(userInfo.Password)) messages.Add("The password cannot be null or empty.");

                return messages;
            }
            catch (Exception) { throw; }
        }

        private static List<string> ValidateImage(ImageInputModel image)
        {
            try
            {
                var messages = new List<string>();

                if (string.IsNullOrEmpty(image.Base64) && image.IsImage) messages.Add("IsImage cannot be true if there is not base64.");
                if (!string.IsNullOrEmpty(image.Base64) && !image.IsImage) messages.Add("IsImage cannot be false if there is base64.");

                if (!Base64IsValid(image.Base64)) messages.Add("The base64 is not valid.");

                return messages;
            }
            catch (Exception) { throw; }
        }

        private static List<string> ValidateItem(ItemDataInputModel item)
        {
            try
            {
                var messages = new List<string>();

                if (string.IsNullOrEmpty(item.Name)) messages.Add("The item's name cannot be null or empty.");
                if (string.IsNullOrEmpty(item.Category.Description)) messages.Add("The item's category cannot be null or empty.");
                if (string.IsNullOrEmpty(item.Supplier.Identification)) messages.Add("The supplier's identification cannot be null or empty.");

                return messages;
            }
            catch (Exception) { throw; }
        }

        private static bool IsNumber(object number)
        {
            try
            {
                return (!Regex.IsMatch(number.ToString(), @"^\d+$"));
            }
            catch (Exception) { throw; }
        }

        private static bool Base64IsValid(string base64)
        {
            try
            {
                var buffer = new Span<byte>(new byte[base64.Length]);

                return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
            }
            catch (Exception) { throw; }
        }
    }
}
