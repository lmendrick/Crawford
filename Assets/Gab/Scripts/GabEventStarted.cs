using Unity.VisualScripting;

namespace Gab.Scripts
{
    [UnitTitle("On Conversation Started")]
    [UnitCategory("Events\\GabEvents")]
    public class GabEventStarted : EventUnit<string>
    {
        [DoNotSerialize] public ValueOutput ConversationName { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(GabEventNames.ConversationStarted);
        }

        protected override void Definition()
        {
            base.Definition();
            ConversationName = ValueOutput<string>(nameof(ConversationName));
        }

        protected override void AssignArguments(Flow flow, string args)
        {
            //base.AssignArguments(flow, args);
            flow.SetValue(ConversationName, args);
        }
    }
}