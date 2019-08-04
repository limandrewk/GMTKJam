using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public GameManager m_gameManager;

    public float m_rotationSpeed = 50f;
    public float m_moveSpeed = 5f;
    public float m_health = 50f;
    public int m_armour = 0;

    public enum Stance
    {
        Roaming,
        Defensive
    };

    public float m_defensiveRangeSq = 22500f;
    public float m_targetingRangeSq = 8100f;

    protected float m_targetingRangeSqFudge = 0f;

    public Stance m_stance = Stance.Roaming;

    public GameObject m_targetObject;

    public GameObject m_explosionPrefab;


    protected void CheckForDeath()
    {
        if( m_health <= 0 )
        {
            RemoveFromGameManager();
            
            if( m_explosionPrefab != null ) Instantiate( m_explosionPrefab, transform.position, Quaternion.identity );

            Destroy( gameObject );
        }
    }

    public void Damage( float value )
    {
        m_health -= value - m_armour;
    }
    
    protected abstract void RemoveFromGameManager();
}
