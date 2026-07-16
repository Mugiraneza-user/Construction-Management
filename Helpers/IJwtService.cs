
using mks.Models;

namespace mks.Helpers
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}