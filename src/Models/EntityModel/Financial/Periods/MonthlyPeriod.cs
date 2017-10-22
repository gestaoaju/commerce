/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Financial.Periods
{
    public class MonthlyPeriod : DailyPeriod, IEnumerable<DateTime>, IEnumerator<DateTime>
    {
        public MonthlyPeriod(DateTime startDate, DateTime endDate)
            : base(startDate, endDate) { }

        bool IEnumerator.MoveNext()
        {
            Current = Current == DateTime.MinValue ? StartDate : Current.AddMonths(1);
            return Current <= EndDate;
        }
    }
}
