using UnityEngine;

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public int damage;
    public int price;
    public GenericPoolSO poolSO = null;

    public virtual void UseWeapon(GameObject user)
    {
        //Debug.Log("Using a weapon");
    }

    public virtual void StopUse(GameObject user)
    {
        //Debug.Log("Stop using weapon.");
    }
}
