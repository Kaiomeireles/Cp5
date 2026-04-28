# 🎮 GameStoreMVC
 
Loja de Games desenvolvida em ASP.NET Core MVC com MySQL.
 
## 📋 Requisitos
 
- .NET 8.0 SDK
- MySQL Server 8.0+
 
## 🚀 Como Executar
 
1. Clone o repositório:
   ```bash
   git clone https://github.com/Kaiomeireles/Cp5.git
   ```
 
2. Configure a connection string no `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "Conexao": "Server=localhost;Database=gamestore;User=root;Password=SUA_SENHA;"
   }
   ```
 
3. Crie o banco de dados e execute o script SQL.
 
4. Rode o projeto:
   ```bash
   dotnet run
   ```
 
## 👤 Usuários de Teste
 
| Email | Senha | Cargo |
|-------|-------|-------|
| admin@gamestore.com | 123456 | Admin |
| user@gamestore.com | 123456 | Usuário |
 
## 🛡️ Segurança
 
- Senhas criptografadas com BCrypt
- Autenticação via Cookies e Claims
- Autorização por Roles (Admin/Usuario)
```
 
### 💻 Comandos Git:
 
```bash
git checkout -b feat/readme-instalacao
git add README.md
git commit -m "docs: adiciona README com instrucoes de instalacao"
git push origin feat/readme-instalacao
