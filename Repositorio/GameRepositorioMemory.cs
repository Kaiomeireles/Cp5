using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;

namespace GameStoreMVC.Repositorio
{
    public class GameRepositorioMemory : IGameRepositorio
    {
        private static List<Game> _games = new();
        private static int _nextId = 1;

        public GameRepositorioMemory()
        {
            // Seed inicial para testes
            if (_games.Count == 0)
            {
                _games.Add(new Game
                {
                    Id = _nextId++,
                    Titulo = "The Witcher 3",
                    Descricao = "Um RPG de mundo aberto épico.",
                    Genero = "RPG",
                    Preco = 79.90m,
                    ImagemUrl = "https://image.api.playstation.com/vulcan/ap/disc/2112/CUSA00527_00/w2C4DD3LfYt7SRlu.png",
                    Plataforma = "PC",
                    DataLancamento = new DateTime(2015, 5, 19)
                });
                _games.Add(new Game
                {
                    Id = _nextId++,
                    Titulo = "Elden Ring",
                    Descricao = "Aventura de ação em mundo aberto.",
                    Genero = "Ação",
                    Preco = 199.90m,
                    ImagemUrl = "https://image.api.playstation.com/vulcan/ap/disc/2102/CUSA18779_00/0f7R6F2c0Cj8Q3kY.png",
                    Plataforma = "PS5",
                    DataLancamento = new DateTime(2022, 2, 25)
                });
                _games.Add(new Game
                {
                    Id = _nextId++,
                    Titulo = "Forza Horizon 5",
                    Descricao = "Corrida arcade em mundo aberto.",
                    Genero = "Corrida",
                    Preco = 249.90m,
                    ImagemUrl = "https://upload.wikimedia.org/wikipedia/pt/9/9f/Forza_Horizon_5_capa.jpg",
                    Plataforma = "Xbox",
                    DataLancamento = new DateTime(2021, 11, 9)
                });
            }
        }

        public List<Game> ListarTodos() => _games.OrderByDescending(g => g.Id).ToList();

        public Game? ObterPorId(int id) => _games.FirstOrDefault(g => g.Id == id);

        public void Adicionar(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
        }

        public void Atualizar(Game game)
        {
            var existente = _games.FirstOrDefault(g => g.Id == game.Id);
            if (existente != null)
            {
                existente.Titulo = game.Titulo;
                existente.Descricao = game.Descricao;
                existente.Genero = game.Genero;
                existente.Preco = game.Preco;
                existente.ImagemUrl = game.ImagemUrl;
                existente.Plataforma = game.Plataforma;
                existente.DataLancamento = game.DataLancamento;
            }
        }

        public void Deletar(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game != null) _games.Remove(game);
        }
    }
}
