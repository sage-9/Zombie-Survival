using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public GameObject[] weaponSlots= new GameObject[1];

    
    bool isMeleeWeapon;

    void GetWeaponReferences()
    {
        if (weaponSlots[0]==null) return;
        if(weaponSlots[0].GetComponent<MeleeWeapon>()==null&&
        weaponSlots[0].GetComponent<GunScript>()==null) return;
        if(weaponSlots[0].GetComponent<MeleeWeapon>()==null) isMeleeWeapon=false;
        else isMeleeWeapon=true;        
    }    
    void callAttackFuction()
    {
        if(isMeleeWeapon)
        {
            weaponSlots[0].GetComponent<MeleeWeapon>();
        }
        else weaponSlots[0].GetComponent<GunScript>().Shoot();
    }        
}
