using System.Security.Claims;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests;

public class PersonalInformationControllerTests
{
    // [Theory, PersonalInformationData]
    // public async Task AddPersonInformation_ValidRequest_ReturnsOk(
    //      [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //      [Frozen] Mock<IMapper> mapperMock,
    //      [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //      ClaimsPrincipal user,
    //      PersonInformationRequest personInformationRequest,
    //      PersonInformation personInformation,
    //      PersonalInformationController controller)
    // {
    //     // Arrange
    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = user }
    //     };

    //     mapperMock.Setup(m => m.Map<PersonInformation>(It.IsAny<PersonInformationRequest>())).Returns(personInformation);
    //     personInformationServiceMock.Setup(s => s.AddPersonAsync(It.IsAny<PersonInformation>())).Returns(Task.CompletedTask);

    //     // Act
    //     var result = await controller.AddPersonInformation(personInformationRequest);

    //     // Assert
    //     var okResult = Assert.IsType<OkResult>(result);
    //     loggerMock.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
    // }

    // [Theory, PersonalInformationData]
    // public async Task GetAllPersonalInformationAsync_UserClaimNotFound_ReturnsNotFound(
    //     [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //     [Frozen] Mock<IMapper> mapperMock,
    //     [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //     PersonalInformationController controller)
    // {
    //     // Arrange
    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
    //     };

    //     // Act
    //     var result = await controller.GetAllPersonalInformationAsync();

    //     // Assert
    //     var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
    //     Assert.Equal("User claim not found in the token.", notFoundResult.Value);
    // }

    // [Theory, PersonalInformationData]
    // public async Task GetPersonalInformationbyIdAsync_ValidRequest_ReturnsOk(
    //     [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //     [Frozen] Mock<IMapper> mapperMock,
    //     [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //     ClaimsPrincipal user,
    //     PersonInformation personInformation,
    //     PersonInformationResponse personInformationResponse,
    //     PersonalInformationController controller)
    // {
    //     // Arrange
    //     var personInformationId = personInformation.Id;

    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = user }
    //     };

    //     personInformationServiceMock.Setup(s => s.GetPersonByIdAsync(personInformationId)).ReturnsAsync(personInformation);
    //     mapperMock.Setup(m => m.Map<PersonInformationResponse>(personInformation)).Returns(personInformationResponse);

    //     // Act
    //     var result = await controller.GetPersonalInformationbyIdAsync(personInformationId);

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     Assert.Equal(personInformationResponse, okResult.Value);
    // }

    // [Theory, PersonalInformationData]
    // public async Task GetPersonalInformationbyIdAsync_InvalidUser_ReturnsBadRequest(
    //     [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //     [Frozen] Mock<IMapper> mapperMock,
    //     [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //     ClaimsPrincipal user,
    //     PersonInformation personInformation,
    //     PersonalInformationController controller)
    // {
    //     // Arrange
    //     var personInformationId = personInformation.Id;
    //     personInformation.User.Id = 999; // Ensure the user id does not match

    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = user }
    //     };

    //     personInformationServiceMock.Setup(s => s.GetPersonByIdAsync(personInformationId)).ReturnsAsync(personInformation);

    //     // Act
    //     var result = await controller.GetPersonalInformationbyIdAsync(personInformationId);

    //     // Assert
    //     var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //     Assert.Equal("Updating Data of Another User not allowed ", badRequestResult.Value);
    // }

    // [Theory, PersonalInformationData]
    // public async Task UpdatePersonalInformation_ValidRequest_ReturnsNoContent(
    //     [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //     [Frozen] Mock<IPlaceOfResidenceService> placeOfResidenceServiceMock,
    //     [Frozen] Mock<IMapper> mapperMock,
    //     [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //     ClaimsPrincipal user,
    //     PersonInformationUpdate personInformationUpdate,
    //     PersonInformation personInformation,
    //     PlaceOfResidence placeOfResidence,
    //     PersonalInformationController controller)
    // {
    //     // Arrange
    //     var personInformationId = personInformation.Id;

    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = user }
    //     };

    //     personInformationServiceMock.Setup(s => s.GetPersonByIdAsync(personInformationId)).ReturnsAsync(personInformation);
    //     placeOfResidenceServiceMock.Setup(s => s.GetPlaceOfResidenceByIdAsync(personInformation.PlaceOfResidence.Id)).ReturnsAsync(placeOfResidence);
    //     personInformationServiceMock.Setup(s => s.UpdatePersonAsync(It.IsAny<int>(), It.IsAny<PersonInformation>())).Returns(Task.CompletedTask);
    //     placeOfResidenceServiceMock.Setup(s => s.UpdatePlaceOfResidenceAsync(It.IsAny<int>(), It.IsAny<PlaceOfResidenceDto>())).Returns(Task.CompletedTask);

    //     // Act
    //     var result = await controller.GetPersonalInformationbyIdAsync(personInformationId, personInformationUpdate);

    //     // Assert
    //     var noContentResult = Assert.IsType<NoContentResult>(result);
    //     loggerMock.Verify(l => l.LogInformation(It.IsAny<string>()), Times.Once);
    // }

    // [Theory, PersonalInformationData]
    // public async Task GetUserPhotoByUserIDAsync_ValidRequest_ReturnsFile(
    //     [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //     [Frozen] Mock<IMapper> mapperMock,
    //     [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //     ClaimsPrincipal user,
    //     PersonInformation personInformation,
    //     PersonalInformationController controller)
    // {
    //     // Arrange
    //     var personInformationId = personInformation.Id;

    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = user }
    //     };

    //     personInformationServiceMock.Setup(s => s.GetPersonByIdAsync(personInformationId)).ReturnsAsync(personInformation);

    //     // Act
    //     var result = await controller.GetUserPhotoByUserIDAsync(personInformationId);

    //     // Assert
    //     var fileResult = Assert.IsType<FileContentResult>(result);
    //     Assert.Equal("image/jpeg", fileResult.ContentType);
    //     Assert.Equal("image.jpg", fileResult.FileDownloadName);
    // }

    // [Theory, PersonalInformationData]
    // public async Task GetUserPhotoByUserIDAsync_PhotoNotFound_ReturnsNotFound(
    //     [Frozen] Mock<IPersonInformationService> personInformationServiceMock,
    //     [Frozen] Mock<IMapper> mapperMock,
    //     [Frozen] Mock<ILogger<PersonalInformationController>> loggerMock,
    //     ClaimsPrincipal user,
    //     PersonInformation personInformation,
    //     PersonalInformationController controller)
    // {
    //     // Arrange
    //     var personInformationId = personInformation.Id;
    //     personInformation.ProfilePhoto = null;

    //     controller.ControllerContext = new ControllerContext
    //     {
    //         HttpContext = new DefaultHttpContext { User = user }
    //     };

    //     personInformationServiceMock.Setup(s => s.GetPersonByIdAsync(personInformationId)).ReturnsAsync(personInformation);

    //     // Act
    //     var result = await controller.GetUserPhotoByUserIDAsync(personInformationId);

    //     // Assert
    //     var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
    //     Assert.Equal($"Photo for person information with Id - {personInformationId} not found .", notFoundResult.Value);
    // }

    

        [Theory ,ReviewData]
        public void GetReview_Returns_OkResult(int id ,Review review)
        {
            //Arrange
            var mockUserService = new Mock<IPersonInformationService>();
            var sut = new PersonalInformationController(mockUserService.Object);
            // mockUserService.Setup(x=>x.
            mockUserService.Setup(x => x.GetReviewById(It.IsAny<int>())).Returns(review);

            //Act
            var result = sut.GetReview(id);
                
            //Assert    


           var okresult = Assert.IsType<OkObjectResult>(result);

            //Assert.IsType<OkObjectResult>(okresult.);
            Assert.Equal(review, okresult.Value);

        }




}