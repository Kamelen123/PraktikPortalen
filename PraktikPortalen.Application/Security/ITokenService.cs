using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Application.Security
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
