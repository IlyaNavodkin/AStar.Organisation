using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.DomainServices.Exceptions;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Repositories;
using AStar.Organisation.Core.DomainServices.Validators;
using AStar.Organization.Infrastructure.BLL.Services;
using FluentValidation;
using Moq;

namespace AStar.Organisation.Infrastructure.UnitTests.Position
{
    [TestFixture]
    public class PositionServiceTests
    {
        private Mock<IPositionRepository> _positionRepositoryMock;
        private PositionValidator _positionValidator;
        private PositionService _positionService;
        
        [SetUp]
        public void Setup()
        {
            _positionRepositoryMock = new Mock<IPositionRepository>();
            // _positionValidator = new PositionValidator(_positionRepositoryMock.Object);
            // _positionService = new PositionService(_positionRepositoryMock.Object, _positionValidator);
        }

        [Test]
        public async Task GetAll_Should_Return_All_PositionDtos()
        {
            // Arrange
            _positionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(GetAllPositions());
            var expectedPositions = await _positionRepositoryMock.Object.GetAll();

            // Act
            var actualPositions = await _positionService.GetAll();

            // Assert
            Assert.AreEqual(expectedPositions.Count(), actualPositions.Count());
        }
        
        [Test]
        public async Task GetById_Should_Return_PositionDto()
        {
            // Arrange
            _positionRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(GetById());
            var expected = await _positionRepositoryMock.Object.GetById(1);

            // Act
            var actual = await _positionService.GetById(1);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DepartmentId, actual.DepartmentId);
        }
        
        [Test]
        public async Task GetById_Should_Throw_NotFoundEntityException_When_Position_Not_Found()
        {
            // Arrange
            var id = 1;
            _positionRepositoryMock.Setup(r => r.GetById(1)).ReturnsAsync((Core.Domain.Entities.Position) null);
            
            // Act & Assert
            Assert.ThrowsAsync<NotFoundEntityException>(async () => await _positionService.GetById(id));
        }
        
        [Test]
        public async Task Create_Should_Call_Repository_Create_With_Valid_Entity()
        {
            // Arrange
            var positionDto = new PositionDto { Name = "New Position", DepartmentId = 1 };
            var entity = new Core.Domain.Entities.Position { Name = positionDto.Name, DepartmentId = positionDto.DepartmentId };
            _positionRepositoryMock.Setup(r => r
                    .Create(It.IsAny<Core.Domain.Entities.Position>()))
                .Returns(Task.CompletedTask);
            
            // Act
            await _positionService.Create(positionDto);

            // Assert
            _positionRepositoryMock.Verify(r => r
                .Create(It.Is<Core.Domain.Entities.Position>(p => p.Name == entity.Name && 
                                                                  p.DepartmentId == entity.DepartmentId)), Times.Once);
        }
        
        [Test]
        [TestCaseSource(nameof(GetNotValidPositions))]
        public void Create_Should_Throw_ValidationException_When_Entity_Fails_Validation(PositionDto positionDto)
        {
            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _positionService.Create(positionDto));
        }
        
        private static IEnumerable<Core.Domain.Entities.Position> GetAllPositions()
        {
            var entities = new List<Core.Domain.Entities.Position>
            {
                new() { Id=1, Name="Инженер", DepartmentId = 2},
                new() { Id=2, Name="Секретарь", DepartmentId = 3},
                new() { Id=3, Name="Офис менеджер", DepartmentId = 3},
                new() { Id=4, Name="Работяга", DepartmentId = 4}
            };
            
            return entities;
        }
        
        private static IEnumerable<PositionDto> GetNotValidPositions()
        {
            var entities = new List<PositionDto>
            {
                new() { Id=1, Name="", DepartmentId = 2},
                new() { Id=2, Name="Ы", DepartmentId = 3},
                new() { Id=3, Name="Офис менеджер Офис менеджер Офис менеджер Офис менеджер Офис менеджер", 
                    DepartmentId = 3},
            };
            
            return entities;
        }

        private static Core.Domain.Entities.Position GetById() => new() { Id = 1, Name = "Инженер", DepartmentId = 2 };
    }
}