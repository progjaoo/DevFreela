﻿using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevFreela.Coree.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infrastructure.Auth
{
    //Classe que implementa a interface criada IAuthService...
    public class AuthService : IAuthService
    {
        /*faço o construtor da IConfiguration que vai pegar as configurações
        lá em AppSettingsJson que fica na camada DevFreela.API*/
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //Compute Hash - retorna array para string
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Converte byte array para strin
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); //converte para HEXADECIMAL
                }
                return builder.ToString();
            }
        }
        //GERANDO O TOKEN JWT
        public string GenerateJwtToken(string email, string role)
        {
            //recuperando as 3 informações lá no AppSettings
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            /*aqui uso a Key junto com algoritmo de assinatura
             que vai usar o algoritmo de hashing que é o Sha256 */
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //alegando os usuários e suas informações...
            var claims = new List<Claim>
            {
                new Claim("userName", email),
                new Claim(ClaimTypes.Role, role) // qnd for enviar o token, vai buscar alegações do tipo Papel do usuario
            };

            //inicializando o Token com os parametros que passamos em cima...
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials,
                claims: claims);
            //criando a cadeia de caracteres
            var tokenHandler = new JwtSecurityTokenHandler();
            //token ja no formato string para retornar pro metodo GenerateJwtToken
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
