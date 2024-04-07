using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        public LeaveAllocationRepository(ApplicationDbContext context,
                                        UserManager<Employee> userManager,
                                        ILeaveTypeRepository leaveTypeRepository,
                                        IMapper mapper)
                                        : base(context)
        {
            _context = context;
            _userManager = userManager;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;

        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(x => x.EmployeeId == employeeId
                                                              && x.LeaveTypeId == leaveTypeId
                                                              && x.Period == period);
        }

        public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await _context.LeaveAllocations
                .Where(x => x.EmployeeId == employeeId)
                .Include(x => x.LeaveType)
                .ToListAsync();
            var employee = await _userManager.FindByIdAsync(employeeId);

            var employeeAllocationModel = _mapper.Map<EmployeeAllocationViewModel>(employee);
            employeeAllocationModel.LeaveAllocations = _mapper.Map<List<LeaveAllocationViewModel>>(allocations);

            return employeeAllocationModel;

        }

        public async Task<LeaveAllocationViewModel> GetEmployeeAllocation(int id)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (allocation == null)
            {
                return null;
            }

            var employee = await _userManager.FindByIdAsync(allocation.EmployeeId);

            var model = _mapper.Map<LeaveAllocationEditViewModel>(allocation);
            model.Employee = _mapper.Map<EmployeeListViewModel>(await _userManager.FindByIdAsync(allocation.EmployeeId));

            return model;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepository.GetAsync(leaveTypeId);
            var allocations = new List<LeaveAllocation>();

            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveTypeId, period)) continue;
                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }
            await AddRangeAsync(allocations);
        }
        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditViewModel model)
        {
            var leaveAllocation = await GetAsync(model.Id);
            if (leaveAllocation == null)
            {
                return false;
            }
            leaveAllocation.Period = model.Period;
            leaveAllocation.NumberOfDays = model.NumberOfDays;
            await UpdateAsync(leaveAllocation);

            return true;
        }
    }
}
