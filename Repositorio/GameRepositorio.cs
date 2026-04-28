using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using MySql.Data.MySqlClient;

namespace GameStoreMVC.Repositorio
{
    public class GameRepositorio : IGameRepositorio
    {
        private readonly string _connectionString;

        public GameRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexao")!;
        }

        public List<Game> ListarTodos()
        {
            var games = new List<Game>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT * FROM Games ORDER BY Id DESC";
                var cmd = new MySqlCommand(sql, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game
                        {
                            Id = (int)reader["Id"],
                            Titulo = reader["Titulo"].ToString()!,
                            Descricao = reader["Descricao"]?.ToString(),
                            Genero = reader["Genero"].ToString()!,
                            Preco = Convert.ToDecimal(reader["Preco"]),
                            ImagemUrl = reader["ImagemUrl"]?.ToString(),
                            Plataforma = reader["Plataforma"].ToString()!,
                            DataLancamento = reader["DataLancamento"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataLancamento"])
                        });
                    }
                }
            }

            return games;
        }

        public Game? ObterPorId(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT * FROM Games WHERE Id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Game
                        {
                            Id = (int)reader["Id"],
                            Titulo = reader["Titulo"].ToString()!,
                            Descricao = reader["Descricao"]?.ToString(),
                            Genero = reader["Genero"].ToString()!,
                            Preco = Convert.ToDecimal(reader["Preco"]),
                            ImagemUrl = reader["ImagemUrl"]?.ToString(),
                            Plataforma = reader["Plataforma"].ToString()!,
                            DataLancamento = reader["DataLancamento"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataLancamento"])
                        };
                    }
                }
            }

            return null;
        }

        public void Adicionar(Game game)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = @"INSERT INTO Games (Titulo, Descricao, Genero, Preco, ImagemUrl, Plataforma, DataLancamento) 
                             VALUES (@titulo, @descricao, @genero, @preco, @imagemUrl, @plataforma, @dataLancamento)";

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@titulo", game.Titulo);
                cmd.Parameters.AddWithValue("@descricao", game.Descricao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@genero", game.Genero);
                cmd.Parameters.AddWithValue("@preco", game.Preco);
                cmd.Parameters.AddWithValue("@imagemUrl", game.ImagemUrl ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@plataforma", game.Plataforma);
                cmd.Parameters.AddWithValue("@dataLancamento", game.DataLancamento ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Game game)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = @"UPDATE Games SET 
                                Titulo = @titulo, 
                                Descricao = @descricao, 
                                Genero = @genero, 
                                Preco = @preco, 
                                ImagemUrl = @imagemUrl, 
                                Plataforma = @plataforma, 
                                DataLancamento = @dataLancamento 
                             WHERE Id = @id";

                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", game.Id);
                cmd.Parameters.AddWithValue("@titulo", game.Titulo);
                cmd.Parameters.AddWithValue("@descricao", game.Descricao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@genero", game.Genero);
                cmd.Parameters.AddWithValue("@preco", game.Preco);
                cmd.Parameters.AddWithValue("@imagemUrl", game.ImagemUrl ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@plataforma", game.Plataforma);
                cmd.Parameters.AddWithValue("@dataLancamento", game.DataLancamento ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "DELETE FROM Games WHERE Id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

