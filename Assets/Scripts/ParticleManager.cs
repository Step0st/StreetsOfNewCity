using UnityEngine;

public class ParticleManager : MonoBehaviour
{
      [SerializeField]private ParticleSystem damageParticle;
      [SerializeField]private ParticleSystem armorParticle;
      [SerializeField]private ParticleSystem healParticle;

      public void PlayDamageParticle()
      {
            damageParticle.Play();
      }
      
      public void PlayArmorParticle()
      {
            armorParticle.Play();
      }
      
      public void PlayHealParticle()
      {
            healParticle.Play();
      }
}
