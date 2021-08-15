using SearchApartments.Core.Models.APIRequests;
using SearchApartments.Core.Models.APIResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApartments.Core.Interfaces
{
    public interface ISearchDataService
    {
        Task<List<SearchDataResponse>> SearchData(string searchPhrase, string market, int limit);
    }
}
