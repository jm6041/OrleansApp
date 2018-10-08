using Orleans;
using StoreManager.Abstractions;
using StoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orleans.Providers;

namespace StoreManager.Grains
{
    [StorageProvider(ProviderName = "GoodsStorgeProvider")]
    public class GoodsInfoGrain : Grain<GoodsInfo>, IGoodsInfoGrain
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public GoodsInfoGrain(ApplicationDbContext context, ILogger<GoodsInfoGrain> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<GoodsInfo>> GetAllGoods()
        {
            return await _context.GoodsInfos.ToListAsync();
        }

        public async Task<bool> BuyGoods(int count, string buyerUser)
        {
            _logger.LogInformation(buyerUser + ":购买商品--" + this.State.GoodsName + "    " + count + "个");

            if (count > 0 && this.State.Stock >= count)
            {
                this.State.Stock -= count;
                OrdersInfo ordersInfo = new OrdersInfo();
                ordersInfo.OrderNo = Guid.NewGuid().ToString("n");
                ordersInfo.BuyCount = count;
                ordersInfo.BuyerNo = buyerUser;
                ordersInfo.GoodsNo = this.State.GoodsNo;
                ordersInfo.InTime = DateTime.Now;
                _context.OrdersInfos.Add(ordersInfo);
                await _context.SaveChangesAsync();
                await this.WriteStateAsync();
                _logger.LogInformation("购买完成");
                return await Task.FromResult(true);
            }
            else
            {
                _logger.LogInformation("库存不足--剩余库存:" + this.State.Stock);
                return await Task.FromResult(false);
            }
        }
    }
}
