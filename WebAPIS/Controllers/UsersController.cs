using Entites.Entities;
using Entites.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPIS.Models;
using WebAPIS.Token;

namespace WebAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            //Verifica Senha/Email
            if (string.IsNullOrWhiteSpace(login.Senha) || string.IsNullOrWhiteSpace(login.Email))
            {
                return Unauthorized();
            }

            //Verifica Login Valido
            var resultado = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                //Busca Usuario
                var userCurrent = await _userManager.FindByEmailAsync(login.Email);

                //Gera Token Usuario
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JWTSecurityKey.Create("Secret_Key-1234568"))
                    .AddSubject("Empresa - Dev Net Core")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("idUsuario", userCurrent.Id)
                    .AddExpity(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuarioIdentity")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Senha) || string.IsNullOrWhiteSpace(login.Email))
            {
                return Ok("Falta alguns dados");
            }

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                CPF = login.CPF,
                Tipo = TipoUsuario.Comum
            };

            var resultado = await _userManager.CreateAsync(user, login.Senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            
            //Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //Retorno Email //Força a confirmação via e-mail
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);

            if (confirmEmail.Succeeded)
            {
                return Ok("Usuario Adiconado com sucesso");
            }
            else
            {
                return Ok("Erro ao confirmar usuario");
            }
        }
    }
}