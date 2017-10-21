using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using System;

namespace Gestaoaju.Models.EntityModel.Financial.FixedExpenses
{
    public class FixedExpense : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Value { get; set; }
        public Tenant Tenant { get; set; }
        public Store Store { get; set; }
    }
}
