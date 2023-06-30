using Tickmill.Common.Types;

namespace Tickmill.Integrations.Bitly.Core.Exceptions
{
    internal sealed class ServiceApiException : CustomException
    {
        public ServiceApiException(string error) : base($"Service's API has thrown an exception: {error}")
        {
        }
    }
}
