using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApartments.Core.Models.APIRequests
{
    public class SearchDataRequest
    {   [Required(ErrorMessage ="Search phrase is required")]
        public string SearchPhrase { get; set; }
        public string Market { get; set; }
        public int Limit { get; set; } = 25;
    }
}
