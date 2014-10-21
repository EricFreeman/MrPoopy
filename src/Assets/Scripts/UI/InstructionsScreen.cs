using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class InstructionsScreen : MonoBehaviour
    {
        public void GoBack()
        {
            EventAggregator.SendMessage(new CloseInstructionsMessage());
        }
    }
}