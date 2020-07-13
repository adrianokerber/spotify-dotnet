using Kerber.SpotifyLibrary.Specs.SpecFlow_CalculatorExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Specs.Controllers.MusicasController.Steps
{
    [Binding]
    class PortugueseTestStep
    {
        private string manName;
        private string bullName;
        private string story;

        [Given(@"que um homem de nome ""(.*)""")]
        public void DadoQueUmHomemDeNome(string name)
        {
            manName = name;
        }

        [Given(@"um touro chamado ""(.*)""")]
        public void DadoUmTouroChamado(string name)
        {
            bullName = name;
        }

        [When(@"andando pela rua se encontram")]
        public void QuandoAndandoPelaRuaSeEncontram()
        {
            story = ManAndBullPhraseGenerator.Phrase(manName, bullName);
        }

        [Then(@"resulta na frase ""(.*)""")]
        public void EntaoResultaNaFrase(string expectedStory)
        {
            Assert.AreEqual(expectedStory, story);
        }

    }
}
