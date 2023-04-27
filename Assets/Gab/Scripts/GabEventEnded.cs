using Unity.VisualScripting;

namespace Gab.Scripts
{
    [UnitTitle("On Conversation Ended")]
    [UnitCategory("Events\\GabEvents")]
    public class GabEventEnded : EventUnit<bool>
    {
        [DoNotSerialize] public ValueOutput VariableChangesMade { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(GabEventNames.ConversationEnded);
        }

        protected override void Definition()
        {
            base.Definition();
            VariableChangesMade = ValueOutput<bool>(nameof(VariableChangesMade));
        }

        protected override void AssignArguments(Flow flow, bool args)
        {
            //base.AssignArguments(flow, args);
            flow.SetValue(VariableChangesMade, args);
        }
    }
}
