﻿using System.Collections.Generic;
using Assets.Scripts.EventHandler;
using Assets.Scripts.EventHandler.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Poop : MonoBehaviour
    {
        public List<Sprite> Falling;
        public List<Sprite> Splatter;

        private int _frame;
        private float _speed;
        private bool _isFalling;
        private bool _isDestroyed;

        void Start()
        {
            _frame = Random.Range(0, Falling.Count);
            GetComponent<SpriteRenderer>().sprite = Falling[_frame];
            _speed = Random.Range(2, 4);
            _isFalling = true;
        }

        void Update()
        {
            if (_isDestroyed)
            {
                Destroy(gameObject);
                return;
            }

            if (!_isFalling) return;

            transform.Translate(0, -_speed * Time.deltaTime, 0);
            if (transform.position.y <= -4)
            {
                _isFalling = false;
                GetComponent<SpriteRenderer>().sprite = Splatter[_frame];
                EventAggregator.SendMessage(new LoseHealthMessage());
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag != "Player" || !_isFalling) return;
            _isDestroyed = true;
        }
    }
}