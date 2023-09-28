
using Framework.core.comman;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Business.comman
{
    public class CurrentUserService : ICurrentUserService
    {

        public IHttpContextAccessor _httpContext;
        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }


        public long? CurrentUserId
        {
            get
            {
                if (this._httpContext != null && this._httpContext.HttpContext != null)
                {
                    Microsoft.Extensions.Primitives.StringValues authorization;

                    if (this._httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authorization))
                    {
                        var token = authorization.ToString().Split(" ")[1];
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                        if (jwtToken == null)
                            return null;

                        string id = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                   
                        return long.Parse(id);
                    }
                }
                return null;
            }
        }

    }
}

