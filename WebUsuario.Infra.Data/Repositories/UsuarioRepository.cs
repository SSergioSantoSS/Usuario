using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUsuario.Infra.Data.Entities;
using WebUsuario.Infra.Data.Interfaces;

namespace WebUsuario.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
       
        private readonly string _connectionString;
        
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Usuario entity)
        {
            var query = @"
                INSERT INTO USUARIO(
                        IDUSUARIO,
                        NOME,
                        EMAIL,
                        CPF,
                        SENHA,
                        ULTIMO_ACESSO,
                        QNT_ERRO_LOGIN,
                        BL_ATIVO)
                 VALUES(
                        @IdUsuario,
                        @Nome,
                        @Email,
                        @Cpf,
                        CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                        @Ultimo_Acesso,
                        @Qnt_Erro_Login,
                        @Bl_Ativo)

            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }
        public void Update(Usuario entity)
        {
            var query = @"
                 UPDATE USUARIO
                     SET
                        NOME = @Nome,
                        EMAIL = @Email,
                        CPF = @Cpf,
                        SENHA = @Senha,
                        ULTIMO_ACESSO = @Ultimo_Acesso
                    WHERE
                        IDUSUARIO = @IdUsuario
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }
        public void Delete(Usuario entity)
        {
            var query = @"
                        DELETE FROM USUARIO
                  WHERE
                        IDUSUARIO = @IdUsuario
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }
        public List<Usuario> GetAll()
        {
            var query = @"
                        SELECT * FROM USUARIO
                ORDER BY 
                        NOME ASC
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query)
                .ToList();

            }
        }
        public Usuario Get(Guid id)
        {
            var query = @"
                        SELECT * FROM USUARIO
                   WHERE
                        IDUSUARIO = @id
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query, new { id })
                .FirstOrDefault();
            }
        }
        public Usuario Get(string email)
        {
            var query = @"
                        SELECT * FROM USUARIO
                   WHERE
                        EMAIL = @email
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query, new { email })
                .FirstOrDefault();
            }
        }
        public Usuario Get(string email, string senha)
        {
            var query = @"
                        SELECT * FROM USUARIO
                   WHERE
                        EMAIL = @email
                     AND
                        SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2)
            ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                .Query<Usuario>(query, new { email, senha })
                .FirstOrDefault();
            }
        }

        public List<Usuario> GetAll(DateTime dataMin, DateTime dataMax, Guid idUsuario)
        {
            var query = @"
                        SELECT * FROM USUARIO
                        WHERE
                             DATA BETWEEN @dataMin AND @dataMax
                        AND
                             IDUSUARIO = @idUsuario
                       
             ";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(query, new { dataMin, dataMax, idUsuario})

                .ToList();
            }
        }
       
    }

}
//AND
//  ATIVO = @ativo
                        
//  ORDER BY DATA