using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Web.Data
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }

        [ForeignKey("LeaveTypeId")]
        public int LeaveTypeId { get; set; }
        [ForeignKey("EmployeeId")]
        public string EmployeeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public Employee Employee { get; set; }
    }
}
