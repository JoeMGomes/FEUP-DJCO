using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static SoundManager m_Instance;


    public static SoundManager Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(SoundManager) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = FindObjectOfType<SoundManager>();

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<SoundManager>();
                        singletonObject.name = typeof(SoundManager).ToString() + " (Singleton)";
                        m_Instance.Initialize();
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }


    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }

    public enum Sound
    {
        PlayerJump,
        PlayerHit,
        PlayerDie,
        PlayerShootPrimary,
        PlayerShootSecondary,
        GrenadeExplode,
        PlayerGrabPowerUp,
        EnemyHit,
        EnemyDie,
        StartRun,
        EndRun,
    }

    private Dictionary<Sound, float> soundTimes;
    private LevelManager levelManager;
    private GameObject soundObject;
    private AudioSource audioSource;

    public void Initialize()
    {
        soundTimes = new Dictionary<Sound, float>();
        levelManager = FindObjectOfType<LevelManager>();


    }

    public void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (soundObject == null)
            {
                soundObject = new GameObject("Sound");
                audioSource = soundObject.AddComponent<AudioSource>();
            }
            audioSource.PlayOneShot(GetClip(sound));
        }
    }

    private AudioClip GetClip(Sound sound)
    {
        foreach (LevelManager.SoundAudioClip s in levelManager.audioClips)
        {
            if (s.sound == sound)
            {
                return s.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;

    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            
            case Sound.PlayerDie:
                float minTime = 15f; //only play sound every 50ms
                if (soundTimes.ContainsKey(sound))
                {
                    if (soundTimes[sound] + minTime < Time.time)
                    {
                        soundTimes[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            default:
                return true;
                //example code for limiting sound playing
                /* case Sound.Example:
                 * float minTime = 0.05f //only play sound every 50ms
                 * if(soundTimes.ContainsKey(sound)){
                 *      if(soundTimes[sound] + minTime < Time.time){
                 *          soundTimeDictionary[sound] = Time.time;
                 *          return true;
                 *      } else{
                 *          return false;
                 *      }
                 * } else{
                 *  return true;
                 * }
                 * break
                 */

        }
    }

}
