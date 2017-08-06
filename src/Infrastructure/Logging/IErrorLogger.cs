// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Threading.Tasks;

namespace Gestaoaju.Infrastructure.Logging
{
    public interface IErrorLogger
    {
        Task LogAsync(Exception ex);
        Task LogAsync(string error);
    }
}