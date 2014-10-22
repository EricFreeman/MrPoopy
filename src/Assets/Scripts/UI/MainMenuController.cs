using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour,
        IListener<OpenInstructionsMessage>,
        IListener<CloseInstructionsMessage>
    {
        public GameObject MainMenu;
        public GameObject Instructions;

        void Start()
        {
            this.Register<OpenInstructionsMessage>();
            this.Register<CloseInstructionsMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<OpenInstructionsMessage>();
            this.UnRegister<CloseInstructionsMessage>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape)) Application.Quit();
        }

        public void Handle(OpenInstructionsMessage message)
        {
            MainMenu.SetActive(false);
            Instructions.SetActive(true);
        }

        public void Handle(CloseInstructionsMessage message)
        {
            MainMenu.SetActive(true);
            Instructions.SetActive(false);
        }
    }
}