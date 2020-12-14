using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Application.SpecFlowTests.Common.Bases
{
    /// <summary>
    /// SharedSteps is the base classe for Context Injection of SpecFlow. Inherit from this class to have ScenarioContext object injected on your step classes.
    /// </summary>
    public abstract class SharedSteps
    {
        protected readonly ScenarioContext _scenarioContext;

        public SharedSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
    }
}
