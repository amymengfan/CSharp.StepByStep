using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApp
{
    public class StreamController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> CreateMultipart()
        {
            #region Please implement the method

            /*
             * Please implement the method to retrive all the files data.
             */
            var uploadedFiles = new List<string>();

            await Request.Content
                .ReadAsMultipartAsync(new MultipartMemoryStreamProvider())
                .ContinueWith(async task =>
                    {
                        var provider = task.Result;
                        foreach (var content in provider.Contents)
                        {
                            var fileName = content.Headers.ContentDisposition.FileName;
                            var readString = await content.ReadAsStringAsync();

                            uploadedFiles.Add($"{fileName}:{readString}");
                        }
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion);

            return Request.CreateResponse(HttpStatusCode.OK, uploadedFiles);

            #endregion
        }
    }
}