using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using GrpcApplication.Model;
using Newtonsoft.Json.Linq;
using Supremes;
using Supremes.Nodes;

namespace GrpcApplication.Services.Impl
{
    public class ApplicationDetailsBuilder
    {
        private readonly Application _application;
        private readonly App _details;

        private Document _document;

        public ApplicationDetailsBuilder(Application application)
        {
            _application = application;
            _details = new App(_application.Url);
        }

        public async Task<ApplicationDetailsBuilder> GetDocument(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync(_application.Url);
            response.EnsureSuccessStatusCode();
            _document = response.Parse();
            
            return this;
        }

        public ApplicationDetailsBuilder ParseName()
        {
            _details.Name = _document?.GetElementsByTag("h1").Text ?? 
                            throw new ArgumentNullException("Document is not created, use method GetDocument()");
            return this;
        }

        public ApplicationDetailsBuilder ParseInstallCount()
        {
            string pattern = "\\{key: 'ds:5'.*?data:(.*?), sideChannel";
            var collection = Regex.Matches(_document.Data, pattern, 
                RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string jsonData = collection.FirstOrDefault()?.Groups[1].Value ?? 
                              throw new ArgumentNullException("Can't get data by pattern");
            JArray fullData = JArray.Parse(jsonData);

            var count = fullData[0][12]?[9]?[2]?.Value<long>() ??
                        throw new ArgumentNullException("Can't get count data");
            
            _details.InstallsCount = count;
            
            return this;
        }

        public ApplicationDetails Build()
        {
            return new ()
            {
                DownloadCount = _details.InstallsCount,
                Name = _details.Name
            };
        }
        
    }
}