using System.Threading.Tasks;
using Backend.Core.Application.DTOs;
using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Application.UseCases.Configurations;
using Backend.Core.Domain.Entities;
using NSubstitute;
using NUnit.Framework;
using PdfConverter.Core.Application.Interfaces.Repositories;
using PdfConverter.Core.Domain.Entities;

namespace Backend.Tests.Core.Application.UseCases.Configurations
{
    [TestFixture]
    public class GetConfigurationByIdUseCaseTests
    {
        private IConfigurationRepository _configurationRepository;
        private GetConfigurationByIdUseCase _useCase;

        [SetUp]
        public void Setup()
        {
            _configurationRepository = Substitute.For<IConfigurationRepository>();
            _useCase = new GetConfigurationByIdUseCase(_configurationRepository);
        }

        [Test]
        public async Task ExecuteAsync_WithExistingId_ReturnsConfigurationDto()
        {
            // Arrange
            var configId = 1;
            var configuration = new Configuration
            {
                Id = configId,
                Name = "Test Configuration",
                Description = "Test Description",
                WorkflowId = 10,
                EngineId = 5,
                RetryLimit = 3
            };

            _configurationRepository.GetByIdAsync(configId).Returns(configuration);

            // Act
            var result = await _useCase.ExecuteAsync(configId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(configId));
            Assert.That(result.Name, Is.EqualTo("Test Configuration"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.WorkflowId, Is.EqualTo(10));
            Assert.That(result.EngineId, Is.EqualTo(5));
            Assert.That(result.RetryLimit, Is.EqualTo(3));
        }

        [Test]
        public async Task ExecuteAsync_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingId = 999;
            _configurationRepository.GetByIdAsync(nonExistingId).Returns((Configuration)null);

            // Act
            var result = await _useCase.ExecuteAsync(nonExistingId);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
