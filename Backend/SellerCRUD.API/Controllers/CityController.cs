using Microsoft.AspNetCore.Mvc;
using SellerCRUD.Services.DTOS;
using SellerCRUD.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityCRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: /City/Cities
        [HttpGet("Cities")]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            var result = await _cityService.GetCitiesAsync();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        // POST: /City/CreateCity
        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCityAsync([FromBody] CreateCityDto cityCreate)
        {
            var result = await _cityService.CreateCityAsync(cityCreate);

            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction("CreateCity", new { id = result.Entity.Id }, result);
        }

        // PUT: /City/37ca3e81-53c3-441f-9121-466ca40e930e
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCityAsync(int id, CityDto city)
        {
            var result = await _cityService.UpdateCityAsync(id, city);

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        // DELETE: api/City/{id}
        [HttpDelete("{ids}")]
        public async Task<IActionResult> DeleteCitiesAsync(string ids)
        {
            var idsToDelete = new List<int>();
            ids.Split(",").ToList().ForEach(x => { idsToDelete.Add(int.Parse(x)); });

            var result = await _cityService.DeleteCitiesAsync(idsToDelete);
            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
