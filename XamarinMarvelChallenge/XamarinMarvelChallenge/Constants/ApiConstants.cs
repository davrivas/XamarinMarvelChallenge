using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMarvelChallenge.Constants
{
    class ApiConstants
    {
        public readonly string ApiBaseEndpoint = ConfigurationManager.AppSettings["ApiBaseEndpoint"];
        public readonly string PublicKey = ConfigurationManager.AppSettings["PublicKey"];
        public readonly string PrivateKey = ConfigurationManager.AppSettings["PrivateKey"];
        public readonly string Attribution = $"Data provided by Marvel. © {DateTime.Now.Year} Marvel";
    }
}
