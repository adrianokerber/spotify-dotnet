using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Specs.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Specs.Controllers.MusicasController.Steps
{
    [Binding, Scope(Feature = "Find song by ID on MusicasController")]
    public class FindMusicaByIdSteps : BaseSteps
    {
        public FindMusicaByIdSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [Given(@"The song for that id exists")]
        public void GivenTheSongForThatIdExists()
        {
            _scenarioContext.TryGetValue(ParameterNameGuide.MockMusicaRepository, out Mock<IMusicaRepository> mockMusicaRepository);
            _scenarioContext.TryGetValue(ParameterNameGuide.GivenId, out string givenId);

            var music = new Musica("Music1", 0.0, givenId);
            mockMusicaRepository
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
            _scenarioContext.TryGetValue(ParameterNameGuide.GivenId, out string givenId);
            _scenarioContext.TryGetValue(ParameterNameGuide.MusicasController, out WebApi.Controllers.MusicasController musicasController);

            var result = musicasController.Get(givenId);

            int? responseCode = null;
            string resultId = null;
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

            _scenarioContext[ParameterNameGuide.ReponseCode] = responseCode;
            _scenarioContext[ParameterNameGuide.ResultId] = resultId;
        }

        [Then(@"the result should be a song with the same ""(.*)"" id")]
        public void ThenTheResultShouldBeASongWithTheSameId(string expectedId)
        {
            _scenarioContext.TryGetValue(ParameterNameGuide.ResultId, out string resultId);

            Assert.AreEqual(expectedId, resultId);
        }

        [Then(@"the result should be null")]
        public void ThenTheResultShouldBeNull()
        {
            _scenarioContext.TryGetValue(ParameterNameGuide.ResultId, out string resultId);

            Assert.IsNull(resultId);
        }
    }
}
