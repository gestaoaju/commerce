/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

namespace Gestaoaju.Infrastructure.Tenancy
{
    public interface ITenantIdProvider
    {
         int CurrentId { get; }
    }
}
