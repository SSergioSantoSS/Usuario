namespace WebUsuario.Models
{
    public class UsuarioModel
    {
        public Guid IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }        
        public string? Ultimo_Acesso { get; set; }
        public int Qnt_Erro_Login { get; set; }
        public int BL_Ativo { get; set; }
    }
}
