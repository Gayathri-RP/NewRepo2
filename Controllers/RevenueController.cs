using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using SalesProject_Backend.Models;

namespace SalesProject_Backend.DataAccess
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DataAccess _dataAccess;

        public SalesController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // POST: api/sales/revenue-by-product
        [HttpPost("revenue-by-product")]
        public IActionResult GetRevenueByProduct([FromBody] RevenueRequest request)
        {
            try
            {
                // Fetch data from DB
                List<ProductRevenue> productRevenue = _dataAccess.GetRevenueByProduct(request.StartDate, request.EndDate);
                return Ok(productRevenue);  
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
