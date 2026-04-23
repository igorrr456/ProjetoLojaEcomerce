using LojaEcomerce.Interfaces;
using LojaEcomerce.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;

namespace LojaEcomerce.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // variavel somente leitura e privada para receber a conexao do banco
        private readonly string _connectionString;
        // construtor tem sempre o nome da class cabeça
        public UsuarioRepositorio(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Conexao");

        }
        //metodo para validar o login
        public LoginViewModel Validar(string Email, string Senha)
        {

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var sql = " SELECT * FROM tb_Usuario WHERE Email= @email AND Senha= @senha ";
            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@senha", Senha);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new LoginViewModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Nivel = reader["Nivel"].ToString()!
                };
            }
            return null;

        }
    }
}
