using Microsoft.AspNetCore.Http;

namespace microkart.shared.Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _context;

        public UserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext?.User.FindFirst("sub")?.Value ?? "";
        }
    }
}
