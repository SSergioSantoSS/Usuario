using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUsuario.Infra.Data.Entities;
using WebUsuario.Infra.Data.Interfaces;
using WebUsuario.Models;
using Newtonsoft.Json;

namespace WebUsuario.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model, [FromServices] IUsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {                     
                                         
                    var usuario = usuarioRepository.Get(model.Email, model.Senha);
                    

                    if (usuario != null)
                    {

                        AutenticarUsuario(usuario);

                        TempData["MensagemSucesso"] = $"Seja bem vindo { usuario.Nome}, seu acesso foi realizado com sucesso.";
                        

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                        TempData["MensagemErro"] = "Acesso negado, usuário não identificado.";
                    
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";

                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model, [FromServices] IUsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (usuarioRepository.Get(model.Email) != null)
                    {
                        TempData["MensagemErro"] = "O email informado já está cadastrado, por favor tente outro."; 
                        
                        return View();
                    }

                    var usuario = new Usuario()
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,  
                        Senha = model.Senha,
                        CPF = model.Cpf,
                        Ultimo_Acesso = DateTime.Now,
                        BL_Ativo = 1                       
                                                                                                                                      

                    };                   

                    usuarioRepository.Add(usuario);

                    TempData["MensagemSucesso"] = $"Parabéns {usuario.Nome}, seu cadastro foi realizado com sucesso!";

                    ModelState.Clear();

                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {       
            
            EncerrarSessao();
            
            return RedirectToAction("Login");
        }

        private void AutenticarUsuario(Usuario usuario)
        {

            var identificacao = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString()) },
            CookieAuthenticationDefaults.AuthenticationScheme);

            var claimPrincipal = new ClaimsPrincipal(identificacao);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

            //armazenar os dados do usuário em sessão
            var model = new UsuarioModel
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Ultimo_Acesso = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Qnt_Erro_Login = usuario.Qnt_Erro_Login,
                BL_Ativo = usuario.BL_Ativo

            };

            HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(model));

        }
        private void EncerrarSessao()
        {
            
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
    }
}



 