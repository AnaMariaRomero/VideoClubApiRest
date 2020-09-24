using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VideoClubApiRest.Infraestructure.Data;
using Xunit;

namespace XUnitTestVideoClub
{
    public class VideoClubDBContextFake
    {
        private DbContextOptions<VideoClubDBContext> options;
       public VideoClubDBContext GetDBContext() {
            var context = new VideoClubDBContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public DbContextOptions<VideoClubDBContext> GetDbContextOptions
        {
            get
            {
                var options = new DbContextOptionsBuilder<VideoClubDBContext>()
                                    .UseInMemoryDatabase(databaseName: "VideoClubDB")
                                    .Options;
                return options;
            } }
    }
}
