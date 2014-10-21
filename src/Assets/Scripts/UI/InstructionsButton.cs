using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class InstructionsButton : MonoBehaviour
    {
        public void Instructions()
        {
            EventAggregator.SendMessage(new OpenInstructionsMessage());
        }
    }
}