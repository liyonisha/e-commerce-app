using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
