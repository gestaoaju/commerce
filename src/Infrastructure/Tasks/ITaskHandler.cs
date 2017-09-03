// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Threading.Tasks;

namespace Gestaoaju.Infrastructure.Tasks
{
    public interface ITaskHandler
    {
        void ExecuteInBackground(Func<Task> action);
    }
}
