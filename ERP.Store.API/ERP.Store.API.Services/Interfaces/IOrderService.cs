﻿using System.Threading.Tasks;
using ERP.Store.API.Entities.Models.InputModel;

namespace ERP.Store.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task RegisterOrderAsync(OrderInputModel input);
    }
}
