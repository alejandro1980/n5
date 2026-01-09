using N5Company.Application.Commands;
using N5Company.Application.Interfaces;
using N5Company.Application.Elastic;

namespace N5Company.Application.Handlers
{
    public class ModifyPermissionHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticLogger _elasticLogger;

        public ModifyPermissionHandler(
            IUnitOfWork unitOfWork,
            IElasticLogger elasticLogger)
        {
            _unitOfWork = unitOfWork;
            _elasticLogger = elasticLogger;
        }

        public async Task<bool> Handle(ModifyPermissionCommand command)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(command.Id);
            if (permission == null)
                return false;

            permission.EmployeeName = command.EmployeeName;
            permission.EmployeeLastName = command.EmployeeLastName;
            permission.PermissionTypeId = command.PermissionTypeId;

            _unitOfWork.Permissions.Update(permission);
            await _unitOfWork.CompleteAsync();

            await _elasticLogger.LogAsync("modify", new
            {
                PermissionId = permission.Id
            });

            return true;
        }
    }
}