using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour,
        IListener<YouLoseMessage>
    {
        public Sprite OpenMouth;
        public Sprite Smiling;

        public float Speed = 5;
        private bool _isDead;

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
            if (_isDead) return;

            transform.Translate(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.8f, 3.5f), -3.5f, 0);

            if (Input.GetKey(KeyCode.Space))
            {
                EventAggregator.SendMessage(new StartPoopingMessage());
                GetComponent<SpriteRenderer>().sprite = OpenMouth;
            }
        }

        public void Handle(YouLoseMessage message)
        {
            _isDead = true;
        }
    }
}