using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BGMsound : MonoBehaviour   
{
   /*private static BGMsound instance = null; // 생성된 비지엠이없으면
    public static BGMsound I
    {
        get
        {
            if (instance == null) instance = FindObjectOfType(typeof(BGMsound)) as BGMsound; // 씬내의 사운드를 찾아보고 없을 시 아래 실행.
            if (instance == null) // 하이아키에서 비지엠파일 찾아오고 그래도없으묜
            { // 사운드 프리펩찾아오고.
                GameObject go = Instantiate(Resources.Load("SOUND")) as GameObject;
                
                //go.AddComponent<BGMsound>();
                go.name = "SoundManger"; // 이름설정
                instance = go.GetComponent<BGMsound>();
            }
            return instance;
        }

    }*/
    public AudioSource MusicPlayer = null;
    public AudioSource ClickSound = null;
    private object SOUNDDATA;

    //AudioSource _musicPlayer = null;
    /*public AudioSource MusicPlayer
    { // BGM용 스피커 사용.
        get
        {
            if (_musicPlayer == null)
            {
                _musicPlayer = Camera.allCameras[0].GetComponent<AudioSource>();
            }
            return _musicPlayer;
        }
    }*/

    private void Awake()
    {
       // MusicPlayer.volume = 1.0f - PlayerPrefs.GetFloat("BGM_Volume"); // 키값만 가져오면됨.
        // 최조 실행이 되어 값이 없게되니 0.0이 들어오게됨. 
    }

    /* public void PlayBGM(AudioClip BGM, bool bLoop = true)
     {
         MusicPlayer.clip = BGM;
         MusicPlayer.loop = bLoop;
         MusicPlayer.Play();
     }*/
    /*  public void OneShotSound(AudioClip EffSound, float TempPitch = 1.0f)
      { //pitch = 재생속도 .
          SoundPlater.pitch = TempPitch;
          SoundPlater.loop = false;
          SoundPlater.PlayOneShot(EffSound);
      }
      public void LoopSound(AudioClip EffSound) //사운드 루프
      {
          SoundPlater.clip = EffSound;
          SoundPlater.loop = true;
          SoundPlater.Play();
      }
      public void StopLoopSound() // 사운드 루프 중단.
      {
          //SoundPlater.Stop();
          SoundPlater.pitch = 0.5f;
          SoundPlater.loop = false;
      }*/

    public void SetMusicVolume(float fVlume)
    {
        MusicPlayer.volume = fVlume;
       // PlayerPrefs.SetFloat("BGM_Volume", 1.0f - fVlume); // 저장.
    }

    public void SetClickSoundVolume(float EVlume)
    {
        ClickSound.volume = EVlume;
    }

    public void EffPlay()
    {
        ClickSound.Play();
    }
}
