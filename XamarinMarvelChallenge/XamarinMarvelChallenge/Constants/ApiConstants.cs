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
        public const string Attribution = "Data provided by Marvel. © 2014 Marvel";
    }
}
