using Tickmill.Common.Types;

namespace Tickmill.Integrations.Bitly.Core.Exceptions
{
    internal sealed class UrlCannotBeEmptyException : CustomException
    {
        public UrlCannotBeEmptyException() : base("Url should not be empy.")
        {

        }
    }

}
