using LinkArava.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkArava.Services
{
    public class JWTService
    {
        public IConfiguration confing { get; set; }
        public JWTService(IConfiguration _confing) { _confing = confing; }
        public string genJWToken(UserModel user)
        { 
            // get key and exp from the confing
            string? key = confing.GetValue("JWT:key",string.Empty);
            int? exp = confing.GetValue("JWT:exp", 3);

            // create byte key
            SymmetricSecurityKey secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // create credantials
            SigningCredentials crd = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

            // create token payload (claim)
            Claim[] claims = new[]
            {
             new Claim("id", user.Id.ToString()),
             new Claim("userName", user.UserName)
            };

            // gen token from all the data
            JwtSecurityToken token = new JwtSecurityToken(
                expires : DateTime.Now.AddMinutes((double)exp),
                signingCredentials : crd,
                claims : claims
                );

            // get token string (write token)
            string tkn = new JwtSecurityTokenHandler().WriteToken(token);

            // return the token string
            return tkn;


        }
    }
}
