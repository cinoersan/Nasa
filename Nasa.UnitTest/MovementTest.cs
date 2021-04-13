using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Nasa.Business.Exceptions;
using Nasa.Business.Profiles;
using Nasa.Business.Repositories.Configuration;
using Nasa.Business.Services.Commands;
using Nasa.Model.Config;
using Nasa.Model.Keys;
using Nasa.UnitTest.Mocks;
using NUnit.Framework;

namespace Nasa.UnitTest
{
    [TestFixture]
    public class Tests
    {
        private Mock<IConfigurationRepository> _configurationRepository;
        private ICommandService _commandService;
        [SetUp]
        public void Setup()
        {
            _configurationRepository = new Mock<IConfigurationRepository>();
            _configurationRepository.Setup(t => t.GetConfig<Plateau>(ConfigKeys.Plateau))
                .ReturnsAsync(NasaConfigMock.Plateau);
            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LocationProfile());
            });

            var mapper = config.CreateMapper();
            _commandService = new CommandService(_configurationRepository.Object, mapper);
        }

        [Test]
        public async Task CheckCorrectMovement()
        {
            var items = RoverMocks.OkTest;
            var result = await _commandService.MoveAll(items);
            var firstRover = result.Rovers.FirstOrDefault()?.CurrentCoordinate;
            var lastRover = result.Rovers.LastOrDefault()?.CurrentCoordinate;
            Assert.AreEqual(1, firstRover?.X ?? 0);
            Assert.AreEqual(3, firstRover?.Y ?? 0);
            Assert.AreEqual(5, lastRover?.X ?? 0);
            Assert.AreEqual(1, lastRover?.Y ?? 0);
        }
        [Test]
        public void CheckProblematicMovement()
        {
            var items = RoverMocks.CrashTest;
            Func<Task> result = async () => await _commandService.MoveAll(items);
            result.Should().Throw<BadRequestException>()
                .And.Message.Should().StartWith("With this mapping there will be crash at coordinate");

        }
    }
}