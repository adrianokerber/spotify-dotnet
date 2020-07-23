using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
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
        private string _expectedListOfSongs;
        private int _responseCode;

        // TODO: rename all 'musics' to 'songs' since music is uncountable and there is no plural

        public void PrepareTestServerAndDependencies()
        {
            Mock<IMusicaRepository> mockRepoMusicaRepository = new Mock<IMusicaRepository>();

            var music = new Musica("Music1", 1.0, "1"); // TODO: get songs from _givenListOfSongs
            mockRepoMusicaRepository
                .Setup(repo => repo.Obter("1"))
                .Returns(music);

            Action<IServiceCollection> services = (services => {
                services.AddScoped<IMusicaRepository>(x => mockRepoMusicaRepository.Object);
            });

            var server = TestServerHelper.CreateTestServer(services);
            
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

            var message = new HttpRequestMessage(HttpMethod.Get, $"api/musicas");
            var response = _client.SendAsync(message).GetAwaiter().GetResult();

            var responseContentAsString = response.Content.ReadAsStringAsync().Result;
            //_expectedListOfSongs = JsonConvert.DeserializeObject<List<Musica>>(responseContentAsString);
            _responseCode = (int)response.StatusCode;
        }

        [Then(@"the result should be these songs (.*)")]
        public void ThenTheResultShouldBeTheseSongs(string expectedSongListJsonFilename)
        {
            var expectedResponse = new StreamReader(@$"{_resourcesPath}\{expectedSongListJsonFilename}").ReadToEnd();

            JToken expected = JToken.Parse(expectedResponse);
            JToken actual = JToken.Parse(_expectedListOfSongs); // TODO: review condition since we must have the actual value here

            Assert.AreEqual(expected, actual);
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedResponseCode)
        {
            Assert.AreEqual(expectedResponseCode, _responseCode);
        }
    }
}
