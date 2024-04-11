using LeaveManagement.Web.Data;

namespace LeaveManagement.Web.Models
{
    public class EmployeeLeaveRequestViewViewModel
    {
        private List<LeaveAllocationViewModel> allocations;
        private Task<List<LeaveRequest>> requests;

        public EmployeeLeaveRequestViewViewModel
            (List<LeaveAllocationViewModel> leaveAllocations,
            List<LeaveRequestViewModel> leaveRequests)
        {
            LeaveAllocations = leaveAllocations;
            LeaveRequests = leaveRequests;
        }

        public EmployeeLeaveRequestViewViewModel(List<LeaveAllocationViewModel> allocations, Task<List<LeaveRequest>> requests)
        {
            this.allocations = allocations;
            this.requests = requests;
        }

        public List<LeaveAllocationViewModel>? LeaveAllocations { get; set; }
        public List<LeaveRequestViewModel>? LeaveRequests { get; set; }
    }
}
