using Microsoft.AspNetCore.Mvc;
using SellerCRUD.Services.DTOS;
using SellerCRUD.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerCRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        // GET: /Seller/Sellers
        [HttpGet("Sellers")]
        public async Task<IActionResult> GetAllSellersAsync()
        {
            var result = await _sellerService.GetSellersAsync();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        // POST: /Seller/CreateSeller
        [HttpPost("CreateSeller")]
        public async Task<IActionResult> CreateSellerAsync([FromBody] CreateSellerDto sellerCreate)
        {
            var result = await _sellerService.CreateSellerAsync(sellerCreate);

            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction("CreateSeller", new { id = result.Entity.Id }, result);
        }

        // PUT: /Seller/37ca3e81-53c3-441f-9121-466ca40e930e
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSellerAsync(int id, SellerDto seller)
        {
            var result = await _sellerService.UpdateSellerAsync(id, seller);

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        // DELETE: api/Seller/{id}
        [HttpDelete("{ids}")]
        public async Task<IActionResult> DeleteSellersAsync(string ids)
        {
            var idsToDelete = new List<int>();
            ids.Split(",").ToList().ForEach(x => { idsToDelete.Add(int.Parse(x)); });

            var result = await _sellerService.DeleteSellersAsync(idsToDelete);
            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
