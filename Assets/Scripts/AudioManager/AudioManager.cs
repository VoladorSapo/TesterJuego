using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Transform player;
    [SerializeField]
    private Sound[] _sounds;
    List<AudioSource> pausedSounds= new List<AudioSource>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start(){
        CheckScene();
    }

    //Musica por escena
    void CheckScene(){
        Scene _scene=SceneManager.GetActiveScene();
        
        switch(_scene.name){
            case "1": Debug.LogWarning(_scene.name); break;
            case "2": break;
            default: break;
            //case: "Otras que ya veremos"
        }
    }

    //Poner sonido/musica
    public void PlaySound(string name, bool loop, Vector2 pos, bool onlyOne)
    {
        if(onlyOne && SearchSource(name)){return;}

        Sound Sound = System.Array.Find(_sounds, sound => sound.name == name);
        if(Vector2.Distance(pos,player.position)>Sound.maxSoundDistance || Sound==null){return;}

        if (Sound != null)
        {
            AudioSource a= gameObject.AddComponent<AudioSource>();
            a.maxDistance=Sound.maxSoundDistance;
            a.transform.position=pos;
            a.clip = Sound.audioClip;
            a.volume = Sound.volume;
            a.pitch = Sound.pitch;
            a.loop = loop;
            a.Play();
            //_audioSources.Add(name, a);

        }

    }

    public void PlaySound(string name, bool loop, Vector2 pos, bool onlyOne, float minPitch, float maxPitch)
    {
        if(onlyOne && SearchSource(name)){return;}

        Sound Sound = System.Array.Find(_sounds, sound => sound.name == name);
        if(Vector2.Distance(pos,player.position)>Sound.maxSoundDistance || Sound==null){return;}
        if (Sound != null)
        {
            
            AudioSource a= gameObject.AddComponent<AudioSource>();
            a.maxDistance=Sound.maxSoundDistance;
            a.transform.position=pos;
            a.clip = Sound.audioClip;
            a.volume = Sound.volume;
            a.pitch = Random.Range(minPitch,maxPitch);
            a.loop = loop;
            a.Play();

        }

    }

    public void PlaySoundFadeIn(string name, bool loop, Vector2 pos, bool onlyOne, float fadeInDuration){
        
        if(onlyOne && SearchSource(name)){return;}
        
        Sound Sound = System.Array.Find(_sounds, sound => sound.name == name);
        if(Vector2.Distance(pos,player.position)>Sound.maxSoundDistance || Sound==null){return;}
        if (Sound != null)
        {
            
            AudioSource a= gameObject.AddComponent<AudioSource>();
            a.maxDistance=Sound.maxSoundDistance;
            a.transform.position=pos;
            a.clip = Sound.audioClip;
            StartCoroutine(FadeIn(a,fadeInDuration,Sound.volume));
            a.pitch = Sound.pitch;
            a.loop = loop;
            a.Play();
            //_audioSources.Add(name, a);

        }

    }

    public void PlaySoundFadeIn(string name, bool loop, Vector2 pos, bool onlyOne, float fadeInDuration, float minPitch, float maxPitch){
        
        if(onlyOne && SearchSource(name)){return;}
        
        Sound Sound = System.Array.Find(_sounds, sound => sound.name == name);
        if(Vector2.Distance(pos,player.position)>Sound.maxSoundDistance || Sound==null){return;}
        if (Sound != null)
        {
            
            AudioSource a= gameObject.AddComponent<AudioSource>();
            a.maxDistance=Sound.maxSoundDistance;
            a.transform.position=pos;
            a.clip = Sound.audioClip;
            StartCoroutine(FadeIn(a,fadeInDuration,Sound.volume));
            a.pitch = Random.Range(minPitch,maxPitch);
            a.loop = loop;
            a.Play();
            //_audioSources.Add(name, a);

        }

    }
    public void PlayMusicFadeIn(string name, bool loop, bool isMusic, float minPitch, float maxPitch, Vector2 pos, float fadeInDuration){
        if(SearchSource(name)){return;}

        Sound Sound=System.Array.Find(_sounds, sound=> sound.name==name);
        if(Vector2.Distance(pos,player.position)>Sound.maxSoundDistance || Sound==null){return;}
        if(Sound!=null){
            AudioSource a= gameObject.AddComponent<AudioSource>();
            a.maxDistance=Sound.maxSoundDistance;
            a.transform.position=pos;
            a.clip = Sound.audioClip;
            StartCoroutine(FadeIn(a,fadeInDuration,Sound.volume));
            a.pitch = Sound.pitch;
            a.loop = loop;
            a.Play();
        }
    }

    public void PlayAllPaused()
    {
        
        foreach (AudioSource a in pausedSounds.ToArray())
        {
                a.UnPause();
                pausedSounds.Remove(a);
        }
    }

    //Cambio de sonido/musica
    public void ChangeMusicTo(string currMusicName, float fadeOutTime, string newMusicName, float fadeInTime){
        StartCoroutine(FadeOutThenFadeIn(currMusicName,fadeOutTime,newMusicName,fadeInTime));
    }
    IEnumerator FadeOutThenFadeIn(string currMusicName, float fadeOutTime, string newMusicName, float fadeInTime){
        string realName= System.Array.Find(_sounds, sound => sound.name == currMusicName).audioClip.name;
        AudioSource audioSource=GetComponents<AudioSource>().Where(a => a.clip.name==realName).ToArray()[0];

        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeOutTime)
        {
            if(audioSource==null){yield break;}
            float elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / fadeOutTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, normalizedTime);

            audioSource.volume = newVolume;
            yield return null;
        }
        
        audioSource.Stop();
        Destroy(audioSource);

        AudioSource audioSourceNew=gameObject.AddComponent<AudioSource>();
        startVolume = 0f;
        startTime = Time.time;
        float desiredVolume=System.Array.Find(_sounds, sound=> sound.name==name).volume;

        while (Time.time < startTime + fadeInTime)
        {
            if(audioSource==null){yield break;}
            float elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / fadeInTime;
            float newVolume = Mathf.Lerp(startVolume, desiredVolume, normalizedTime);

            audioSourceNew.volume = newVolume;
            yield return null;
        }
    }

    //Parar sonido/musica
    public void StopAllSoundsWithName(string name)
    {
        AudioSource[] AudioSources = FindAudioSources(name);
        foreach(AudioSource a in AudioSources){
            a.Stop();
            Destroy(a);
        }
    }

    public void PauseAllSoundsWithName(string name)
    {
        AudioSource[] AudioSources = FindAudioSources(name);
        foreach(AudioSource a in AudioSources){
            a.Pause();
            if(!pausedSounds.Contains(a)){pausedSounds.Add(a);}
        }
    }


    public void StopSoundwithName(string name, float fadeOutTime){
        string realName= System.Array.Find(_sounds, sound => sound.name == name).audioClip.name;
        AudioSource[] _audioSources=GetComponents<AudioSource>();
        foreach(AudioSource a in _audioSources){
            if(a.clip.name==realName){
                Debug.Log("Encontrado");
                StartCoroutine(FadeOut(a,fadeOutTime));
            }
        }
    }

    public void StopAllSounds(string name)
    {
        AudioSource[] AudioSources=GetComponents<AudioSource>();
        foreach(AudioSource a in AudioSources){
            a.Stop();
            Destroy(a);
        }
    }

    public void PauseAllSounds()
    {
        AudioSource[] AudioSources=GetComponents<AudioSource>();
        foreach(AudioSource a in AudioSources){
            a.Pause();
            if(!pausedSounds.Contains(a)){pausedSounds.Add(a);}
        }
    }


    //Efecto Fade
    private IEnumerator FadeOut(AudioSource audioSource, float fadeOutDuration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeOutDuration)
        {
            if(audioSource==null){yield break;}
            float elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / fadeOutDuration;
            float newVolume = Mathf.Lerp(startVolume, 0f, normalizedTime);

            audioSource.volume = newVolume;
            yield return null;
        }
        
        audioSource.Stop();
        Destroy(audioSource);
    }

    private IEnumerator FadeIn(AudioSource audioSource, float fadeInDuration, float desiredVolume)
    {
        float startVolume = 0f;
        float startTime = Time.time;

        while (Time.time < startTime + fadeInDuration)
        {
            if(audioSource==null){yield break;}
            float elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / fadeInDuration;
            float newVolume = Mathf.Lerp(startVolume, desiredVolume, normalizedTime);

            audioSource.volume = newVolume;
            yield return null;
        }
    }


    //Buscar
    private bool SearchSource(string name){
        string realName= System.Array.Find(_sounds, sound => sound.name == name).audioClip.name;
        AudioSource[] _audioSources=GetComponents<AudioSource>();
        foreach(AudioSource a in _audioSources){
            if(a.clip.name==realName){
                return true;
            }
        }
        return false;
    }
    
    private AudioSource[] FindAudioSources(string name)
    {
        AudioSource[] audioSources=gameObject.GetComponents<AudioSource>();
        string realName= System.Array.Find(_sounds, sound => sound.name == name).audioClip.name;
        List<AudioSource> returnAudios=new List<AudioSource>();

        foreach(AudioSource a in audioSources){
            if(a.clip.name==realName){
                returnAudios.Add(a);
            }
        }
        return returnAudios.ToArray();
    }
    
    //Random
    public string PickRandom(string[] sounds){
      int i=Random.Range(0,sounds.Length);
      return sounds[i];  
    }
    
    void Update(){
        AudioSource[] audioSources=gameObject.GetComponents<AudioSource>().Where(a=>!a.loop).ToArray();
        foreach(AudioSource a in audioSources){
            if(!a.isPlaying && !pausedSounds.Contains(a)){
                a.Stop();
                Destroy(a);
            }
        }
    }
}
