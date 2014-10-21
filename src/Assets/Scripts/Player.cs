using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using Assets.Scripts.UI;
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

        [HideInInspector]
        public bool IsDead;
        public float Speed = 5;
        private bool _isStarted;
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

            ApplyMovementSpeed();

            if (Input.GetKey(KeyCode.Space) && !_isStarted)
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
            var tiltSpeed = GetTiltSpeed();
            var grossSpeed = speed + tiltSpeed.x;

            transform.Translate(grossSpeed * Speed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.8f, 3.5f), -3.5f, 0);
        }

        private Vector3 GetTiltSpeed()
        {
            var dir = Vector3.zero;
            dir.x = -Input.acceleration.y;
            dir.z = Input.acceleration.x;
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            dir *= Time.deltaTime;

            return dir;
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
        }
    }
}