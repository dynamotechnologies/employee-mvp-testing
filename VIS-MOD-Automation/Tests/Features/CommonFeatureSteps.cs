using StepDefinitions.UI;
using TechTalk.SpecFlow;

namespace Tests.Features
{
    public class CommonFeatureSteps //: CommonUiSteps
    {
        public class CommonFeatureSteps1 : CommonUiSteps {}
        public class CommonFeatureSteps2 : TableSteps {}
        public class CommonFeatureSteps3 : TextBoxSteps {}
        public class CommonFeatureSteps4 : ButtonSteps {}
        public class CommonFeatureSteps5 : CheckBoxSteps { }
        public class CommonFeatureSteps6 : SelectDropDownSteps { }
    }
}
