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
    public class CashFlowCredits
    {
        public CashFlowCredits(IEnumerable<CashActivity> cashActivities)
        {
            CashActivities = cashActivities;
        }

        public DateTime StartDate { get; private set; }
        
        public DateTime EndDate { get; private set; }

        public IEnumerable<CashActivity> CashActivities { get; private set; }

        public IEnumerable<CashActivity> RentContracts => CashActivities
            .Where(cashFlow =>cashFlow.Activity == CashActivityType.Rental);

        public IEnumerable<CashActivity> Sales => CashActivities
            .Where(cashFlow => cashFlow.Activity == CashActivityType.Sale);

        public IEnumerable<CashActivity> Others => CashActivities
            .Where(cashFlow => cashFlow.Activity == CashActivityType.Other && cashFlow.Total > 0);

        public void ToPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Money Total() => TotalRentContracts()
            .Sum(TotalSales())
            .Sum(TotalOthers());

        public Money Total(DateTime date) => TotalRentContracts(date)
            .Sum(TotalSales(date))
            .Sum(TotalOthers(date));

        public Money Total(int month, int year) => TotalRentContracts(month, year)
            .Sum(TotalSales(month, year))
            .Sum(TotalOthers(month, year));

        public Money TotalRentContracts() => RentContracts.Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalRentContracts(DateTime date) => RentContracts
            .Where(activity => activity.StartDate == date)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalRentContracts(int month, int year) => RentContracts
            .Where(activity =>
                activity.StartDate.Month == month &&
                activity.StartDate.Year == year)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalSales() => Sales.Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalSales(DateTime date) => Sales
            .Where(activity => activity.StartDate == date)
            .Sum(activity => (decimal?)activity.Total) ?? 0;

        public Money TotalSales(int month, int year) => Sales
            .Where(activity =>
                activity.StartDate.Month == month &&
                activity.StartDate.Year == year)
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
