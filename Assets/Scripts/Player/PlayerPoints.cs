using System;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] int points = 0;
    [SerializeField] int pointsMultiplier = 1;

    public event Action<int> pointsChanged;

    bool dead = false;

    public void SetPointsMultiplier(int mult)
    {
        pointsMultiplier = mult;
    }

    private void Start()
    {
        SumPoints(PlayerSaveManager.instance.GetPoints());
    }

    public void SumPoints(int pts)
    {
        if (dead) return;

        points += pts * pointsMultiplier;

        pointsChanged?.Invoke(points);
    }

    public int GetPoints() 
    {
        return points; 
    }

    public void Die()
    {
        dead = true;
    }
}
