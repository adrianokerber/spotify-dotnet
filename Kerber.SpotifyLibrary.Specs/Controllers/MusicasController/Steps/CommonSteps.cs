using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.Linq;
using Moq;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kerber.SpotifyLibrary.Specs.Bases;
using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Microsoft.Extensions.Logging;

namespace Kerber.SpotifyLibrary.Specs.Controllers
{
    [Binding]
    public class CommonSteps : SharedSteps
    {
        public CommonSteps(ScenarioContext scenarioContext) : base(scenarioContext) {}

        [Before]
        public void Before()
        {
            var mockMusicaRepository = new Mock<IMusicaRepository>();
            var mockMusicaService = new Mock<MusicaService>(mockMusicaRepository.Object);

            _scenarioContext[ParameterNameGuide.MockMusicaRepository] = mockMusicaRepository;
            _scenarioContext[ParameterNameGuide.MockMusicaService] = mockMusicaService;
            
            var mockRepoLogger = new Mock<ILogger<WebApi.Controllers.MusicasController>>();

            _scenarioContext[ParameterNameGuide.MusicasController] = new WebApi.Controllers.MusicasController(mockMusicaRepository.Object, mockMusicaService.Object, mockRepoLogger.Object);
        }

        [Given(@"I have the id ""(.*)""")]
        public void GivenIHaveTheId(string id)
        {
            _scenarioContext[ParameterNameGuide.GivenId] = id;
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedResponseCode)
        {
            _scenarioContext.TryGetValue(ParameterNameGuide.ReponseCode, out int responseCode);

            Assert.AreEqual(expectedResponseCode, responseCode);
        }
    }
}
