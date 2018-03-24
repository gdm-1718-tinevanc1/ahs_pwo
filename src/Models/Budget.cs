using System;
using System.Collections.Generic;

namespace Models
{
    public class Budget
    {
        public Int32 Id { get; set; }
        
        public Int32 TotalBudget { get; set; }

        public Int32 ArteveldeBudget { get; set; }

        public Int32 OperatingBudget { get; set; }

        public Int32 InvestmentBudget { get; set; }
        
        public Int32 StaffBudget { get; set; }

        public List<Project> Project { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

    }
}
