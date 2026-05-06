using LojaEcomerce.Interfaces;
using LojaEcomerce.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography.X509Certificates;
using BCrypt.Net;
using System.IO.Pipelines;
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

            var sql = " SELECT * FROM tb_Usuario WHERE Email= @email ";
            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", Email);
         

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string SenhaBanco = reader["Senha"].ToString();

                if (BCrypt.Net.BCrypt.Verify(Senha, SenhaBanco))
                {
                    return new LoginViewModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString()!,
                        Email = reader["Email"].ToString()!,
                        Nivel = reader["Nivel"].ToString()!

                    };
                }


            }
            return null;

        }

        public void CriarConta(LoginViewModel usuario)
        {
           
           using (var conn = new MySqlConnection(_connectionString))
            {
                //Criptografando a senha antes de enviar ao mysql(Banco)
                conn.Open();
                string senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                var sql = "Insert into tb_Usuario(Nome,Email,Senha,Nivel) VALUES(@nome,@email,@senha,@nivel)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", senhaHash);
                cmd.Parameters.AddWithValue("@nivel", "Usuario");
                cmd.ExecuteNonQuery();

            }
        }
            
        
    }

}
