using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponData defaultWeapon;
    [SerializeField] private GenericPool magazinePool;

    public List<WeaponData> unlockedWeapons = new List<WeaponData>();
    public WeaponData currentWeapon;
    private PlayerInput playerInput;
    private GameObject currentWeaponGO;

    private void Start()
    {
        if (defaultWeapon != null)
        {
            unlockedWeapons.Add(defaultWeapon);
            SelectWeaponByIndex(0);
        }
    }

    private void OnEnable()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>(); 
        var actions = playerInput.actions;

        actions["SelectWeapon1"].performed += ctx => SelectWeaponByIndex(0);
        actions["SelectWeapon2"].performed += ctx => SelectWeaponByIndex(1);
        actions["SelectWeapon3"].performed += ctx => SelectWeaponByIndex(2);
        actions["SelectWeapon4"].performed += ctx => SelectWeaponByIndex(3);
        actions["Attack"].performed += OnAttackStart;
        actions["Attack"].canceled += OnAttackEnd;
    }

    private void OnDisable()
    {
        if (playerInput == null) return;
        var actions = playerInput.actions;

        actions["SelectWeapon1"].performed -= ctx => SelectWeaponByIndex(0);
        actions["SelectWeapon2"].performed -= ctx => SelectWeaponByIndex(1);
        actions["SelectWeapon3"].performed -= ctx => SelectWeaponByIndex(2);
        actions["SelectWeapon4"].performed -= ctx => SelectWeaponByIndex(3);
        actions["Attack"].performed -= OnAttackStart;
        actions["Attack"].canceled -= OnAttackEnd;
    }

    void SelectWeaponByIndex(int index)
    {
        if (!Player.IsDead)
        {
            if (index < unlockedWeapons.Count)
            {
                currentWeapon = unlockedWeapons[index];
                UpdateWeaponVisual();
                UpdateWeaponPool();
            }
        }
    }

    private void UpdateWeaponVisual()
    {
        if (currentWeapon == null || currentWeapon.weaponPrefab == null)
        {
            Debug.Log("El arma actual no tiene prefab");
            return;
        }

        if (currentWeaponGO != null)
        {
            Destroy(currentWeaponGO);
        }
        currentWeaponGO = Instantiate(currentWeapon.weaponPrefab, transform);
    }

    private void UpdateWeaponPool()
    {
        if (currentWeapon == null || currentWeapon.poolSO == null)
        {
            return;
        }

        magazinePool = currentWeaponGO?.GetComponentInChildren<GenericPool>();
        
        if (magazinePool == null)
        {
            GameObject poolGO = new GameObject("MagazinePool");
            poolGO.transform.SetParent(currentWeaponGO.transform);
            magazinePool = poolGO.AddComponent<GenericPool>();
        }

        magazinePool.poolConfig = currentWeapon.poolSO;

        foreach (Transform child in magazinePool.transform)
        {
            Destroy(child.gameObject);
        }
        
        if (GenericPool.Pools.ContainsKey(magazinePool.poolConfig.poolID))
        {
            GenericPool.Pools.Remove(magazinePool.poolConfig.poolID);
        }
        
        magazinePool.InitializePool();
    }

    public void UnlockWeapon(WeaponData newWeapon)
    {
        if (!unlockedWeapons.Contains(newWeapon))
        {
            unlockedWeapons.Add(newWeapon);
        }
        
    }

    private void OnAttackStart(InputAction.CallbackContext context)
    {
        if (currentWeapon != null && !Player.IsDead)
        {
            currentWeapon.UseWeapon(gameObject);
        }
    }

    private void OnAttackEnd(InputAction.CallbackContext context)
    { 
        if (currentWeapon != null)
        {
            currentWeapon.StopUse(gameObject);
        }
    }
}
