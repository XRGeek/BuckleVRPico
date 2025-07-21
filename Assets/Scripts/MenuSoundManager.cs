/*
Simple Sound Manager (c) 2016 Digital Ruby, LLC
http://www.digitalruby.com

Source code may no longer be redistributed in source format. Using this in apps and games is fine.
*/

using UnityEngine;
using UnityEngine.UI;

using System.Collections;

// Be sure to add this using statement to your scripts
// using DigitalRuby.SoundManagerNamespace

namespace DigitalRuby.SoundManagerNamespace
{
    public class MenuSoundManager : MonoBehaviour
    {
        
		[Header("Menu Background Track")]
		public AudioSource BGTrack;
        
		void Start(){
			PlayMenuMusic ();
		}
        private void PlaySound(int index)
        {
           
        }

        private void PlayMenuMusic()
        {
			BGTrack.PlayLoopingMusicManaged(1.0f, 1.0f,false);
        }

        private void CheckPlayKey()
        {
           

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlaySound(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlaySound(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlaySound(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlaySound(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PlaySound(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                PlaySound(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                PlaySound(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
              //  PlayMusic(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
               // PlayMusic(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
               // PlayMusic(2);
            }
			if (Input.GetKeyDown(KeyCode.B))
            {
				BGTrack.PlayLoopingMusicManaged(1.0f, 1.0f,false);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
				Debug.Log ("Stop Audio");
				BGTrack.StopLoopingMusicManaged ();
            }
        }

        

        private void Update()
        {
            CheckPlayKey();
        }
		public void StopMenuTrack(){
			Debug.Log ("Stop Audio");
			BGTrack.StopLoopingMusicManaged ();
		}

     
    }
}
