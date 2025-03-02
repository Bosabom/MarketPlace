using MarketPlaceService.API.Models;
using MarketPlaceService.API.DataTransfer;
using MarketPlaceService.API.MappingProfiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlaceService.API.Repositories;
using AutoMapper;
using RedSail.KafkaWrapper;
using MarketPlaceService.API.Static_classes;

namespace MarketPlaceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketPlaceController : ControllerBase
    {
        private readonly IMarketPlaceRepository _repository;
        private readonly Mapper _mapper;
        private readonly IProducerWrapper _producer;
        private readonly MapperConfiguration _mapperConfig;

        public MarketPlaceController(IMarketPlaceRepository marketPlacerepository, IProducerWrapper producer)
        {
            _repository = marketPlacerepository;
            _producer = producer;
            _mapperConfig = new MapperConfiguration(config => config.AddProfile(new MarketPlaceMappingProfile()));
            _mapper = new Mapper(_mapperConfig);
        }

        // GET: api/<MarketPlaceController>
        [HttpGet]
        public async Task<IActionResult>Get()
        {
            IEnumerable<MarketPlaceDTO> marketPlaces = _mapper.Map<IEnumerable<MarketPlace>, IEnumerable<MarketPlaceDTO>>(await _repository.GetAllAsync());
            if (marketPlaces == null)
            {
                return NotFound();
            }
            return Ok(marketPlaces);
        }

        // GET api/<MarketPlaceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MarketPlaceDTO marketPlace = _mapper.Map<MarketPlace, MarketPlaceDTO>(await _repository.GetAsync(id));
            if (marketPlace == null)
            {
                return NotFound();
            }
            return Ok(marketPlace);
        }

        // POST api/<MarketPlaceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MarketPlaceCreateDTO marketPlaceCreateDto)
        {
            MarketPlaceDTO new_marketplaceDTO = _mapper.Map<MarketPlace, MarketPlaceDTO>(await _repository.CreateAsync(_mapper.Map<MarketPlaceCreateDTO, MarketPlace>(marketPlaceCreateDto)));
            if (new_marketplaceDTO == null)
            {
                return BadRequest();
            }

            // int id = new_marketPlace.MarketPlaceId;
            await _producer.SendAsync(new_marketplaceDTO, OperationCode.Create, KafkaTopics.producer);
            return Ok(new_marketplaceDTO);
        }

        // PUT api/<MarketPlaceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]MarketPlaceDTO marketPlaceDTO)
        {
            MarketPlaceDTO updated_marketplace = _mapper.Map<MarketPlace,MarketPlaceDTO>(await _repository.UpdateAsync(_mapper.Map<MarketPlaceDTO, MarketPlace>(marketPlaceDTO)));
            if (updated_marketplace == null)
            {
                return BadRequest();
            }
            await _producer.SendAsync(updated_marketplace, OperationCode.Update, KafkaTopics.producer);

            return Ok(updated_marketplace); 
        }

        // DELETE api/<MarketPlaceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            MarketPlaceDTO marketplaceDTO_for_delete = _mapper.Map<MarketPlace, MarketPlaceDTO>(await _repository.GetAsync(id));
            if (marketplaceDTO_for_delete == null)
            {
                return BadRequest();
            }

            var deleted_marketPlaceDTO = _mapper.Map<MarketPlace, MarketPlaceDTO>(await _repository.DeleteAsync(new MarketPlace { MarketPlaceId = id }));
            
            await _producer.SendAsync<object>(null, OperationCode.Delete, KafkaTopics.producer);

            return NoContent();
        }
    }
}
