using Assets.Scripts.EventHandler;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pooper : MonoBehaviour,
        IListener<StartPoopingMessage>
    {
        public Sprite PoopingImage;
        private bool _isPooping;

        void Start()
        {
            this.Register<StartPoopingMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<StartPoopingMessage>();
        }

        void Update()
        {
            if (!_isPooping) return;
        }

        public void Handle(StartPoopingMessage message)
        {
            _isPooping = true;
            GetComponent<SpriteRenderer>().sprite = PoopingImage;
        }
    }
}