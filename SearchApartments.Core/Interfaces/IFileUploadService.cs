using SearchApartments.Core.Models.APIRequests;
using SearchApartments.Core.Models.APIResponse;
using System.Threading.Tasks;

namespace SearchApartments.Core.Interfaces
{
    public interface IFileUploadService
    {
        Task<IndexDataErrorResponse> Uploadfiles(FileUploadRequest files);
    }
}
