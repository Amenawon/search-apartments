using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApartments.Core.Models.APIResponse
{
    public class SearchDataResponse {
        public Management Mgmt { get; set; }
        public Property Property { get; set; }
    }
}
