/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace Gestaoaju.Infrastructure.Security
{
    public class Sha256Hash
    {
        private string plainText;
        private string salt;

        public Sha256Hash(string plainText)
            : this(plainText, string.Empty) { }

        public Sha256Hash(string plainText, string salt)
        {
            this.plainText = plainText;
            this.salt = salt;
        }

        public override string ToString()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText + salt);

            using (var sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
