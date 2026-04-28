using GameStoreMVC.Models;

namespace GameStoreMVC.Interfaces
{
    public interface IGameRepositorio
    {
        List<Game> ListarTodos();
        Game? ObterPorId(int id);
        void Adicionar(Game game);
        void Atualizar(Game game);
        void Deletar(int id);
    }
}

