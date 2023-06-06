using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CursoAngular.Repository.Users
{
    public interface IUsersRepository
    {
        Task<IdentityResult> SignInAsync(string name, string email, string password);
        Task<SignInResult> LogInAsync(IdentityUser user, string password);
        Task<IList<Claim>> GetClaimsAsync(IdentityUser user);
        Task<IdentityUser> GetUserAsync(string email);
    }
}
