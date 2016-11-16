using System;
using System.Net;
using System.Security;

namespace NuGet.Samples.VisualStudioCredentialProvider
{
    public sealed class SampleCredentials
        : ICredentials
    {
        private readonly string _username;
        private readonly SecureString _token;

        internal SampleCredentials(string username, SecureString token)
        {
            _username = username;
            _token = token;
        }

        public NetworkCredential GetCredential(Uri uri, string authType)
        {
            return new NetworkCredential(_username, _token);
        }
    }
}