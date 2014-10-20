using Assets.Scripts.EventHandler;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Speed = 5;

        void Update()
        {
            transform.Translate(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.8f, 3.5f), -3.5f, 0);

            if(Input.GetKey(KeyCode.Space))
                EventAggregator.SendMessage(new StartPoopingMessage());
        }
    }
}