using Moq;
using N5Company.Application.Commands;
using N5Company.Application.Elastic;
using N5Company.Application.Handlers;
using N5Company.Application.Interfaces;
using N5Company.Domain.Entities;
using Xunit;

public class ModifyPermissionTests
{
    [Fact]
    public async Task Should_Modify_Permission()
    {
        var permission = new Permission
        {
            Id = 10,
            EmployeeName = "John",
            EmployeeLastName = "Doe",
            PermissionTypeId = 1
        };

        var mockUow = new Mock<IUnitOfWork>();
        var mockRepo = new Mock<IPermissionRepository>();
        var mockElastic = new Mock<IElasticLogger>();

        mockUow.Setup(u => u.Permissions).Returns(mockRepo.Object);
        mockRepo.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(permission);
        mockUow.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        var handler = new ModifyPermissionHandler(mockUow.Object, mockElastic.Object);

        var result = await handler.Handle(new ModifyPermissionCommand
        {
            Id = 10,
            EmployeeName = "Jane",
            EmployeeLastName = "Smith",
            PermissionTypeId = 2
        });

        Assert.True(result);
        Assert.Equal("Jane", permission.EmployeeName);
        Assert.Equal(2, permission.PermissionTypeId);

        mockElastic.Verify(e => e.LogAsync("modify", It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task Should_Return_False_When_Permission_Not_Found()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var mockRepo = new Mock<IPermissionRepository>();
        var mockElastic = new Mock<IElasticLogger>();

        mockUow.Setup(u => u.Permissions).Returns(mockRepo.Object);
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Permission)null);

        var handler = new ModifyPermissionHandler(mockUow.Object, mockElastic.Object);

        var result = await handler.Handle(new ModifyPermissionCommand
        {
            Id = 99,
            EmployeeName = "X",
            EmployeeLastName = "Y",
            PermissionTypeId = 1
        });

        Assert.False(result);
        mockElastic.Verify(e => e.LogAsync("modify", It.IsAny<object>()), Times.Never);
    }
}