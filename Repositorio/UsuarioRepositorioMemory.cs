using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using BCrypt.Net;

namespace GameStoreMVC.Repositorio
{
    public class UsuarioRepositorioMemory : IUsuarioRepositorio
    {
        private static List<Usuario> _usuarios = new();

        public UsuarioRepositorioMemory()
        {
            // Seed: admin e usuario de teste (senha: 123456)
            if (_usuarios.Count == 0)
            {
                _usuarios.Add(new Usuario
                {
                    Id = 1,
                    Email = "admin@gamestore.com",
                    Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Cargo = "Admin"
                });
                _usuarios.Add(new Usuario
                {
                    Id = 2,
                    Email = "user@gamestore.com",
                    Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Cargo = "Usuario"
                });
            }
        }

        public Usuario ValidarLogin(string email, string senha)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario != null && BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                return new Usuario
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Cargo = usuario.Cargo
                };
            }
            return null!;
        }

        public void Cadastrar(Usuario usuario)
        {
            usuario.Id = _usuarios.Count > 0 ? _usuarios.Max(u => u.Id) + 1 : 1;
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            usuario.Cargo ??= "Usuario";
            _usuarios.Add(usuario);
        }
    }
}
