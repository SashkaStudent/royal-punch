using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected int maxHealth = 100;
    [SerializeField]
    protected int health = 100;
    public int Current => health;
    public float Percentage => (float)health / maxHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDead;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        health = maxHealth;
     //   OnHealthChanged?.Invoke(health, 0);
    }
    public void DecreaseHealth(int value)
    {
        health = (int)MathF.Max(0, health - value);
        OnHealthChanged?.Invoke(health, value);
        if (health <= 0)
        {
            OnDead?.Invoke();
            Debug.Log("DEAD");
        }
    }

    public void SetMax(int value)
    {
        maxHealth = value;
        Restart();
    }
    public void Restart()
    {
        health = maxHealth;
        OnHealthChanged?.Invoke(health, 0);
    }


    protected virtual void HitHandler(int value)
    {
        DecreaseHealth(value);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnDestroy()
    {
    }
}
