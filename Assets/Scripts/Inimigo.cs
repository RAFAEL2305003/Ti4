using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe base para os inimigos
public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public Rigidbody2D rb;

    // Método para receber dano
    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    // Método chamado quando o inimigo morre
    protected virtual void Die()
    {
        // Implemente aqui o que acontece quando o inimigo morre
        Destroy(gameObject);
    }
}
