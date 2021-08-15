using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApartments.Core.Models.APIResponse
{
    public class IndexDataErrorResponse
    {
        public bool ManagementError { get; set; }
        public bool PropertyError { get; set; }
    }
}
