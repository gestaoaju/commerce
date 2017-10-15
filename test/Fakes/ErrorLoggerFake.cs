/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Logging;
using System;
using System.Threading.Tasks;

namespace Gestaoaju.Fakes
{
    public class ErrorLoggerFake : IErrorLogger
    {
        public bool ErrorLogged { get; private set; }

        public Task LogAsync(Exception ex)
        {
            throw ex;
        }

        public Task LogAsync(string error)
        {
            ErrorLogged = true;
            return Task.CompletedTask;
        }
    }
}
