using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using NuGet.VisualStudio;

namespace NuGet.Samples.VisualStudioCredentialProvider
{
    // This is a sample credential provider to demonstrate implementing your own NuGet Credential Provider for NuGet.
    // This sample does not actually authenticate against any endpoint and just returns dummy results.
    [Export(typeof(IVsCredentialProvider))]
    public sealed class SampleCredentialProvider
        : IVsCredentialProvider
    {
        // As an example, this sample credential provider only supports www.nuget.org domain name.
        // When implementing your own provider, change this to the specific domain name you want to support.
        private const string _supportedTargetHost = "www.nuget.org";

        /// <summary>
        /// Get credentials for the supplied package source Uri.
        /// </summary>
        /// <param name="uri">The NuGet package source Uri for which credentials are being requested.</param>
        /// <param name="proxy">Web proxy to use when comunicating on the network. Null if there is no proxy
        /// authentication configured.</param>
        /// <param name="isProxyRequest">True if this request is to get proxy authentication
        /// credentials. If the credential provider does not support acquiring proxy credentials, then
        /// null should be returned.</param>
        /// <param name="isRetry">True if credentials were previously acquired for this uri, but
        /// the supplied credentials did not allow authorized access.</param>
        /// <param name="nonInteractive">If true, then interactive prompts must not be allowed.</param>
        /// <param name="cancellationToken">This cancellation token should be checked to determine if the
        /// operation requesting credentials has been cancelled.</param>
        /// <returns>
        /// Credentials acquired by this provider for the given package source uri.
        /// If the provider does not handle requests for the input parameter set, then null should be returned.
        /// If the provider does handle the request, but cannot supply credentials, an exception should be thrown.
        /// </returns>
        public async Task<ICredentials> GetCredentialsAsync(
            Uri uri,
            IWebProxy proxy,
            bool isProxyRequest,
            bool isRetry,
            bool nonInteractive,
            CancellationToken cancellationToken)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            // This sample credential provider doesn't handle getting proxy credentials.
            if (isProxyRequest)
            {
                return null;
            }

            // This sample credential provider does not support a relative Uri.
            if (!uri.IsAbsoluteUri)
            {
                return null;
            }

            // Force HTTPS
            if (string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.InvariantCultureIgnoreCase))
            {
                uri = ForceHttps(uri);
            }

            // This should be enforced HTTPS by now.
            // If not, the endpoint protocol is not supported by this sample credential provider.
            if (!string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            // Check if the target host is supported by this credential provider.
            // All credential providers must validate they support the target Uri before actually doing any authentication!
            if (!string.Equals(uri.Host, _supportedTargetHost, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            try
            {
                var credentials = await Authenticate(uri);

                return credentials;
            }
            catch (TaskCanceledException)
            {
                Trace.TraceError($"Credentials acquisition for server {uri} was cancelled by the user.");
                throw;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Credentials acquisition for server {uri} failed with error: {ex.Message}");
            }

            return null;
        }

        private static Uri ForceHttps(Uri uri)
        {
            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Scheme = Uri.UriSchemeHttps;

            // Ensure we preserve original port number if provided.
            var hadDefaultPort = uriBuilder.Uri.IsDefaultPort;
            uriBuilder.Port = hadDefaultPort ? -1 : uriBuilder.Port;

            return uriBuilder.Uri;
        }

        private static Task<SampleCredentials> Authenticate(Uri uri)
        {
            // This sample credential provider will randomly generate a new access token.
            // Replace this with actual authentication against the targeted Uri.
            // The result of this operation should provide you with a username and a secure (temporary) access token.
            var accessToken = Guid.NewGuid().ToString();
            const string username = "username";

            var token = new SecureString();
            accessToken.ToList().ForEach(x => token.AppendChar(x));
            token.MakeReadOnly();

            var sampleCredentials = new SampleCredentials(username, token);

            return Task.FromResult(sampleCredentials);
        }
    }
}