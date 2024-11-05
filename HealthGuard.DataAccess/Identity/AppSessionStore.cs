using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Concurrent;

namespace HealthGuard.DataAccess.Identity
{
    public class AppSessionStore : ITicketStore
    {
        private ConcurrentDictionary<string, AuthenticationTicket> tickets = new();

        public Task RemoveAsync(string key)
        {
            if (tickets.ContainsKey(key))
            {
                tickets.TryRemove(key, out _);
            }

            return Task.FromResult(0);
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            tickets[key] = ticket;
            return Task.FromResult(false);
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            if (tickets.ContainsKey(key))
            {
                var ticket = tickets[key];
                return Task.FromResult(ticket);
            }
            else
            {
                return Task.FromResult((AuthenticationTicket)null!);
            }
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = Guid.NewGuid().ToString();
            var result = tickets.TryAdd(key, ticket);

            if (result)
            {
                return Task.FromResult(key);
            }
            else
            {
                throw new Exception("Failed to add entry to the MySessionStore.");
            }
        }
    }
}
