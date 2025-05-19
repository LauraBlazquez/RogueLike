using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBehaviour : MonoBehaviour
{
    public FlameThrowerBehaviour FlameThrowerBehaviour;
    private void Start()
    {
        FlameThrowerBehaviour = GetComponentInParent<FlameThrowerBehaviour>();
    }
    public void OnParticleCollision(GameObject other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(FlameThrowerBehaviour.weaponData.damage * Time.deltaTime);
        }
    }
}
