using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Extensions
{
    public class RandomizedAudioSource : MonoBehaviour
    {
        private AudioSource AudioSource;
        [SerializeField]
        private AudioClip[] AudioClips;

        private Coroutine SoundCoroutine;

        private int lastPlayedIndex = -1;

        private void Start()
        {
            if (AudioSource == null) AudioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            if (AudioClips.Length == 0) return;
            int index = Random.Range(0, AudioClips.Length);

            AudioSource.clip = AudioClips[index];
            AudioSource.Play();
        }

        public void Loop()
        {
            if (AudioClips.Length == 0) return;
            SoundCoroutine ??= StartCoroutine(PlayRandomSounds());
        }

        public void Stop()
        {
            if (SoundCoroutine == null) return;

            StopCoroutine(SoundCoroutine);
            SoundCoroutine = null;

            AudioSource.Stop();
        }

        private IEnumerator PlayRandomSounds()
        {
            while (true)
            {
                int nextIndex;
                do nextIndex = Random.Range(0, AudioClips.Length);
                while (nextIndex == lastPlayedIndex);

                lastPlayedIndex = nextIndex;

                AudioSource.clip = AudioClips[nextIndex];
                AudioSource.Play();

                yield return new WaitForSeconds(AudioSource.clip.length);
            }
        }
    }
}
