using System;
using System.Threading.Tasks;
using N5Company.Application.Commands;
using N5Company.Application.Interfaces;
using N5Company.Application.Elastic;
using N5Company.Domain.Entities;

namespace N5Company.Application.Handlers
{
    public class RequestPermissionHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticLogger _elasticLogger;

        public RequestPermissionHandler(
            IUnitOfWork unitOfWork,
            IElasticLogger elasticLogger)
        {
            _unitOfWork = unitOfWork;
            _elasticLogger = elasticLogger;
        }

        public async Task<int> Handle(RequestPermissionCommand command)
        {
            var permission = new Permission
            {
                EmployeeName = command.EmployeeName,
                EmployeeLastName = command.EmployeeLastName,
                PermissionTypeId = command.PermissionTypeId,
                Date = DateTime.UtcNow
            };

            await _unitOfWork.Permissions.AddAsync(permission);
            await _unitOfWork.CompleteAsync();

            await _elasticLogger.LogAsync("request", new
            {
                PermissionId = permission.Id,
                permission.EmployeeName,
                permission.EmployeeLastName
            });

            return permission.Id;
        }
    }
}