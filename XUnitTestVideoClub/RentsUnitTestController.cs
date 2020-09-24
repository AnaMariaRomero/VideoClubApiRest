using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VideoClubApiRest.Api.Controllers;
using VideoClubApiRest.Core.Entities;
using VideoClubApiRest.Infraestructure.Repositories;

namespace XUnitTestVideoClub
{
    [TestClass]
    public class RentsUnitTestController
    {
       private VideoClubDBContextFake _videoClubDBContextFake;
        [TestInitialize]
        public void Init()
        {
            _videoClubDBContextFake = new VideoClubDBContextFake();
        }

        [TestMethod]
        public void GetRents_Ok_Result() {
            
            using (var context = _videoClubDBContextFake.GetDBContext()) {
                //ARANGE
                context.Rents.Add(new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" });
                context.SaveChanges();
                }
            using (var context = _videoClubDBContextFake.GetDBContext()) {
                //ACT
                var rentsRepository = new RentsRepository(context);
                var result = rentsRepository.GetRents();


                Assert.Equals(1, result.Result);
            }
        }

        
    }
}
