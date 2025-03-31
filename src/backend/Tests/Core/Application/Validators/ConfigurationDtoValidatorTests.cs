using Backend.Core.Application.DTOs;
using Backend.Core.Application.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Backend.Tests.Core.Application.Validators
{
    [TestFixture]
    public class ConfigurationDtoValidatorTests
    {
        private ConfigurationDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ConfigurationDtoValidator();
        }

        [Test]
        public void Validate_WithValidConfiguration_ShouldNotHaveErrors()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = "Valid Configuration",
                Description = "Valid Description",
                WorkflowId = 1,
                EngineId = 2,
                RetryLimit = 3
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_WithEmptyName_ShouldHaveNameError()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = "",
                Description = "Valid Description",
                WorkflowId = 1,
                EngineId = 2,
                RetryLimit = 3
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_WithTooLongName_ShouldHaveNameError()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = new string('A', 101), // 101 characters
                Description = "Valid Description",
                WorkflowId = 1,
                EngineId = 2,
                RetryLimit = 3
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Validate_WithInvalidWorkflowId_ShouldHaveWorkflowIdError()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = "Valid Configuration",
                Description = "Valid Description",
                WorkflowId = 0, // Invalid
                EngineId = 2,
                RetryLimit = 3
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.WorkflowId);
        }

        [Test]
        public void Validate_WithInvalidEngineId_ShouldHaveEngineIdError()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = "Valid Configuration",
                Description = "Valid Description",
                WorkflowId = 1,
                EngineId = 0, // Invalid
                RetryLimit = 3
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.EngineId);
        }

        [Test]
        public void Validate_WithNegativeRetryLimit_ShouldHaveRetryLimitError()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = "Valid Configuration",
                Description = "Valid Description",
                WorkflowId = 1,
                EngineId = 2,
                RetryLimit = -1 // Invalid
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RetryLimit);
        }

        [Test]
        public void Validate_WithTooHighRetryLimit_ShouldHaveRetryLimitError()
        {
            // Arrange
            var configDto = new ConfigurationDto
            {
                Name = "Valid Configuration",
                Description = "Valid Description",
                WorkflowId = 1,
                EngineId = 2,
                RetryLimit = 11 // Invalid
            };

            // Act
            var result = _validator.TestValidate(configDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RetryLimit);
        }
    }
} 