using System;
using System.Collections;

using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
   //weapon stats
   [SerializeField]
   string weaponName;
   [SerializeField]
   int weaponDamage;
   [SerializeField]
   int durability;
   [SerializeField]
   float weaponRange;
   [SerializeField]
   float weaponCooldown;
   [SerializeField]
   float radius = 0.75f;
   [SerializeField]
   LayerMask Ignorelayer;
   //cooldown variables
   bool isAttacking;
   bool isReadytoAttack;

   bool isMelee;

   //event
   public static event Action<Collider, int> OnHit;
   public static event Action<Collider, Vector3> PassPlayerPosition;

   void OnEnable()
   {
      PlayerAttack.OnMeleeAttack += Attack;
   }
   void Start()
   {
      isAttacking = false;
      isReadytoAttack = true;
   }

   void Update()
   {

   }

   void Attack(Vector3 position, Vector3 direction)
   {
      if (isReadytoAttack && !isAttacking)
      {
         isReadytoAttack = false;
         isAttacking = true;
         Collider[] EnemiesHit = Physics.OverlapSphere(direction * weaponRange + position, radius, ~Ignorelayer);
         foreach (Collider Enemy in EnemiesHit)
         {
            if (Enemy.tag == "Enemy")
            {
               OnHit?.Invoke(Enemy, weaponDamage);
               PassPlayerPosition?.Invoke(Enemy, position);
            }
         }
         isAttacking = false;
         StartCoroutine(WeaponCooldown());
      }
   }

   IEnumerator WeaponCooldown()
   {
      yield return new WaitForSeconds(weaponCooldown);
      isReadytoAttack = true;
   }


}
