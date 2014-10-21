using System.Collections.Generic;
using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using Assets.Scripts.UI;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pooper : MonoBehaviour,
        IListener<StartPoopingMessage>,
        IListener<YouLoseMessage>
    {
        public Sprite StandingImage;
        public Sprite PoopingImage;
        public GameObject Poop;
        public List<AudioClip> FartSounds;

        public float MinPoopDelay;
        public float MaxPoopDelay;

        private bool _isPooping;
        private float _poopDelay;

        void Start()
        {
            this.Register<StartPoopingMessage>();
            this.Register<YouLoseMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<StartPoopingMessage>();
            this.UnRegister<YouLoseMessage>();
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
            this.PlaySound(FartSounds);
        }

        public void Handle(StartPoopingMessage message)
        {
            if (_isPooping) return;

            _isPooping = true;
            GetComponent<SpriteRenderer>().sprite = PoopingImage;
            _poopDelay = Random.Range(MinPoopDelay, MaxPoopDelay);
        }

        public void Handle(YouLoseMessage message)
        {
            GetComponent<SpriteRenderer>().sprite = StandingImage;
            _isPooping = false;
        }
    }
}