using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prueba_SUPERINTENDENCIA_DE_BANCOS.Methods
{
    public class GoogleOAuthHelper
    {

        private static readonly string[] Scopes = { DriveService.Scope.Drive };

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _applicationName;

        public GoogleOAuthHelper(string clientId, string clientSecret, string applicationName)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _applicationName = applicationName;
        }

        public async Task<DriveService> AuthenticateAsync()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("token.json", true));
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName
            });

            return service;
        }



    }
}
