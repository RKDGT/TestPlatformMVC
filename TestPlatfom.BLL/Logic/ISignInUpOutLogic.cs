using System.Threading.Tasks;
using TestPlatform.BLL.DTO;

namespace TestPlatform.BLL.Services
{
    public interface ISignInUpOutLogic
    {
        Task<bool> In(SignInModel signIn);
        Task<bool> Up(SignUpModel signUp);

        Task Out();
    }
}
