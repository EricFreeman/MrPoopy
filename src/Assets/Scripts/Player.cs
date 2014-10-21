﻿using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour,
        IListener<YouLoseMessage>,
        IListener<CollectedPoopMessage>
    {
        public Sprite ClosedMouth;
        public Sprite OpenMouth;
        public Sprite Smiling;

        public float Speed = 5;
        private bool _isStarted;
        [HideInInspector]
        public bool IsDead;
        private float _smileTime;

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
            if (IsDead) return;

            _smileTime--;
            if (_smileTime > 0) GetComponent<SpriteRenderer>().sprite = Smiling;
            else if(_isStarted) GetComponent<SpriteRenderer>().sprite = OpenMouth;

            transform.Translate(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.8f, 3.5f), -3.5f, 0);

            if (Input.GetKey(KeyCode.Space))
            {
                EventAggregator.SendMessage(new StartPoopingMessage());
                GetComponent<SpriteRenderer>().sprite = OpenMouth;
                _isStarted = true;
            }
        }

        public void Handle(YouLoseMessage message)
        {
            IsDead = true;
            GetComponent<SpriteRenderer>().sprite = ClosedMouth;
        }

        public void Handle(CollectedPoopMessage message)
        {
            _smileTime = 50;
        }
    }
}