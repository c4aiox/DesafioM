using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Api.Controllers;
using BikeRental.Api.Data;
using BikeRental.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace BikeRental.Api.Tests.Controllers
{

    public class MotorcycleControllerTest
    {

        private readonly Mock<ILogger<MotorcycleController>> loggerMock = new();
        private readonly AppDbContext context;

        public MotorcycleControllerTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "MotorcycleTestDb")
               .Options;
            context = new AppDbContext(options);
        }

        [Fact]
        public void GetMotorcycles_ReturnsOk_WhenMotorcyclesExist()
        {
            // Arrange            
            var motorcycles = new List<Motorcycle>
            {
                new Motorcycle {Id = "001", Model = "ModelA", Plate = "ABC123" }
            };
            context.Motorcycles.AddRange(motorcycles);
            context.SaveChanges();
            var controller = new MotorcycleController(context, loggerMock.Object);

            // Act
            var result = controller.GetMotorcycles();

            // Assert
            var ok = result as OkObjectResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
            var motorcyclesReturned = ok.Value as List<Motorcycle>;
            Assert.Equivalent(motorcycles, motorcyclesReturned);
        }

        [Fact]
        public void GetMotorcycles_ReturnsNotFound_WhenNoMotorcyclesExist()
        {
            // Arrange
            context.Motorcycles.RemoveRange(context.Motorcycles);
            context.SaveChanges();
            var controller = new MotorcycleController(context, loggerMock.Object);


            // Act
            var result = controller.GetMotorcycles();

            // Assert
            var notfound = result as NotFoundObjectResult;
            Assert.NotNull(notfound);
            Assert.Equal(404, notfound.StatusCode);
            Assert.Equal("No motorcycles found.", notfound.Value);
        }

        [Fact]
        public void CreateMotorcycle_ShouldSaveMotorcycle()
        {
            // Arrange
            var controller = new MotorcycleController(context, loggerMock.Object);

            // Act
            var newMotorcycle = new Motorcycle { Id = "002", Model = "ModelB", Plate = "XYZ789" };
            var result = controller.CreateMotorcycle(newMotorcycle);

            // Assert
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
            var savedMotorcycle = context.Motorcycles.Find("002");
            Assert.NotNull(savedMotorcycle);
        }

        [Fact]
        public void CreateMotorcycle_WhenPlateIsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = new MotorcycleController(context, loggerMock.Object);

            // Act
            var newMotorcycle = new Motorcycle { Id = "003", Model = "ModelC", Plate = null };
            var result = controller.CreateMotorcycle(newMotorcycle);

            // Assert
            var badrequest = result as BadRequestObjectResult;
            Assert.NotNull(badrequest);
            Assert.Equal(400, badrequest.StatusCode);
            Assert.Equal("Plate is required.", badrequest.Value);
        }

        [Fact]
        public void CreateMotorcycle_WhenModelIsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = new MotorcycleController(context, loggerMock.Object);

            // Act
            var newMotorcycle = new Motorcycle { Id = "004", Model = null, Plate = "XYD2342" };
            var result = controller.CreateMotorcycle(newMotorcycle);

            // Assert
            var badrequest = result as BadRequestObjectResult;
            Assert.NotNull(badrequest);
            Assert.Equal(400, badrequest.StatusCode);
            Assert.Equal("Model is required.", badrequest.Value);
        }

        [Fact]
        public void CreateMotorcycle_WhenIdIsNull_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = new MotorcycleController(context, loggerMock.Object);

            // Act
            var newMotorcycle = new Motorcycle { Model = "ModelC", Plate = "ABCD" };
            var result = controller.CreateMotorcycle(newMotorcycle);

            // Assert
            var badrequest = result as BadRequestObjectResult;
            Assert.NotNull(badrequest);
            Assert.Equal(400, badrequest.StatusCode);
            Assert.Equal("Id is required.", badrequest.Value);
        }

    }
}