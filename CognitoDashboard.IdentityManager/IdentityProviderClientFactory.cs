namespace CognitoDashboard.IdentityManager
{
    public class IdentityProviderClientFactory
    {
        private readonly IIdentityProviderClient _client;
        private IIdentityProviderClient _proxy;

        public IdentityProviderClientFactory(IIdentityProviderClient client)
        {
            _client = client;
        }

        public IIdentityProviderClient Client
        {
            get
            {
                if (_proxy != null)
                    return _proxy;

                _proxy = IdentityProviderClientProxy<IIdentityProviderClient>.Create(_client);
                return _proxy;
            }
        }
    }
}
