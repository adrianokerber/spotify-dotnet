using Kerber.SpotifyLibrary.Application.SpecFlowTests.Common.Bases;
using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Application.SpecFlowTests.CommonSteps
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

            /////
            var mockAlbumRepository = new Mock<IAlbumRepository>();
            var mockAlbumService = new Mock<AlbumService>(mockAlbumRepository.Object, mockMusicaService.Object);

            _scenarioContext[ParameterNameGuide.MockAlbumRepository] = mockAlbumRepository;
            _scenarioContext[ParameterNameGuide.MockAlbumService] = mockAlbumService;

            _scenarioContext[ParameterNameGuide.AlbunsController] = new WebApi.Controllers.AlbunsController(mockAlbumRepository.Object, mockAlbumService.Object);
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
