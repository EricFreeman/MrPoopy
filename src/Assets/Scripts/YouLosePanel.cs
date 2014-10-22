using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class YouLosePanel : MonoBehaviour,
        IListener<YouLoseMessage>
    {
        public int FadeAmount;
        private bool _isFading;

        void Start()
        {
            this.Register<YouLoseMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<YouLoseMessage>();
        }

        void Update()
        {
            if (!_isFading) return;
            if (FadeAmount < 255) FadeAmount++;

            GetComponent<Image>().color = new Color(255, 255, 255, FadeAmount/255.0f);
        }

        public void GoToMainMenu()
        {
            if(FadeAmount >= 200)
                Application.LoadLevel("MainMenu");
        }

        public void Handle(YouLoseMessage message)
        {
            _isFading = true;
        }
    }
}