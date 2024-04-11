using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateViewModel request);
        Task<EmployeeLeaveRequestViewViewModel> GetMyLeaveDetails();
        Task<LeaveRequestViewModel?> GetLeaveRequestAsync(int? id);
        Task<List<LeaveRequest>> GetAllAsync(string employeeId);
        Task<AdminLeaveRequestViewViewModel> GetAdminLeaveRequestList();
        Task ChangeApprovalStatus (int leaveRequestId, bool approved);
        Task CancelLeaveRequest(int leaveRequestId);

    }
}
