using WebUsuario.Infra.Data.Interfaces;
using WebUsuario.Infra.Data.Repositories;

namespace WebUsuario
{
    public class DependencyInjection
    {
        public static void Register(WebApplicationBuilder builder)
        {
            #region Capturar a connectionstring mapeada no arquivo appsettings.json

            var connectionString = builder.Configuration.GetConnectionString("WebUsuarioDB");

            #endregion

            #region Configurando a injeção de dependência para o repositorio

            builder.Services.AddTransient<IUsuarioRepository>(map => new UsuarioRepository(connectionString));

            #endregion
        }
    }
}
