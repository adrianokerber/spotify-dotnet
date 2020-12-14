using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Application.SpecFlowTests.Common.Bases
{
    // TODO: common methods that are not attached to SpecFlow could be defined on Interfaces and implemented here
    public abstract class BaseSteps : SharedSteps
    {
        public BaseSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }
    }
}
