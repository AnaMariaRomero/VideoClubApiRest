using System;
using VideoClubApiRest.Api.Controllers;
using Xunit;

namespace XUnitTestProjectVideoClub
{
    [TestClass]
    public class UnitTest1
    {
        MockRentsRepository rentsRepository;
        RentsController rentsController;

        [TestInitialize]
        public void InitializerForTest() {
            rentsRepository = new MockRentsRepository();
            rentsController = new RentsController(rentsRepository);
        }
    }
}
