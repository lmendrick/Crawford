using Unity.VisualScripting;

namespace Gab.Scripts
{
    [UnitTitle("On Variable Changed")]
    [UnitCategory("Events\\GabEvents")]
    public class GabEventVariableChanged : EventUnit<string>
    {
        [DoNotSerialize] public ValueOutput VariableName { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(GabEventNames.VariableChanged);
        }

        protected override void Definition()
        {
            base.Definition();
            VariableName = ValueOutput<string>(nameof(VariableName));
        }

        protected override void AssignArguments(Flow flow, string args)
        {
            //base.AssignArguments(flow, args);
            flow.SetValue(VariableName, args);
        }
    }
}