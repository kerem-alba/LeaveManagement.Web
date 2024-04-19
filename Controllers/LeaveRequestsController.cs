using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using AutoMapper;
using LeaveManagement.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Common.Constants;
using LeaveManagement.Application.Repositories;

namespace LeaveManagement.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILogger<LeaveRequestsController> _logger;

        public LeaveRequestsController(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, 
            ILogger<LeaveRequestsController> logger )
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestRepository.GetAdminLeaveRequestList();
            return View(model);
        }

        public async Task<IActionResult> MyLeave()
        {
            var model = await _leaveRequestRepository.GetMyLeaveDetails();
            return View(model);
        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await _leaveRequestRepository.GetLeaveRequestAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        public async Task<IActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await _leaveRequestRepository.ChangeApprovalStatus(id, approved);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error approving leave request: {0}", ex.Message);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _leaveRequestRepository.CancelLeaveRequest(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error cancelling leave request: {0}", ex.Message);
                throw;
            }
            return RedirectToAction(nameof(MyLeave));
        }



        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            var model = new LeaveRequestCreateViewModel
            {
                LeaveTypes = new SelectList(_leaveTypeRepository.GetAllAsync().Result, "Id", "Name")
            };
            return View(model);
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isValidRequest = await _leaveRequestRepository.CreateLeaveRequest(model);
                    if (isValidRequest)
                    {
                        return RedirectToAction(nameof(MyLeave));
                    }
                    ModelState.AddModelError(string.Empty, "You have exceeded your allocation with this request.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating leave request: {0}", ex.Message);
                ModelState.AddModelError(string.Empty, "An Error Has Occurred. Please Try Again Later");
            }

            model.LeaveTypes = new SelectList(await _leaveTypeRepository.GetAllAsync(), "Id", "Name", model.LeaveTypeId);
            return View(model);
        }
    }
}
