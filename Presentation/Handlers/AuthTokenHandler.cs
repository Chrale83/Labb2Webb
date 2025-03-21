//using System.Net.Http.Headers;
//using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

//namespace Presentation.Handlers
//{
//    public class AuthTokenHandler : DelegatingHandler
//    {
//        private readonly ProtectedSessionStorage _sessionStorage;

//        public AuthTokenHandler(ProtectedSessionStorage sessionStorage)
//        {
//            _sessionStorage = sessionStorage;
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(
//            HttpRequestMessage request,
//            CancellationToken cancellationToken
//        )
//        {
//            var tokenResult = await _sessionStorage.GetAsync<string>("authToken");

//            if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value))
//            {
//                request.Headers.Authorization = new AuthenticationHeaderValue(
//                    "Bearer",
//                    tokenResult.Value
//                );
//            }

//            return await base.SendAsync(request, cancellationToken);
//        }
//    }
//}
