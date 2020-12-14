using Kerber.SpotifyLibrary.Application.SpecFlowTests.Common.Bases;
using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Application.SpecFlowTests.Controllers.AlbunsController.Steps
{
    [Binding, Scope(Feature = "Find album my ID on AlbunsController")]
    public class FindAlbumByIdSteps : BaseSteps
    {
        public FindAlbumByIdSteps(ScenarioContext scenarioContext) : base(scenarioContext) {}

        [Given(@"the album for that ID exists")]
        public void GivenTheAlbumForThatIDExists()
        {
            _scenarioContext.TryGetValue(ParameterNameGuide.MockAlbumRepository, out Mock<IAlbumRepository> mockAlbumRepository);
            _scenarioContext.TryGetValue(ParameterNameGuide.GivenId, out string givenId);

            var album = new Album("Album1", givenId);
            mockAlbumRepository
                .Setup(repo => repo.Obter(givenId))
                .Returns(album);
        }

        [Given(@"the album does not exist")]
        public void GivenTheAlbumDoesNotExist()
        {
            // The album does not exist so we do not need to set a mock
        }

        [When(@"I call GET album by ID")]
        public void WhenICallGETAlbumByID()
        {
            _scenarioContext.TryGetValue(ParameterNameGuide.GivenId, out string givenId);
            _scenarioContext.TryGetValue(ParameterNameGuide.AlbunsController, out WebApi.Controllers.AlbunsController albunsController);

            var result = albunsController.Get(givenId);

            int? responseCode = null;
            string resultId = null;
            switch (result)
            {
                case ObjectResult okObjectResult:
                    responseCode = okObjectResult.StatusCode;
                    resultId = (okObjectResult?.Value as Album)?.Id;
                    break;
                case NotFoundResult notFoundResult:
                    responseCode = notFoundResult.StatusCode;
                    break;
            }

            _scenarioContext[ParameterNameGuide.ReponseCode] = responseCode;
            _scenarioContext[ParameterNameGuide.ResultId] = resultId;
        }

        [Then(@"the result should be an album with the same ""(.*)"" id")]
        public void ThenTheResultShouldBeAnAlbumWithTheSameId(string expectedId)
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
