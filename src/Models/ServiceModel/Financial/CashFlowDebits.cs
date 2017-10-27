/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.CashActivities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class CashFlowDebits
    {
        public DateTime StartDate { get; private set; }
        
        public DateTime EndDate { get; private set; }
        
        public IEnumerable<CashActivity> CashActivities { get; private set; }
        
        public IEnumerable<CashActivity> Purchases => CashActivities
            .Where(cashFlow => cashFlow.Activity == CashActivityType.Purchase);

        public IEnumerable<CashActivity> FixedExpenses => CashActivities
            .Where(cashFlow => cashFlow.Activity == CashActivityType.FixedExpense);

        public IEnumerable<CashActivity> Others => CashActivities
            .Where(cashFlow => cashFlow.Activity == CashActivityType.Other && cashFlow.Total < 0);

        public CashFlowDebits(IEnumerable<CashActivity> cashActivities)
        {
            CashActivities = cashActivities;
        }

        public void ToPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Money Total() => TotalPurchases()
            .Sum(TotalFixedExpenses())
            .Sum(TotalOthers());

        public Money Total(DateTime date) => TotalPurchases(date)
            .Sum(TotalFixedExpenses(date))
            .Sum(TotalOthers(date));

        public Money Total(int month, int year) => TotalPurchases(month, year)
            .Sum(TotalFixedExpenses(month, year))
            .Sum(TotalOthers(month, year));
        
        public Money TotalPurchases() => Purchases.Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalPurchases(DateTime date) => Purchases
            .Where(activity => activity.StartDate == date)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalPurchases(int month, int year) => Purchases
            .Where(activity =>
                activity.StartDate.Month == month &&
                activity.StartDate.Year == year)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalFixedExpenses() => FixedExpenses.Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalFixedExpenses(DateTime date) => FixedExpenses
            .Where(activity => activity.StartDate.Day == date.Day)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalFixedExpenses(int month, int year) => FixedExpenses
            .Where(activity =>
                activity.EndDate == null ||
                activity.EndDate.Value.Month >= month &&
                activity.EndDate.Value.Year >= year)
            .Sum(activity => (decimal?)activity.Total) ?? 0;
        
        public Money TotalOthers() => Others.Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalOthers(DateTime date) => Others
            .Where(activity => activity.StartDate == date)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalOthers(int month, int year) => Others
            .Where(activity =>
                activity.StartDate.Month == month &&
                activity.StartDate.Year == year)
            .Sum(activity => (decimal?)activity.Total) ?? 0;
    }
}
