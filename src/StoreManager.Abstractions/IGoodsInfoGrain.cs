using Orleans;
using StoreManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManager.Abstractions
{
    public interface IGoodsInfoGrain : IGrainWithStringKey
    {
        Task<List<GoodsInfo>> GetAllGoods();
        Task<bool> BuyGoods(int count, string buyerUser);
    }
}
