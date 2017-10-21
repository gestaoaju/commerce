/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;

namespace Gestaoaju.Models.EntityModel.Financial.CashActivities
{
    public class CashActivity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CashActivityType Activity { get; set; }
        public decimal Total { get; set; }
    }
}
