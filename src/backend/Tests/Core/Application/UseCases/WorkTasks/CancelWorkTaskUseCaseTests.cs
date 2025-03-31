using System;
using System.Threading.Tasks;
using Backend.Core.Application.Interfaces.Repositories;
using Backend.Core.Application.UseCases.WorkTasks;
using Backend.Core.Domain.Entities;
using Backend.Core.Domain.Enums;
using NSubstitute;
using NUnit.Framework;

namespace Backend.Tests.Core.Application.UseCases.WorkTasks
{
    [TestFixture]
    public class CancelWorkTaskUseCaseTests
    {
        private IWorkTaskRepository _workTaskRepository;
        private ICancelationTaskRepository _cancelationTaskRepository;
        private CancelWorkTaskUseCase _useCase;

        [SetUp]
        public void Setup()
        {
            _workTaskRepository = Substitute.For<IWorkTaskRepository>();
            _cancelationTaskRepository = Substitute.For<ICancelationTaskRepository>();
            _useCase = new CancelWorkTaskUseCase(_workTaskRepository, _cancelationTaskRepository);
        }

        [Test]
        public async Task ExecuteAsync_WithPendingWorkTask_CancelsWorkTask()
        {
            // Arrange
            var workTaskId = 1;
            var justification = "Test cancellation";
            var workTask = new WorkTask
            {
                Id = workTaskId,
                Name = "Test Work Task",
                Status = WorkTaskStatus.Pending
            };

            _workTaskRepository.GetByIdAsync(workTaskId).Returns(workTask);

            // Act
            var result = await _useCase.ExecuteAsync((workTaskId, justification));

            // Assert
            Assert.That(result, Is.True);
            Assert.That(workTask.Status, Is.EqualTo(WorkTaskStatus.Canceled));

            await _cancelationTaskRepository.Received(1).AddAsync(Arg.Is<CancelationTask>(ct => 
                ct.WorkTaskId == workTaskId && 
                ct.Justificacion == justification));
            
            await _workTaskRepository.Received(1).UpdateAsync(workTask);
            await _workTaskRepository.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task ExecuteAsync_WithWorkingWorkTask_CancelsWorkTask()
        {
            // Arrange
            var workTaskId = 1;
            var justification = "Test cancellation";
            var workTask = new WorkTask
            {
                Id = workTaskId,
                Name = "Test Work Task",
                Status = WorkTaskStatus.Working
            };

            _workTaskRepository.GetByIdAsync(workTaskId).Returns(workTask);

            // Act
            var result = await _useCase.ExecuteAsync((workTaskId, justification));

            // Assert
            Assert.That(result, Is.True);
            Assert.That(workTask.Status, Is.EqualTo(WorkTaskStatus.Canceled));
        }

        [Test]
        public async Task ExecuteAsync_WithAlreadyCanceledWorkTask_ReturnsFalse()
        {
            // Arrange
            var workTaskId = 1;
            var justification = "Test cancellation";
            var workTask = new WorkTask
            {
                Id = workTaskId,
                Name = "Test Work Task",
                Status = WorkTaskStatus.Canceled
            };

            _workTaskRepository.GetByIdAsync(workTaskId).Returns(workTask);

            // Act
            var result = await _useCase.ExecuteAsync((workTaskId, justification));

            // Assert
            Assert.That(result, Is.False);
            await _cancelationTaskRepository.DidNotReceive().AddAsync(Arg.Any<CancelationTask>());
        }

        [Test]
        public async Task ExecuteAsync_WithNonExistingWorkTask_ReturnsFalse()
        {
            // Arrange
            var nonExistingId = 999;
            var justification = "Test cancellation";
            
            _workTaskRepository.GetByIdAsync(nonExistingId).Returns((WorkTask)null);

            // Act
            var result = await _useCase.ExecuteAsync((nonExistingId, justification));

            // Assert
            Assert.That(result, Is.False);
            await _cancelationTaskRepository.DidNotReceive().AddAsync(Arg.Any<CancelationTask>());
        }
    }
}
