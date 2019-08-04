using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public GameManager m_gameManager;

    public float m_moveSpeed = 5f;
    public float m_health = 50f;
    public int m_armour = 0;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if( m_health <= 0 )
        {
            RemoveFromGameManager();
            
            Destroy( gameObject );
        }
    }

    protected abstract void RemoveFromGameManager();
}
