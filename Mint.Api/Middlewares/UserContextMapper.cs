using Microsoft.AspNetCore.Http;
using Mint.Business;
using System.Threading.Tasks;

namespace Mint.Api.Middlewares
{
    public class UserContextMapper
    {
        private const string HEADER_USER = "X-Mint-User";

        private const string HEADER_ACCOUNT = "X-Mint-Account";

        public RequestDelegate Next { get; }

        public UserContextMapper(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context, IServiceContext server)
        {
            var userId = 0;

            var accountId = 0;

            if (context.Request.Headers.ContainsKey(HEADER_USER) && context.Request.Headers[HEADER_USER].Count.Equals(1))
            {
                userId = int.Parse(context.Request.Headers[HEADER_USER][0]);

                if (!userId.Equals(0))
                    server.User = userId;
            }

            if (context.Request.Headers.ContainsKey(HEADER_ACCOUNT) && context.Request.Headers[HEADER_ACCOUNT].Count.Equals(1))
            {
                accountId = int.Parse(context.Request.Headers[HEADER_ACCOUNT][0]);

                if (!accountId.Equals(0))
                    server.Account = accountId;
            }

            await Next.Invoke(context);
        }
    }
}