using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Melee", menuName = "Weapons/Melee")]
public class Melee : WeaponData
{
    public float attackRange = 1.2f;

    public override void UseWeapon(GameObject user)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        Vector3 playerPos = user.transform.position;
        float distance = Vector3.Distance(playerPos, mousePos);

        if (distance > attackRange)
        {
            //Debug.Log("Objetivo fuera de alcance del ataque melee");
            return;
        }

        Collider2D hit = Physics2D.OverlapPoint(mousePos, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            IDamageable enemy = hit.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                //Debug.Log($"Golpeado enemigo {hit.name}");
            }
        }
        else
        {
            //Debug.Log("Wiffeada heavy");
        }
    }
}
