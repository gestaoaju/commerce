// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Threading.Tasks;
using Gestaoaju.Infrastructure.Logging;

namespace Gestaoaju.Infrastructure.Tasks
{
    public class TaskHandler : ITaskHandler
    {
        private IErrorLogger logger;

        public TaskHandler(IErrorLogger logger)
        {
            this.logger = logger;
        }

        public void Execute(Func<Task> action)
        {
            action.Invoke().ContinueWith(async task =>
            {
                if (task.IsFaulted)
                {
                    await logger.LogAsync(task.Exception);
                }
            });
        }
    }
}
