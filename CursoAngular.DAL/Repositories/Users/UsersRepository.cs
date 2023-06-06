using CursoAngular.Repository.Users;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CursoAngular.DAL.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CursoAngularDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersRepository(CursoAngularDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> SignInAsync(string name, string email, string password)
        {
            return await userManager.CreateAsync(new IdentityUser() { UserName = name, Email = email }, password);
        }

        public async Task<SignInResult> LogInAsync(IdentityUser user, string password)
        {
            return await signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public async Task<IdentityUser> GetUserAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            return await userManager.GetClaimsAsync(user);
        }
    }
}
