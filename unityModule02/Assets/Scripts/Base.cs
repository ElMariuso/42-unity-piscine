using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int hp = 5;

    private void TakeDamage(int amount)
    {
        hp = hp - amount;

        if (hp <= 0)
        {
            Debug.Log("Game Over");
        }
        else
            Debug.Log("Your base have now " + amount + " hp!");
    }
}
