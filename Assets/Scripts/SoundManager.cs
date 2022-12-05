using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _source;
    
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _healSound;
    [SerializeField] private AudioClip _armorSound;
    [SerializeField] private AudioClip _posionSound;

    public static SoundManager soundManager { get; private set; }

    private void Awake()
    {
        if (soundManager != null && soundManager != this)
        {
            Destroy(this);
        }
        else
        {
            soundManager = this;
        }
    }
    
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    
    public void DamageSound()
    {
        Play(_damageSound);
    }
    
    public void HealSound()
    {
        Play(_healSound);
    }
    
    public void ArmorSound()
    {
        Play(_armorSound);
    }
        
    public void PosionSound()
    {
        Play(_posionSound);
    }
    
    public void Play(AudioClip clip, float volume = 1f, bool loop = false)
    {
        _source.clip = clip;
        _source.volume = volume;
        _source.loop = loop;
        _source.Play();
    }

}