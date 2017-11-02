/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Commercial.PaymentMethodFees;
using Gestaoaju.Models.EntityModel.Financial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public abstract class Billing<TIncome> where TIncome : IIncome
    {
        private IInvoice invoice;

        public Billing(IInvoice invoice)
        {
            this.invoice = invoice;
        }

        private void AssignPaymentMethodFee(IPayment payment)
        {
            PaymentMethodFee paymentMethodFee = payment.PaymentMethod.PaymentMethodFees
                .OrderByDescending(fee => fee.MinimumNumberInstallments)
                .FirstOrDefault(fee => payment.NumberOfInstallments >= fee.MinimumNumberInstallments);

            if (paymentMethodFee != null)
            {
                payment.FeePercentage = paymentMethodFee.Percentage;
                payment.FeeFixedValue = paymentMethodFee.FixedValue;
            }
        }

        private void CalculateTotals(IPayment payment, IIncome income, int numberOfInstallments)
        {
            payment.BilledAmount = payment.Total;

            payment.BilledAmount = new Money(payment.BilledAmount)
                .SubtractPercentage(payment.FeePercentage ?? 0)
                .Subtract(payment.FeeFixedValue ?? 0);

            payment.InstallmentBilling = new Money(payment.BilledAmount).Divide(numberOfInstallments);

            income.AmountReceived = payment.InstallmentBilling;
        }

        private void CalculateReceiptTiming(IPayment payment, IIncome income, int installment)
        {
            income.PaymentDate = payment.Date.AddMonths(installment - 1);

            if (payment.PaymentMethod.DaysToReceive != null)
            {
                DateTime baseDate = payment.PaymentMethod.EarlyReceipt ? payment.Date : income.PaymentDate;
                income.ReceivedDate = baseDate.AddDays(payment.PaymentMethod.DaysToReceive.Value);
            }
        }

        private IEnumerable<TIncome> CalculateIncomes(IPayment payment)
        {
            int installments = payment.PaymentMethod.EarlyReceipt ? 1 : payment.NumberOfInstallments;

            AssignPaymentMethodFee(payment);

            for (int installment = 1; installment <= installments; installment++)
            {
                TIncome income = NewIncome();
                income.PaymentId = payment.Id;

                CalculateTotals(payment, income, installments);
                CalculateReceiptTiming(payment, income, installment);

                yield return income;
            }
        }

        protected abstract TIncome NewIncome();

        protected abstract void AddIncomes(IEnumerable<TIncome> incomes);

        protected abstract void RemoveIncomes();

        public void Confirm()
        {
            foreach (IPayment payment in invoice.Payments)
            {
                CalculateIncomes(payment);
            }

            invoice.ConfirmationDate = DateTime.UtcNow;
            invoice.Total = new Money(invoice.Payments.Sum(p => p.BilledAmount));
        }

        public void Revert()
        {
            foreach (IPayment payment in invoice.Payments)
            {
                payment.FeePercentage = null;
                payment.FeeFixedValue = null;
                payment.InstallmentBilling = 0;
                payment.BilledAmount = 0;
            }

            invoice.ConfirmationDate = null;
            invoice.Total = 0;

            RemoveIncomes();
        }
    }
}
