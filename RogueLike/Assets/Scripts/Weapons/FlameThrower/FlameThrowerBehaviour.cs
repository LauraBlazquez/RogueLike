using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerBehaviour : MonoBehaviour
{
    [SerializeField] private new ParticleSystem particleSystem;
    public FlameThrower weaponData;
    private bool isFiring = false;

    public void Initialize(FlameThrower weaponData)
    {
        this.weaponData = weaponData;
    }

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }
    }

    private void Update()
    {
        Flip();
    }

    public void StartFiring()
    {
        if (!isFiring)
        {
            particleSystem.Play();
        }
        isFiring = true;
    }

    public void StopFiring()
    {
        if (isFiring)
        {
            particleSystem.Stop();
        }
        isFiring = false;
    }

    public void Flip()
    {
        Vector3 rotation = transform.localEulerAngles;
        rotation.y = Player.IsFlipped ? 180f : 0f;
        transform.localEulerAngles = rotation;
    }
}
