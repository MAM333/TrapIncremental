using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] int life = 100;

    public event Action lifeChanged;
    public event Action die;

    public void GetDamage(int damage)
    {
        life -= damage;
        lifeChanged?.Invoke();

        if (life <= 0)
        {
            life = 0;
            die?.Invoke();
        }
    }
}
