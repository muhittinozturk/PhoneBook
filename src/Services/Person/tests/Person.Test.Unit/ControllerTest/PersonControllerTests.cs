using Application.Features.Person.Commands.AddPerson;
using Application.Features.Person.Commands.DeletePerson;
using Application.Features.Person.Commands.UpdatePerson;
using Application.Features.Person.Queries.GetPerson;
using Application.Features.Person.Queries.GetPersonList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonAPI.Controllers;
using System.Net;

namespace Person.Test.Unit.ControllerTest
{
    public class PersonControllerTests
    {
        [Fact]
        public async Task Get_Returns_Ok_Result()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PersonController(mediatorMock.Object);
            var id = Guid.Parse("e2f4d30b-c4a7-46f1-ba5b-3c45f81ba5a7");

            // Setup Mediator Mock
            mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonQueryRequest>(), default))
                        .ReturnsAsync(new GetPersonQueryResponse());

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetAll_Returns_Ok_Result()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PersonController(mediatorMock.Object);

            // Setup Mediator Mock
            mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonListQueryRequest>(), default))
                        .ReturnsAsync(new List<GetPersonListQueryResponse>()
                        {
                            new GetPersonListQueryResponse()
                        });

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_Returns_Created_Result()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<AddPersonCommandRequest>(), default))
                .ReturnsAsync(new AddPersonCommandResponse { IsSuccess = true });

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.Create(new AddPersonCommandRequest());

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, (result as StatusCodeResult)?.StatusCode);
        }

        [Fact]
        public async Task Update_Returns_Ok_Result()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<UpdatePersonCommandRequest>(), default))
                .ReturnsAsync(new UpdatePersonCommandResponse { IsSuccess = true });

            var controller = new PersonController(mediatorMock.Object);

            // Act
            var result = await controller.Update(new UpdatePersonCommandRequest());

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result as StatusCodeResult)?.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<DeletePersonCommandRequest>(), default))
                .ReturnsAsync(new DeletePersonCommandResponse { IsSuccess = true });

            var controller = new PersonController(mediatorMock.Object);
            var id = Guid.NewGuid();

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result as StatusCodeResult)?.StatusCode);
        }
    }
}
