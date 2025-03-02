using MarketPlaceService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedSail.PostgreSQLWrapper.Interfaces;

namespace MarketPlaceService.API.Repositories
{
    public class MarketPlaceRepository : IMarketPlaceRepository
    {
        private readonly IPostgreSQLWrapper _postgre;
        
        public MarketPlaceRepository(IPostgreSQLWrapper postgre)
        {

            _postgre = postgre;
        }
        public async Task<IEnumerable<MarketPlace>> GetAllAsync()
        {
            return await _postgre.ExecuteAsync<IEnumerable<MarketPlace>, IEnumerable<MarketPlace>>("get_all_marketplaces", null);
        }

        public async Task<MarketPlace> GetAsync(int id)
        {
            return await _postgre.ExecuteAsync<MarketPlace,MarketPlace>("get_marketplace", new MarketPlace() { MarketPlaceId = id });
        }
        public async Task<MarketPlace> CreateAsync(MarketPlace marketplace)
        {
            return await _postgre.ExecuteAsync<MarketPlace, MarketPlace>("insert_marketplace", marketplace);
        }
        public async Task<MarketPlace> UpdateAsync(MarketPlace marketplace)
        {
            return await _postgre.ExecuteAsync<MarketPlace, MarketPlace>("update_marketplace", marketplace);
        }

        public async Task<MarketPlace> DeleteAsync(MarketPlace marketplace)
        {
            return await _postgre.ExecuteAsync<MarketPlace, MarketPlace>("delete_marketplace", marketplace);
        }

    }
}
