using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    AudioSource GameplayAudio, SfxCoinState, SfxVoice, SfxAmbience, SfxMenu; //, PauseAudio //MenuAudio,

    public AudioClip GameplayMusic, PauseMusic, Incorrect, Correct1, Correct2, CoinDrop, CoinPickUp, AmbienceAudio, MenuMusic; //MenuMusic, 

    public AudioMixerGroup amgMusic, amgVoice, amgAmbience, amgEffects;

    private int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {

        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SfxAmbience = gameObject.AddComponent<AudioSource>();

        GameplayAudio = gameObject.AddComponent<AudioSource>();

        SfxMenu = gameObject.AddComponent<AudioSource>();

        SfxCoinState = gameObject.AddComponent<AudioSource>();

        SfxVoice = gameObject.AddComponent<AudioSource>();

        SfxAmbience.playOnAwake = GameplayAudio.playOnAwake = SfxMenu.playOnAwake = 
            SfxCoinState.playOnAwake = SfxVoice.playOnAwake = false;

        SfxAmbience.loop = GameplayAudio.loop = SfxMenu.loop = true;

        SfxAmbience.outputAudioMixerGroup = amgAmbience;

        SfxMenu.outputAudioMixerGroup = amgMusic;

        GameplayAudio.outputAudioMixerGroup = amgMusic;

        SfxVoice.outputAudioMixerGroup = amgVoice;

        SfxCoinState.outputAudioMixerGroup = amgEffects;

        GamePlayAudioManager();

    }


    // Update is called once per frame
    //void Update()
    //{

    //    GamePlayAudioManager();

    //}


    public void GamePlayAudioManager()
    {
        switch (sceneIndex)
        {

            case 2: // "Match-Coins-With-Jars": 

                MenuMusicHandler();

                break;

            case 3: // "Match-Coins-With-Jars": 

                MenuMusicHandler();

                break;
            //case "Match-Coins-To-Jars": //"Match-Coins-With-Jars":

            //    break;

            //case "Which-Coins-Are-More": //"Which-Coins-Are-More":

            //    break;

            //case "What-Is-The-Value": // "Guess-The-Value":

            //    break;

            default:

                GameplayAudio.clip = GameplayMusic;

                if (!GameplayAudio.isPlaying)
                {

                    GameplayAudio.volume = 0.9f;

                    GameplayAudio.Play();

                }

                SfxAmbience.clip = AmbienceAudio;

                if (!SfxAmbience.isPlaying)
                {

                    SfxAmbience.volume = 0.02f;

                    SfxAmbience.Play();

                }

                break;

        }


    }


    // Update is called once per frame
    void MenuMusicHandler()
    {

        SfxMenu.clip = MenuMusic;

        if (!SfxMenu.isPlaying)
        {

            //amgVoice.audioMixer.
            SfxMenu.volume = 0.8f;

            SfxMenu.Play();

        }

    }


    public void PlaySound(string SoundName)
    {
        switch (SoundName)
        {

            case "PickedAndNotMatched": // "Match-Coins-With-Jars": 

                SfxCoinState.clip = CoinPickUp;

                if (!SfxCoinState.isPlaying)
                {

                    SfxCoinState.Play();

                }

                //Debug.Log("coinPickUp sound played: " + coinPickUp.name);

                break;

            case "AlreadyPickedAndNotMatched": //"Which-Coins-Are-More":

                StartCoroutine(SoundBoom(false));

                break;

            case "AlreadyPickedAndMatched": // "Guess-The-Value":

                StartCoroutine(SoundBoom(true));

                break;

            default:


                break;
        }
    }

    public IEnumerator SoundBoom(bool SuccessType)
    {

        if (SuccessType == true)
        {

            SfxCoinState.clip = CoinDrop;

            if (!SfxCoinState.isPlaying)
            {

                SfxCoinState.Play();

            }

            Debug.Log("CoinDrop sound played: "); // + CoinDrop.ToString());

            yield return new WaitForSeconds(1);

            //SfxVoice.clip = Correct[Random.Range(0, Correct.Length)];

            SfxVoice.clip = Random.Range(0, 2) is 1 ? SfxVoice.clip = Correct1 : SfxVoice.clip = Correct2;

            if (!SfxVoice.isPlaying)
            {

                SfxVoice.Play();

                Debug.Log("Correct sound played: "); // + SfxVoice.clip.ToString());

            }
        }

        else
        {

            SfxVoice.clip = Incorrect;

            if (!SfxVoice.isPlaying)
            {

                SfxVoice.Play();

                Debug.Log("Incorrect sound played: ");// + Incorrect.ToString());

            }
        }
        //yield return null;
    }

}
