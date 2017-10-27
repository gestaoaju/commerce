/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.CashActivities;
using Gestaoaju.Models.EntityModel.Financial.Periods;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class CashFlowAnalysis
    {
        public CashFlowAnalysis(IEnumerable<CashActivity> cashActivities)
        {
            Credits = new CashFlowCredits(cashActivities);
            Debits = new CashFlowDebits(cashActivities);
        }
        
        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DailyPeriod DailyPeriod => new DailyPeriod(StartDate, EndDate);

        public MonthlyPeriod MonthlyPeriod => new MonthlyPeriod(StartDate, EndDate);
        
        public CashFlowCredits Credits { get; private set; }

        public CashFlowDebits Debits { get; private set; }

        public void ToCurrentMonth()
        {
            int year = DateTime.UtcNow.Year;
            int month = DateTime.UtcNow.Month;

            StartDate = new DateTime(year, month, 1);
            EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            ToPeriod(StartDate, EndDate);
        }

        public void ToPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;

            Credits.ToPeriod(StartDate, EndDate);
            Debits.ToPeriod(StartDate, EndDate);
        }

        public Money Balance() => Credits.Total().Subtract(Debits.Total());

        public Money Balance(DateTime date) => Credits.Total(date)
            .Subtract(Debits.Total(date));

        public Money Balance(int month, int year) => Credits.Total(month, year)
            .Subtract(Debits.Total(month, year));
    }
}
