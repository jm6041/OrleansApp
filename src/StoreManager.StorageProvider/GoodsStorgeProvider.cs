using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Storage;
using StoreManager.Models;
using System;
using System.Threading.Tasks;

namespace StoreManager.StorageProvider
{
    public class GoodsStorgeProvider : IStorageProvider
    {
        private ApplicationDbContext _context;

        public string Name => nameof(GoodsStorgeProvider);

        public ILogger Logger { get; set; }

        public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            return Task.CompletedTask;
        }

        public Task Close()
        {
            _context?.Dispose();
            return Task.CompletedTask;
        }

        public async Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            _context = providerRuntime.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Logger = providerRuntime.ServiceProvider.GetRequiredService<ILogger<GoodsStorgeProvider>>();
            await Task.CompletedTask;
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            Logger.LogInformation("获取商品信息");
            var goodsNo = grainReference.GetPrimaryKeyString();
            grainState.State = await _context.GoodsInfos.FirstOrDefaultAsync(o => o.GoodsNo == goodsNo);
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var model = grainState.State as GoodsInfo;
            var entity = await _context.GoodsInfos.FirstOrDefaultAsync(o => o.GoodsNo == model.GoodsNo);
            entity.Stock = model.Stock;
            await _context.SaveChangesAsync();
        }
    }
}
