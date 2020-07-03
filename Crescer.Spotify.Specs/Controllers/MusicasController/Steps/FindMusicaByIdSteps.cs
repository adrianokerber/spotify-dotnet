using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;

namespace Crescer.Spotify.Specs.Steps
{
    [Binding]
    public class FindMusicaByIdSteps
    {
        Mock<IMusicaRepository> mockRepoMusicaRepository;
        private MusicasController musicasController;
        private string givenId;
        private string resultId;
        private int? responseCode;

        [Before]
        public void Before()
        {
            mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            Mock<MusicaService> mockRepoMusicaService = new Mock<MusicaService>(mockRepoMusicaRepository.Object);

            musicasController = new MusicasController(mockRepoMusicaRepository.Object, mockRepoMusicaService.Object);
        }

        [Given(@"I have the id ""(.*)""")]
        public void GivenIHaveTheId(string id)
        {
            givenId = id;
        }

        [Given(@"The music for that id exists")]
        public void GivenTheMusicForThatIdExists()
        {
            var music = new Musica("Music1", 0.0, givenId);
            mockRepoMusicaRepository
                .Setup(repo => repo.Obter(givenId))
                .Returns(music);
        }

        [Given(@"The music does not exist")]
        public void GivenTheMusicDoesNotExist()
        {
            // The music does not exist so we do not need to set a mock
        }

        [When(@"I call GET music")]
        public void WhenICallGETMusic()
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

        [Then(@"the result should be a music with the same ""(.*)"" id")]
        public void ThenTheResultShouldBeAMusicWithTheSameId(string expectedId)
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
