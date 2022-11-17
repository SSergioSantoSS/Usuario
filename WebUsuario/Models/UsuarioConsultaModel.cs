using System.ComponentModel.DataAnnotations;
using WebUsuario.Infra.Data.Entities;

namespace WebUsuario.Models
{
    public class UsuarioConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataMax { get; set; }

        //[Required(ErrorMessage = "Por favor, selecione uma opção.")]
        //public string Ativo { get; set; }


        public List<Usuario>? Usuarios { get; set; }
    }
}
