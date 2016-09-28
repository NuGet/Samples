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

    [Export(typeof(IVsCredentialProvider))]
    public sealed class SampleCredentialProvider
        : IVsCredentialProvider
    {
        /// <summary>
        /// Get credentials for the supplied package source Uri.
        /// </summary>
        /// <param name="uri">The NuGet package source Uri for which credentials are being requested. Implementors are
        /// expected to first determine if this is a package source for which they can supply credentials.
        /// If not, then Null should be returned.</param>
        /// <param name="proxy">Web proxy to use when comunicating on the network.  Null if there is no proxy
        /// authentication configured.</param>
        /// <param name="isProxyRequest">True if if this request is to get proxy authentication
        /// credentials. If the implementation is not valid for acquiring proxy credentials, then
        /// null should be returned.</param>
        /// <param name="isRetry">True if credentials were previously acquired for this uri, but
        /// the supplied credentials did not allow authorized access.</param>
        /// <param name="nonInteractive">If true, then interactive prompts must not be allowed.</param>
        /// <param name="cancellationToken">This cancellation token should be checked to determine if the
        /// operation requesting credentials has been cancelled.</param>
        /// <returns>Credentials acquired by this provider for the given package source uri.
        /// If the provider does not handle requests for the input parameter set, then null should be returned.
        /// If the provider does handle the request, but cannot supply credentials, an exception should be thrown.</returns>
        public async Task<ICredentials> GetCredentialsAsync(Uri uri, IWebProxy proxy, bool isProxyRequest, bool isRetry, bool nonInteractive, CancellationToken cancellationToken)
        {
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

        private Task<SampleCredentials> Authenticate(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            // todo: validate if the uri is a valid server uri

            // todo: actually authenticate against this specific server (e.g. OAuth flow, Federated Identity handshake)
            // Result of this operation should provide you with a username and secure (temporary) access token.

            const string accessToken = "secret";
            const string username = "username";

            var token = new SecureString();
            accessToken.ToList().ForEach(x => token.AppendChar(x));
            token.MakeReadOnly();

            var sampleCredentials = new SampleCredentials(username, token);

            return Task.FromResult(sampleCredentials);
        }
    }
}