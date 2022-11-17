using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUsuario.Infra.Data.Entities;

namespace WebUsuario.Infra.Data.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario Get(string email);
        Usuario Get(string email, string senha);

        List<Usuario> GetAll(DateTime dataMin, DateTime dataMax, Guid idUsuario);
    }
}
