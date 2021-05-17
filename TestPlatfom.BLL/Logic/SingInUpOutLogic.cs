using Microsoft.AspNetCore.Identity;
using TestPlatform.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPlatform.DAL;
using TestPlatform.DAL.Identity;
using TestPlatform.DAL.Enums;

namespace TestPlatform.BLL.Services
{
    public class SignInUpOutLogic : ISignInUpOutLogic
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public SignInUpOutLogic(
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Out()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<bool> In(SignInModel signIn)
        {
            var res = await _signInManager.PasswordSignInAsync(signIn.Login, signIn.Password, signIn.Remember, false);
            return res.Succeeded;
        }
        public async Task<bool> Up(SignUpModel signUp)
        {
            var addNewUser = new User
            {
                Name = signUp.Name,
                SurName = signUp.SurName,
                Email = signUp.Email,
                UserName = signUp.Login,
                EmailConfirmed = true,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(addNewUser, signUp.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(addNewUser, Roles.Student.ToString());
                await _signInManager.SignInAsync(addNewUser, isPersistent: false);
            }
            return result.Succeeded;
        }
    }
}
