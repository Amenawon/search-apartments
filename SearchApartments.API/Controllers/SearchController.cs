using Microsoft.AspNetCore.Mvc;
using SearchApartments.Core.Interfaces;
using SearchApartments.Core.Models.APIRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApartments.API.Controllers
{
    [ApiController]
    public class SearchController : Controller
    {
        readonly ISearchDataService _searchDataService;
        public SearchController(ISearchDataService searchDataService)
        {
            _searchDataService = searchDataService; 
        }

        [HttpGet("data")]
        public async Task<IActionResult> Data([FromQuery] string searchPhrase, string market, int limit=25)
        {
             var response = await _searchDataService.SearchData(searchPhrase, market, limit);

            return Ok(response);
        }
    }
}
