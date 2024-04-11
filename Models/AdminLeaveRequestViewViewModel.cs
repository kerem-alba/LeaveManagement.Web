using System.ComponentModel;

namespace LeaveManagement.Web.Models
{
    public class AdminLeaveRequestViewViewModel
    {
        [DisplayName("Total Requests")]
        public int TotalRequests { get; set; }

        [DisplayName("Approved Requests")]
        public int ApprovedRequests { get; set; }

        [DisplayName("Pending Requests")]
        public int PendingRequests { get; set; }

        [DisplayName("Rejected Requests")]
        public int RejectedRequests { get; set; }

        public List<LeaveRequestViewModel> LeaveRequests { get; set; }

    }
}
