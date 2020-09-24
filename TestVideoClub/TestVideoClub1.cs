using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoClubApiRest.Api.Controllers;
using VideoClubApiRest.Core.Entities;
using VideoClubApiRest.Core.Interfaces;
using VideoClubApiRest.Infraestructure.Data;
using VideoClubApiRest.Infraestructure.Repositories;
using Xunit;

namespace TestVideoClub
{ 
    public class TestVideoClub1
    {
        private VideoClubDBContext CreateDatabase()
        {
            DbContextOptions<VideoClubDBContext> options;
            var builder = new DbContextOptionsBuilder<VideoClubDBContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString("N"));
            options = builder.Options;
            VideoClubDBContext videoClubDBContext = new VideoClubDBContext(options);
            videoClubDBContext.Database.EnsureDeleted();
            videoClubDBContext.Database.EnsureCreated();

            return videoClubDBContext;

        }
        [Fact]
        public async Task It_should_add_a_rent_successfully_into_dataBaseAsync()
        {
            var dbContext = CreateDatabase();
            //Arrange
            var sut = new RentsRepository(dbContext);
            var rent = new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            await sut.InsertRents(rent);

            //Assert
            Assert.NotNull(sut);
            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public void It_should_get_a_rent_successfully_into_dataBaseAsync()
        {
            var dbContext = CreateDatabase();
            //Arrange
            var sut = new RentsRepository(dbContext);
            var rent = new Rents { RentId = 2, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            sut.InsertRents(rent);
            var rent1 = new Rents { RentId = 3, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            sut.InsertRents(rent1);
            
            var rentResult = sut.GetRents();
            Assert.NotNull(rentResult);

            //Assert
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<Task<IEnumerable<Rents>>>(rentResult);

            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task It_should_add_a_rent_With_RentsController_Result_Ok()
        {
            var dbContext = CreateDatabase();
           
            //Arrange
            var sut = new RentsRepository(dbContext);
            var controller = new RentsController(sut);
            var rent = new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            //controller.InsertRents(rent);
            var rent1 = new Rents { RentId = 2, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            //controller.InsertRents(rent1);
            var rent2 = new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            var result = await controller.InsertRents(rent2);
            var result2 =  controller.InsertRents(rent1);
            ObjectResult result1 = result as ObjectResult;
            //Assert
            await Assert.IsAssignableFrom<Task<IActionResult>>(result2);
            Assert.NotEqual(201, result1.StatusCode);
            
            
            
            
            //Clean up
            dbContext.Dispose();
        }

        [Fact]
        public async Task It_should_add_a_rent_With_Data_Exist_RentsController_Result_Fails()
        {
            var dbContext = CreateDatabase();

            //Arrange
            var sut = new RentsRepository(dbContext);
            var controller = new RentsController(sut);
            var rent = new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            await controller.InsertRents(rent);
            var rent1 = new Rents { RentId = 2, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
            //controller.InsertRents(rent1);
            var rent2 = new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" };
            //Act
           
            await controller.InsertRents(rent1);

            await Assert.ThrowsAsync< InvalidOperationException>(() => controller.InsertRents(rent2));
            //Clean up
            dbContext.Dispose();
        }
    }
}

