using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUsuario.Infra.Data.Entities;
using WebUsuario.Infra.Data.Interfaces;
using WebUsuario.Infra.Data.Repositories;

namespace WebUsuario.Models
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public IActionResult MinhaConta()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(UsuarioCadastroModel model, [FromServices] IUsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    var usuario = new Usuario()
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        CPF = model.Cpf,
                        Ultimo_Acesso = DateTime.Now,
                        BL_Ativo = 1,  
                        


                    };

                    usuarioRepository.Add(usuario);                     

                    ModelState.Clear();

                    TempData["MensagemSucesso"] = $"Parabéns {usuario.Nome}, seu cadastro foi realizado com sucesso!";

                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
                }
            }
            return View();
        }

        
        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(UsuarioConsultaModel model, [FromServices] IUsuarioRepository usuarioRepository)
        {
            
            if (ModelState.IsValid)
            {
                try
                {                     

                    var dataMin = DateTime.Parse(model.DataMin);
                    var dataMax = DateTime.Parse(model.DataMax);
                    
                    model.Usuarios = usuarioRepository.GetAll(dataMin, dataMax, ObterUsuarioAutenticado().IdUsuario);

                    if (model.Usuarios != null && model.Usuarios.Count > 0) 

                        TempData["MensagemSucesso"] = $"A pesquisa obteve { model.Usuarios.Count}registro(s)."; 
                    
                    else

                        TempData["MensagemAlerta"] = "Nenhum registro foi encontrado para a pesquisa realizada.";
                    
                }

                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro: { e.Message}";
                
                }

            }

            return View(model);
        }
       
        private UsuarioModel ObterUsuarioAutenticado()
        {
            
            var json = HttpContext.Session.GetString("usuario");
            
            return JsonConvert.DeserializeObject<UsuarioModel>(json);
        }




    }
    }
