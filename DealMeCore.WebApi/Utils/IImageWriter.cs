using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DealMeCore.WebApi.Utils
{
    /// <summary>
    /// IImageWriter.
    /// </summary>
    public interface IImageWriter
    {
        /// <summary>
        /// Upload Image 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<string> UploadImage(IFormFile file);
    }
}
