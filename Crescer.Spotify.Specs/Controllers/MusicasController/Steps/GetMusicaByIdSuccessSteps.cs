using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using TechTalk.SpecFlow;

namespace Crescer.Spotify.Specs.Steps
{
    [Binding]
    public class GetMusicaByIdSuccessSteps
    {
        private MusicasController controller;
        private string givenId;
        private string resultId;

        [Before]
        public void Before()
        {
            Mock<IMusicaRepository> mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            Mock<MusicaService> mockRepoMusicaService = new Mock<MusicaService>(mockRepoMusicaRepository.Object);

            controller = new MusicasController(mockRepoMusicaRepository.Object, mockRepoMusicaService.Object);
        }

        [Given(@"I have the id ""(.*)""")]
        public void GivenIHaveTheId(string id)
        {
            givenId = id;
        }
        
        [When(@"I call GET music")]
        public void WhenICallGETMusic()
        {
            var result = controller.Get(givenId);

            resultId = result.ToBsonDocument().GetValue("id").ToString();
        }

        [Then(@"the result should be a music with the same ""(.*)"" id")]
        public void ThenTheResultShouldBeAMusicWithTheSameId(string expectedId)
        {
            Assert.AreSame(expectedId, resultId);
        }
    }
}
