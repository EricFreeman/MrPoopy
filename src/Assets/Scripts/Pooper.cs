using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pooper : MonoBehaviour,
        IListener<StartPoopingMessage>
    {
        public Sprite PoopingImage;
        public GameObject Poop;

        public float MinPoopDelay;
        public float MaxPoopDelay;

        private bool _isPooping;
        private float _poopDelay;

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

            _poopDelay--;
            if (_poopDelay < 0)
                SpawnPoop();
        }

        private void SpawnPoop()
        {
            _poopDelay = Random.Range(MinPoopDelay, MaxPoopDelay);
            var p = (GameObject)Instantiate(Poop);
            p.transform.position = transform.position;
        }

        public void Handle(StartPoopingMessage message)
        {
            if (_isPooping) return;

            _isPooping = true;
            GetComponent<SpriteRenderer>().sprite = PoopingImage;
            _poopDelay = Random.Range(MinPoopDelay, MaxPoopDelay);
        }
    }
}