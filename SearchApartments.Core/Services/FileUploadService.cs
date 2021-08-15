using SearchApartments.Core.Interfaces;
using SearchApartments.Core.Models;
using SearchApartments.Core.Models.APIRequests;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using SearchApartments.Core.Constants;
using Nest;
using SearchApartments.Core.Models.APIResponse;

namespace SearchApartments.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        readonly IElasticClient _elasticClient;

        public FileUploadService(IElasticClient elasticClient)
        {
             
            _elasticClient = elasticClient;
        }
        public async Task<IndexDataErrorResponse> Uploadfiles(FileUploadRequest files)
        {
        
            try
            {
                var propertyJson = MapJsonStringToModel<List<PropertyModel>>(files.PropertyFile.OpenReadStream());
                var managementJson = MapJsonStringToModel<List<ManagementModel>>(files.ManagementFile.OpenReadStream());
                var response = await IndexDataAsync(propertyJson, managementJson);

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        private T MapJsonStringToModel<T>(Stream stream)
        {

            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonString = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        private async Task<IndexDataErrorResponse> IndexDataAsync(List<PropertyModel> properties, List<ManagementModel> managements)
        {
            _elasticClient.Indices.Delete(FileType.Properties);
            _elasticClient.Indices.Delete(FileType.Managements);

            var bulkManagementRes = await _elasticClient.BulkAsync(b => b
            .Index(FileType.Managements)
            .IndexMany(managements));

            var bulkPropertyRes = await _elasticClient.BulkAsync(b => b
                .Index(FileType.Properties)
                .IndexMany(properties));

            var response = new IndexDataErrorResponse()
            {
                PropertyError = bulkPropertyRes.Errors,
                ManagementError = bulkManagementRes.Errors
            };

            return response;
        }
    }
}
