using Tickmill.Common.Types;

namespace Tickmill.Integrations.Bitly.Core.Exceptions
{
    internal sealed class UrlCannotBeInvalidException : CustomException
    {
        public UrlCannotBeInvalidException() : base("Url can not be invalid.")
        {
            
        }
    }

}
