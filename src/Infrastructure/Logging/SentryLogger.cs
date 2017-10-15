/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SharpRaven.Core;
using SharpRaven.Core.Configuration;
using SharpRaven.Core.Data;

namespace Gestaoaju.Infrastructure.Logging
{
    public class SentryLogger : IErrorLogger
    {
        private RavenClient ravenClient;

        public SentryLogger(IOptions<RavenOptions> options)
        {
            ravenClient = new RavenClient(options);
        }
        
        public async Task LogAsync(Exception ex)
        {
            await ravenClient.CaptureAsync(new SentryEvent(ex));
        }

        public async Task LogAsync(string error)
        {
            await ravenClient.CaptureAsync(new SentryEvent(new SentryMessage(error)));
        }
    }
}
