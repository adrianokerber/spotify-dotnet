using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Specs.Controllers.MusicasController.Steps
{
    [Binding]
    public class FindMusicaByIdSteps
    {
        Mock<IMusicaRepository> mockRepoMusicaRepository;
        private WebApi.Controllers.MusicasController musicasController;
        private string givenId;
        private string resultId;
        private int? responseCode;

        [Before]
        public void Before()
        {
            mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            Mock<MusicaService> mockRepoMusicaService = new Mock<MusicaService>(mockRepoMusicaRepository.Object);
            Mock<ILogger<WebApi.Controllers.MusicasController>> mockRepoLogger = new Mock<ILogger<WebApi.Controllers.MusicasController>>();

            musicasController = new WebApi.Controllers.MusicasController(mockRepoMusicaRepository.Object, mockRepoMusicaService.Object, mockRepoLogger.Object);
        }

        [Given(@"I have the id ""(.*)""")]
        public void GivenIHaveTheId(string id)
        {
            givenId = id;
        }

        [Given(@"The song for that id exists")]
        public void GivenTheSongForThatIdExists()
        {
            var music = new Musica("Music1", 0.0, givenId);
            mockRepoMusicaRepository
                .Setup(repo => repo.Obter(givenId))
                .Returns(music);
        }

        [Given(@"The song does not exist")]
        public void GivenTheSongDoesNotExist()
        {
            // The song does not exist so we do not need to set a mock
        }

        [When(@"I call GET song")]
        public void WhenICallGETSong()
        {
            var result = musicasController.Get(givenId);

            switch (result)
            {
                case ObjectResult okObjectResult:
                    responseCode = okObjectResult.StatusCode;
                    resultId = (okObjectResult?.Value as Musica)?.Id;
                    break;
                case NotFoundResult notFoundResult:
                    responseCode = notFoundResult.StatusCode;
                    break;
            }
        }

        [Then(@"the result should be a song with the same ""(.*)"" id")]
        public void ThenTheResultShouldBeASongWithTheSameId(string expectedId)
        {
            Assert.AreEqual(expectedId, resultId);
        }

        [Then(@"the result should be null")]
        public void ThenTheResultShouldBeNull()
        {
            Assert.IsNull(resultId);
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedResponseCode)
        {
            Assert.AreEqual(expectedResponseCode, responseCode);
        }



    }
}
