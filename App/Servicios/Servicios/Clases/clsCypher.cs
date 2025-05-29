using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Servicios.Clases
{
    public class clsCypher
    {
        public string Password { get; set; } //Propiedad de entrada, el que recibre los datos
        public string PasswordCifrado { get; set; } // Propiedad que es una respuesta
        public string Salt { get; set; } //Propiedad de salida
        public bool CifrarClave()
        {
            byte[] saltBytes = GenerateSalt();
            // Hash the password with the salt
            PasswordCifrado = HashPassword(Password, saltBytes);
            Salt = Convert.ToBase64String(saltBytes);

            return true;
        }
        //Hash es el metodo que tiene el proceso para encriptar
        public string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())//utiliza el metodo SHA256, que hace el proceso de encripcion
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];//mezcla los dos contenidos

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
        static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())//genera una clase de criptografia
            {
                byte[] salt = new byte[16]; // Adjust the size based on your security requirements
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}