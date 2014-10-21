using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayButton : MonoBehaviour
    {
        public void Play()
        {
            Application.LoadLevel("Game");
        }
    }
}
