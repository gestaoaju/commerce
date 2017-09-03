// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Threading.Tasks;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Tasks;

namespace Gestaoaju.Fakes
{
    public class TaskHandlerFake : ITaskHandler
    {
        public void ExecuteInBackground(Func<Task> action)
        {
            action.Invoke().GetAwaiter().GetResult();
        }
    }
}
