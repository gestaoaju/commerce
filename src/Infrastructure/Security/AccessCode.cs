// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Linq;

namespace Gestaoaju.Infrastructure.Security
{
    public class AccessCode
    {
        private byte[] time;
        private byte[] key;

        public AccessCode()
        {
            time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            key = Guid.NewGuid().ToByteArray();
        }

        public byte[] ToArray() => key.Concat(time).ToArray();

        public override string ToString()
        {
            return BitConverter.ToString(ToArray()).Replace("-", string.Empty).ToLower();
        }
    }
}
