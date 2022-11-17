using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUsuario.Infra.Data.Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? Senha { get; set; }
        public DateTime Ultimo_Acesso { get; set; }
        public int Qnt_Erro_Login { get; set; }
        public int BL_Ativo { get; set; }
    }
}


/*Colunas: CODIGO(IDENTITY), NOME(VARCHAR(50)), LOGIN
(VARCHAR(20)), CPF(VARCHAR(14)),SENHA(VARCHAR(20)),
ULTIMO_ACESSO(DATETIME), QTD_ERRO_LOGIN(INT), BL_ATIVO(BIT) */