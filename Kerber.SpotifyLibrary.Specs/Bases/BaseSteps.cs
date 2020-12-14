using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Kerber.SpotifyLibrary.Specs.Bases
{
    // TODO: common methods that are not attached to SpecFlow could be defined on Interfaces and implemented here
    public abstract class BaseSteps : SharedSteps
    {
        public BaseSteps(ScenarioContext scenarioContext) : base(scenarioContext) {}
    }
}
