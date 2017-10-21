
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Financial.Periods
{
    public class DailyPeriod : IEnumerable<DateTime>, IEnumerator<DateTime>
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime Current { get; set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public DailyPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            Current = Current == DateTime.MinValue ? StartDate : Current.AddDays(1);
            return Current <= EndDate;
        }

        public void Reset()
        {
            Current = DateTime.MinValue;
        }

        public void Dispose() { }
    }
}
