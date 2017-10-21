/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;

namespace Gestaoaju.Models.EntityModel.Commercial.Partners
{
    public class Partner : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public string Homepage { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string GooglePlus { get; set; }
        public Tenant Tenant { get; set; }
    }
}
