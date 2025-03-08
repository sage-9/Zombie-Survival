using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
   public int damage;
   public int durability;
   public float attackSpeed;
   public float range;

   public void weaponSwing()
   {
      Debug.Log("I swung ");
   }
}
