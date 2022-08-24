using Index_Bislat_Back.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Index_Bislat_Back.Helper
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ClaimService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public string GetJson()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.UserData);
            }
            return result;
        }


        public string CreateToken(string name)
        {
            List<Claim> claims = new List<Claim>
            {
              new Claim(ClaimTypes.UserData, name),
              //new Claim (ClaimTypes.Name, course.CourseNumber)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public bool CheakCorretjwt(string jwt, string body)
        {
            if (jwt == null)
                return false;
            if (!jwt.Equals(body))
                return false;
            return true;
        }

        public string CreateToken(object course)
        {
            List<Claim> claims = new List<Claim>
            {
              new Claim(ClaimTypes.UserData, Newtonsoft.Json.JsonConvert.SerializeObject(course)),
              //new Claim (ClaimTypes.Name, course.CourseNumber)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
