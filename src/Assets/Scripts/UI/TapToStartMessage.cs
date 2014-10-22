using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TapToStartMessage : MonoBehaviour,
        IListener<StartPoopingMessage>
    {
        void Start()
        {
            this.Register<StartPoopingMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<StartPoopingMessage>();
        }

        public void Handle(StartPoopingMessage message)
        {
            gameObject.SetActive(false);
        }
    }
}