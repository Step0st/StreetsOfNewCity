using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ParticleManager))]
[RequireComponent(typeof(TemporaryEffectsManager))]
public class CharacterManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int health = 30;
    [Header("Visuals")]
    public TextMeshPro healthText;
    public TextMeshPro armorText;
    [Header("Settings")]
    public bool isPlayableCharacter;
    public CharacterTeam characterTeam;
    [Header("Cards character can play")]
    public PlayableCard[] characterDeck;
    public static Action<CharacterManager> CharacterDead;
    
    private CharacterCardSpawner _cardsSpawner;
    private TemporaryEffectsManager _temporaryEffectsManager;
    private ParticleManager _particleManager;
    private int _maxHealth;
    private int _armor = 0;

    private void OnEnable()
    {
        _cardsSpawner = GetComponentInChildren<CharacterCardSpawner>();
    }

    private void Start()
    {
        _temporaryEffectsManager = GetComponent<TemporaryEffectsManager>();
        _particleManager = GetComponent<ParticleManager>();
        healthText.text = health.ToString();
        armorText.text = _armor.ToString();
        _maxHealth = health;
    }

    public void TakeDamage(int damage)
    {
        _armor -= damage;
        if (_armor > 0)
        {
            armorText.text = _armor.ToString();
        } 
        else if (_armor == 0)
        {
            RemoveArmor();
        } 
        else if (_armor < 0)
        {
            health += _armor;
            healthText.text = health.ToString();
            RemoveArmor();
        }
        
        SoundManager.soundManager.DamageSound();
        _particleManager.PlayDamageParticle();
        
        if (health <= 0)
        {
            CharacterDead?.Invoke(this);
        }
    }

    public void RemoveArmor()
    {
        _armor = 0;
        armorText.renderer.enabled = false;
    }
    
    public void Heal(int healing)
    {
        health += healing;
        if (health > _maxHealth)
        {
            health = _maxHealth;
        }
        healthText.text = health.ToString();
        _temporaryEffectsManager.RemovePoisoning();
        
        SoundManager.soundManager.HealSound();
        _particleManager.PlayHealParticle();
        
    }
    
    public void GetArmor(int armorValue, int duration)
    {
        _armor += armorValue;
        armorText.renderer.enabled = true;
        armorText.text = _armor.ToString();
        _temporaryEffectsManager.ArmoringStart(duration);
        
        SoundManager.soundManager.ArmorSound();
        _particleManager.PlayArmorParticle();
        
    }
    
    public void Poisoning(int temporaryDamage, int duration)
    {
        SoundManager.soundManager.PosionSound();
        _temporaryEffectsManager.PoisonEffectsStart(temporaryDamage, duration);
    }

    public PlayableCard DrawCard()
    {
        return _cardsSpawner.SpawnCard(characterDeck);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        CharacterEvents.OnEnterCharacter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CharacterEvents.OnExitCharacter(gameObject);
    }
}