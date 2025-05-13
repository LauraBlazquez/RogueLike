using UnityEngine;

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public int damage;
    public GenericPoolSO poolSO = null;

    public virtual void UseWeapon(GameObject user)
    {
        Debug.Log("Using a weapon");
    }
}
