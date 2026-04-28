using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStoreMVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameRepositorio _gameRepositorio;

        public GameController(IGameRepositorio gameRepositorio)
        {
            _gameRepositorio = gameRepositorio;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var games = _gameRepositorio.ListarTodos();
            return View(games);
        }

        private bool IsAdmin()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value == "Admin";
        }

        public IActionResult Criar()
        {
            if (!IsAdmin())
            {
                TempData["Erro"] = "Acesso negado. Apenas administradores podem cadastrar games.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Game game)
        {
            if (!IsAdmin())
            {
                TempData["Erro"] = "Acesso negado. Apenas administradores podem cadastrar games.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(game);
            }

            _gameRepositorio.Adicionar(game);
            TempData["MensagemSucesso"] = "Game cadastrado com sucesso!";
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            if (!IsAdmin())
            {
                TempData["Erro"] = "Acesso negado. Apenas administradores podem editar games.";
                return RedirectToAction("Index");
            }

            var game = _gameRepositorio.ObterPorId(id);
            if (game == null)
            {
                TempData["Erro"] = "Game não encontrado.";
                return RedirectToAction("Index");
            }

            return View(game);
        }

        [HttpPost]
        public IActionResult Editar(Game game)
        {
            if (!IsAdmin())
            {
                TempData["Erro"] = "Acesso negado. Apenas administradores podem editar games.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(game);
            }

            _gameRepositorio.Atualizar(game);
            TempData["MensagemSucesso"] = "Game atualizado com sucesso!";
            return RedirectToAction("Index");
        }

        public IActionResult Deletar(int id)
        {
            if (!IsAdmin())
            {
                TempData["Erro"] = "Acesso negado. Apenas administradores podem excluir games.";
                return RedirectToAction("Index");
            }

            var game = _gameRepositorio.ObterPorId(id);
            if (game == null)
            {
                TempData["Erro"] = "Game não encontrado.";
                return RedirectToAction("Index");
            }

            return View(game);
        }

        [HttpPost, ActionName("DeletarConfirmado")]
        public IActionResult DeletarConfirmado(int id)
        {
            if (!IsAdmin())
            {
                TempData["Erro"] = "Acesso negado. Apenas administradores podem excluir games.";
                return RedirectToAction("Index");
            }

            _gameRepositorio.Deletar(id);
            TempData["MensagemSucesso"] = "Game excluído com sucesso!";
            return RedirectToAction("Index");
        }
    }
}

