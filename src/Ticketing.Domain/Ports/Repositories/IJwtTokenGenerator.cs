using Ticketing.Domain.Entities;

namespace Ticketing.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, IEnumerable<string> roles);
}
