
using Microsoft.AspNetCore.Mvc;
using N5Company.Application.Commands;
using N5Company.Application.DTOs;
using N5Company.Application.Handlers;
using N5Company.Application.Interfaces;

namespace N5Company.Api.Controller
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _unitOfWork.Permissions.GetWithTypeAsync();

            var result = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                EmployeeName = p.EmployeeName,
                EmployeeLastName = p.EmployeeLastName,
                Date = p.Date,
                PermissionType = new PermissionTypeDto
                {
                    Id = p.PermissionType.Id,
                    Description = p.PermissionType.Description
                }
            });

            return Ok(result);
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestPermission(
            [FromBody] RequestPermissionCommand command,
            [FromServices] RequestPermissionHandler handler)
        {
            var id = await handler.Handle(command);
            return CreatedAtAction(nameof(GetPermissions), new { id }, null);
        }

        [HttpPut("modify")]
        public async Task<IActionResult> ModifyPermission(
            [FromBody] ModifyPermissionCommand command,
            [FromServices] ModifyPermissionHandler handler)
        {
            var updated = await handler.Handle(command);
            if (!updated)
                return NotFound();

            return NoContent();
        }
    }

}
