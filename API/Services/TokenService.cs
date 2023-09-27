using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService
{
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            "OzSVHD7BFjagp4Icb1MmALZWqNG5Uids3vxuYX2JkECKtweRo0yMh1cWwZUpTgvP9AI0eE4r2aCtDzdQHbXG7loFkNfVsqxRiJ8jLwJ3nyZdRV29AfCl8ap6YhEce5t4xmbDX7krFuUNW10zIQSBHsvNMKjb0Hho5VXA638JucigFfCTypBeWLRq2mxEsldO1GD9a7kPLboM7kanPhqzcj2Imsg09AEt3wZ46VdSiep1vUBW8lTQXyCJ5F"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}