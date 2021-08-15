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
    public class FilesController : ControllerBase
    {
        readonly IFileUploadService _fileUploadService;
        public FilesController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("files")]
        public async Task<IActionResult> Files([FromForm] FileUploadRequest fileUploadRequest)
        {
            var response = await _fileUploadService.Uploadfiles(fileUploadRequest);
            
            return Ok(response);
        }
    }
}
