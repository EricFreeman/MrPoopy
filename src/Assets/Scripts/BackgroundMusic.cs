using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class BackgroundMusic : MonoBehaviour 
    {
        public List<AudioClip> Songs;
        private int _lastSong;

        void Start()
        {
            // so it won't only play first song evey time level starts
            _lastSong = -1;

            DontDestroyOnLoad(gameObject);
            if (!AlreadyExists())
                PlayNewSong();
        }

        void Update()
        {
            if (!GetComponent<AudioSource>().isPlaying) PlayNewSong();
        }

        private bool AlreadyExists()
        {
            // because this object isn't destroyed when switching levels,
            // there should already be one in the scene on load unless
            // you're starting a specific level from the editor

            if (FindObjectsOfType<BackgroundMusic>().ToList().Count() > 1)
            {
                DestroyImmediate(gameObject);
                return true;
            }

            return false;
        }

        private void PlayNewSong()
        {
            var newSong = _lastSong;
            var rand = new System.Random(DateTime.Now.Millisecond);

            while (newSong == _lastSong)
                newSong = rand.Next(0, Songs.Count);

            GetComponent<AudioSource>().clip = Songs[newSong];
            GetComponent<AudioSource>().Play();
            _lastSong = newSong;
        }
    }
}
