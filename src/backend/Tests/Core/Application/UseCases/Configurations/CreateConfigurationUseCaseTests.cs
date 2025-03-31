using System;
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
    public class CreateConfigurationUseCaseTests
    {
        private IConfigurationRepository _configurationRepository;
        private IEngineRepository _engineRepository;
        private CreateConfigurationUseCase _useCase;

        [SetUp]
        public void Setup()
        {
            _configurationRepository = Substitute.For<IConfigurationRepository>();
            _engineRepository = Substitute.For<IEngineRepository>();
            _useCase = new CreateConfigurationUseCase(_configurationRepository, _engineRepository);
        }

        [Test]
        public async Task ExecuteAsync_WithValidData_CreatesConfiguration()
        {
            // Arrange
            var engineId = 5;
            var engine = new Engine { Id = engineId, Name = "Test Engine" };
            _engineRepository.GetByIdAsync(engineId).Returns(engine);

            var configDto = new ConfigurationDto
            {
                Name = "New Configuration",
                Description = "New Description",
                WorkflowId = 10,
                EngineId = engineId,
                RetryLimit = 3
            };

            _configurationRepository.When(x => x.AddAsync(Arg.Any<Configuration>()))
                .Do(callInfo => 
                {
                    var config = callInfo.Arg<Configuration>();
                    config.Id = 1; // Simular asignación de ID por la base de datos
                });

            // Act
            var result = await _useCase.ExecuteAsync(configDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("New Configuration"));
            Assert.That(result.Description, Is.EqualTo("New Description"));
            Assert.That(result.WorkflowId, Is.EqualTo(10));
            Assert.That(result.EngineId, Is.EqualTo(engineId));
            Assert.That(result.RetryLimit, Is.EqualTo(3));

            await _configurationRepository.Received(1).AddAsync(Arg.Any<Configuration>());
            await _configurationRepository.Received(1).SaveChangesAsync();
        }

        [Test]
        public void ExecuteAsync_WithNonExistingEngine_ThrowsKeyNotFoundException()
        {
            // Arrange
            var nonExistingEngineId = 999;
            _engineRepository.GetByIdAsync(nonExistingEngineId).Returns((Engine)null);

            var configDto = new ConfigurationDto
            {
                Name = "New Configuration",
                Description = "New Description",
                WorkflowId = 10,
                EngineId = nonExistingEngineId,
                RetryLimit = 3
            };

            // Act & Assert
            Assert.That(() => _useCase.ExecuteAsync(configDto), Throws.TypeOf<KeyNotFoundException>());
            _configurationRepository.DidNotReceive().AddAsync(Arg.Any<Configuration>());
        }
    }
}
