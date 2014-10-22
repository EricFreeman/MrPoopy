using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using Assets.Scripts.Util;
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

        public AudioClip StartLevel;
        public AudioClip EndLevel;
        public AudioClip Yummy;

        [HideInInspector]
        public bool IsDead;
        public float Speed = 5;
        private bool _isStarted;
        private float _smileTime;
        private int _nextYummy;

        void Start()
        {
            this.Register<YouLoseMessage>();
            this.Register<CollectedPoopMessage>();
            _nextYummy = Random.Range(0, 5);
        }

        void OnDestroy()
        {
            this.UnRegister<YouLoseMessage>();
            this.UnRegister<CollectedPoopMessage>();
        }

        void Update()
        {
            if (IsDead) return;

            _smileTime--;
            if (_smileTime > 0) GetComponent<SpriteRenderer>().sprite = Smiling;
            else if(_isStarted) GetComponent<SpriteRenderer>().sprite = OpenMouth;

            ApplyMovementSpeed();

            if ((Input.GetKey(KeyCode.Space) || Input.touchCount > 0) && !_isStarted)
            {
                EventAggregator.SendMessage(new StartPoopingMessage());
                this.PlaySound(StartLevel);
                GetComponent<SpriteRenderer>().sprite = OpenMouth;
                _isStarted = true;
            }
        }

        private void ApplyMovementSpeed()
        {
            var speed = Input.GetAxisRaw("Horizontal");
            var grossSpeed = speed + Input.acceleration.x;

            transform.Translate(grossSpeed * Speed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.8f, 3.5f), -3.5f, 0);
        }

        public void Handle(YouLoseMessage message)
        {
            if (IsDead) return;

            IsDead = true;
            GetComponent<SpriteRenderer>().sprite = ClosedMouth;
            this.PlaySound(EndLevel);
        }

        public void Handle(CollectedPoopMessage message)
        {
            _smileTime = 50;
            if((_nextYummy--) % 5 == 0)
                this.PlaySound(Yummy);
        }
    }
}