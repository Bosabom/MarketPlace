using MarketPlaceService.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketPlaceService.API.Repositories
{
    public interface IMarketPlaceRepository
    {
        public Task <IEnumerable<MarketPlace>> GetAllAsync();
        
        public Task<MarketPlace> GetAsync(int id);
        
        public Task<MarketPlace> CreateAsync(MarketPlace item);
        
        public Task<MarketPlace> UpdateAsync(MarketPlace item);
        
        public Task<MarketPlace> DeleteAsync(MarketPlace item);
    }
}
