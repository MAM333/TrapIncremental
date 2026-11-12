using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] int maxLife = 100;
    [SerializeField] int life = 100;

    public event Action<int, int> lifeChanged;
    public event Action<int> die;

    public void SetMaxLife(int life)
    {
        maxLife = life;
    }

    private void Start()
    {
        life = maxLife;

        lifeChanged?.Invoke(life, maxLife);
    }

    public void GetDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            life = 0;
            die?.Invoke(maxLife);
        }
        else lifeChanged?.Invoke(life, maxLife);
    }
}
