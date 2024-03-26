using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;
    public delegate void ResetEvent();
    public ResetEvent OnHitReset;
    public float MaxHealth = 10f;
    public Cooldown Invulnerability;

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }
    
    private float _currentHealth = 10f;
    private bool _canDamage = true;

    void Start()
    {
        ResetHealthToMax();
    }

    private void Update()
    {
        ResetInvulnerable();
    }
    void ResetInvulnerable()
    {
        if (_canDamage)
            return;

        if (Invulnerability.IsOnCoolDown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
        
    }
    public void Damage(float damage, GameObject source)
    {
        if (!_canDamage)
            return;

        _currentHealth -= damage;

        if(_currentHealth <= 0f)
        {
            Die();
        }

        Invulnerability.StartCooldown();
        _canDamage = false;

        OnHit?.Invoke(source);
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
    void ResetHealthToMax()
    {
        _currentHealth = MaxHealth;
    }
}
