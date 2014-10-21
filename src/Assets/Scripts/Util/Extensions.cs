using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Util
{
    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) return;
            foreach (var e in enumerable)
                action(e);
        }

        public static string ToFormat(this string s, params object[] parmaters)
        {
            return string.Format(s, parmaters);
        }

        public static void PlaySound(this MonoBehaviour m, AudioClip c)
        {
            var a = m.GetComponent<AudioSource>();
            if (a == null) return;

            a.clip = c;
            a.Play();
        }

        public static void PlaySound(this MonoBehaviour m, List<AudioClip> c)
        {
            PlaySound(m, c[Random.Range(0, c.Count)]);
        }
    }
}
