using System.Collections.Generic;
using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Scoreboard : MonoBehaviour,
        IListener<CollectedPoopMessage>
    {
        public GameObject Hundreds;
        public GameObject Tens;
        public GameObject Ones;

        public List<Sprite> Frames;

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            { 
                _score = value;
                UpdateScoreboard();
            }
        }

        void Start()
        {
            this.Register<CollectedPoopMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<CollectedPoopMessage>();
        }

        private void UpdateScoreboard()
        {
            Hundreds.GetComponent<SpriteRenderer>().sprite = Frames[Score / 100 % 10];
            Tens.GetComponent<SpriteRenderer>().sprite = Frames[Score / 10 % 10];
            Ones.GetComponent<SpriteRenderer>().sprite = Frames[Score / 1 % 10];
        }

        public void Handle(CollectedPoopMessage message)
        {
            Score++;
        }
    }
}