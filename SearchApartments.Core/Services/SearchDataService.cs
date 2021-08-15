using Nest;
using SearchApartments.Core.Constants;
using SearchApartments.Core.Interfaces;
using SearchApartments.Core.Models;
using SearchApartments.Core.Models.APIRequests;
using SearchApartments.Core.Models.APIResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchApartments.Core.Services
{
  
    public class SearchDataService : ISearchDataService
    {
        readonly IElasticClient _elasticClient;
        public SearchDataService(IElasticClient elasticClient)
        {

            _elasticClient = elasticClient;
        }

        public async Task<List<SearchDataResponse>> SearchData(string searchPhrase, string market, int limit)
        {
            ISearchResponse<SearchDataResponse> response;

            try
            {
                if (!string.IsNullOrEmpty(market)) 
                {
                     response = await _elasticClient.SearchAsync<SearchDataResponse>(s => s
                        .Size(limit) 
                        .MinScore(0.1)
                        .Index($"{FileType.Properties},{FileType.Managements}")
                        .Query(q => q
                        .Bool(b => b.Filter(m => m.MultiMatch(mm => mm.Fields(f => f
                        .Field(Infer.Field<PropertyModel>(ff => ff.Property.Market))
                        .Field(Infer.Field<ManagementModel>(ff => ff.Mgmt.Market))).Query(market)))
                        .Should(s => s.MultiMatch(mm => mm.Fields(f => f
                        .Field(Infer.Field<PropertyModel>(ff => ff.Property.FormerName))
                        .Field(Infer.Field<PropertyModel>(ff => ff.Property.Name))
                        .Field(Infer.Field<ManagementModel>(ff => ff.Mgmt.Name)))
                        .Query(searchPhrase))))));

                }

                else 
                {
                   
                      response = await _elasticClient.SearchAsync<SearchDataResponse>(s => s
                        .Size(limit) 
                        .Index($"{FileType.Properties},{FileType.Managements}")
                        .Query(q => q
                        .MultiMatch(m => m
                        .Fields(f => f
                        .Field(Infer.Field<PropertyModel>(ff => ff.Property.FormerName))
                        .Field(Infer.Field<PropertyModel>(ff => ff.Property.Name))
                        .Field(Infer.Field<ManagementModel>(ff => ff.Mgmt.Name)))
                        .Query(searchPhrase)))); 
                }

            }
            catch (Exception)
            {

                throw;
            }
            return response?.Documents?.ToList();

        }
    }
}
