using System;
using System.Collections.Generic;
using System.Text;

namespace XamTutorial.Models
{
    public class PayStatement
    {
        /// <summary>
        /// Pay Period start date
        /// </summary>
        public DateTime Start { get; set; }
        // pay period end date
        public DateTime End { get; set; }
        // payout date for pay period
        public DateTime Date { get; set; }
        // payout amount
        public double Amount { get; set; }
        // list of work items for the pay period
        public List<WorkItem> WorkItems { get; set; }
    }
}
