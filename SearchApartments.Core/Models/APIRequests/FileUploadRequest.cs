using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SearchApartments.Core.Models.APIRequests
{
    public class FileUploadRequest
    {
        [Required(ErrorMessage ="Please upload a file")]
        public IFormFile PropertyFile { get; set; }
        [Required(ErrorMessage = "Please upload a file")]
        public IFormFile ManagementFile { get; set; }
    }
}
