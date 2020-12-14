using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Specs.Bases
{
    public abstract class SharedSteps
    {
        protected readonly ScenarioContext _scenarioContext;

        public SharedSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
    }
}
