using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Specs.ServerIntegration.MusicasController.Steps
{
    [Binding, Scope(Feature = "FindAllMusic from MusicasController")]
    class FindAllMusicSteps
    {
        private const string _resourcesPath = @".\ServerIntegration\MusicasController\TestData";

        private List<Musica> _givenListOfSongs;
        private HttpClient _client;
        private string _responseListOfSongs;
        private int _responseCode;

        // TODO: rename all 'musics' to 'songs' since music is uncountable and there is no plural

        public void PrepareTestServerAndDependencies()
        {
            // Set up mocks
            Mock<IMusicaRepository> mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            // TODO: move the mock values to proper step
            mockRepoMusicaRepository
                .Setup(repo => repo.ListarMusicas())
                .Returns(_givenListOfSongs);

            var server = TestServerHelper.CreateTestServer(services => {
                services.AddScoped<IMusicaRepository>(x => mockRepoMusicaRepository.Object);
                services.AddScoped<MusicaService, MusicaService>();
            });
            
            _client = server.CreateClient();
        }

        [Given(@"I have these songs (.*)")]
        public void GivenIHaveTheseSongs(string songListJsonFilename)
        {
            var givenJsonFile = new StreamReader(@$"{_resourcesPath}\{songListJsonFilename}").ReadToEnd();

            _givenListOfSongs = JsonConvert.DeserializeObject<List<Musica>>(givenJsonFile);
        }

        [When(@"I call GET all songs")]
        public void WhenICallGETAllSongs()
        {
            PrepareTestServerAndDependencies();

            // TODO: review why the request to 'GET: api/musicas' is always failing
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/musicas");
            var response = _client.SendAsync(message).GetAwaiter().GetResult();

            _responseListOfSongs = response.Content.ReadAsStringAsync().Result;
            _responseCode = (int)response.StatusCode;
        }

        [Then(@"the result should be these songs (.*)")]
        public void ThenTheResultShouldBeTheseSongs(string expectedSongListJsonFilename)
        {
            var expectedResponse = new StreamReader(@$"{_resourcesPath}\{expectedSongListJsonFilename}").ReadToEnd();

            JToken expected = JToken.Parse(expectedResponse);
            JToken actual = JToken.Parse(_responseListOfSongs);

            Assert.AreEqual(expected, actual);
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedResponseCode)
        {
            Assert.AreEqual(expectedResponseCode, _responseCode);
        }
    }
}
