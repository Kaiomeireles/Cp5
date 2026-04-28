using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using MySql.Data.MySqlClient;
using BCrypt.Net;

namespace GameStoreMVC.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexao")!;
        }

        public Usuario ValidarLogin(string email, string senha)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT * FROM Usuarios WHERE Email = @email";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string senhaDoBanco = reader["Senha"].ToString()!;

                        if (BCrypt.Net.BCrypt.Verify(senha, senhaDoBanco))
                        {
                            return new Usuario
                            {
                                Id = (int)reader["Id"],
                                Email = reader["Email"].ToString()!,
                                Cargo = reader["Cargo"].ToString()!
                            };
                        }
                    }
                }
            }
            return null!;
        }

        public void Cadastrar(Usuario usuario)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "INSERT INTO Usuarios (Email, Senha, Cargo) VALUES (@email, @senha, @cargo)";
                var cmd = new MySqlCommand(sql, conn);

                string senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", senhaHash);
                cmd.Parameters.AddWithValue("@cargo", usuario.Cargo ?? "Usuario");

                cmd.ExecuteNonQuery();
            }
        }
    }
}

