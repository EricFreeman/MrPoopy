using System.Collections.Generic;
using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour,
        IListener<LoseHealthMessage>
    {
        public int MaxHealth = 9;
        public List<Sprite> Frames;
        public Sprite DeadFrame;

        private int _currentHealth;

        void Start()
        {
            _currentHealth = MaxHealth;
            this.Register<LoseHealthMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<LoseHealthMessage>();
        }

        public void Handle(LoseHealthMessage message)
        {
            _currentHealth--;

            if (_currentHealth <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = DeadFrame;
                EventAggregator.SendMessage(new YouLoseMessage());
            }
            else
                GetComponent<SpriteRenderer>().sprite = Frames[MaxHealth - _currentHealth];
        }
    }
}