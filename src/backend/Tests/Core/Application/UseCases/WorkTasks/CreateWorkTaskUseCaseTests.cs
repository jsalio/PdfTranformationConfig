using System;
using System.Threading.Tasks;
using Backend.Core.Application.DTOs;
using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Application.UseCases.WorkTasks;
using Backend.Core.Domain.Entities;
using Backend.Core.Domain.Enums;
using Moq;
using NUnit.Framework;
using PdfConverter.Core.Application.Interfaces.Repositories;
using PdfConverter.Core.Domain.Entities;

namespace Backend.Tests.Core.Application.UseCases.WorkTasks
{
    [TestFixture]
    public class CreateWorkTaskUseCaseTests
    {
        private Mock<IWorkTaskRepository> _workTaskRepository;
        private Mock<IConfigurationRepository> _configurationRepository;
        private CreateWorkTaskUseCase _useCase;

        [SetUp]
        public void Setup()
        {
            _workTaskRepository = new Mock<IWorkTaskRepository>();
            _configurationRepository = new Mock<IConfigurationRepository>();
            _useCase = new CreateWorkTaskUseCase(_workTaskRepository.Object, _configurationRepository.Object);
        }

        [Test]
        public async Task ExecuteAsync_WithValidData_CreatesWorkTask()
        {
            // Arrange
            var configId = 5;
            var configuration = new Configuration { Id = configId, Name = "Test Config" };
            _configurationRepository.Setup(x => x.GetByIdAsync(configId)).ReturnsAsync(configuration);

            var workTaskDto = new WorkTaskDto
            {
                Name = "New Work Task",
                ConfigurationId = configId
            };

            WorkTask capturedWorkTask = null;
            _workTaskRepository.Setup(x => x.AddAsync(It.IsAny<WorkTask>()))
                .Callback<WorkTask>(workTask => 
                {
                    capturedWorkTask = workTask;
                    capturedWorkTask.Id = 1; // Simulate DB assigning ID
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.ExecuteAsync(workTaskDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("New Work Task"));
            Assert.That(result.ConfigurationId, Is.EqualTo(configId));
            Assert.That(result.Status, Is.EqualTo(WorkTaskStatus.Pending));
            Assert.That(result.Created, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));

            _workTaskRepository.Verify(x => x.AddAsync(It.IsAny<WorkTask>()), Times.Once);
            _workTaskRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void ExecuteAsync_WithNonExistingConfiguration_ThrowsKeyNotFoundException()
        {
            // Arrange
            var nonExistingConfigId = 999;
            _configurationRepository.Setup(x => x.GetByIdAsync(nonExistingConfigId)).ReturnsAsync((Configuration)null);

            var workTaskDto = new WorkTaskDto
            {
                Name = "New Work Task",
                ConfigurationId = nonExistingConfigId
            };

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _useCase.ExecuteAsync(workTaskDto));
            _workTaskRepository.Verify(x => x.AddAsync(It.IsAny<WorkTask>()), Times.Never);
        }
    }
}
