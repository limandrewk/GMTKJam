using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public enum Stance
    {
        Roaming,
        Defensive
    };

    private float m_defensiveRangeSq = 3600f;
    private float m_targetingRangeSq = 400f;

    private float m_targetingRangeSqFudge = 0f;

    public Stance m_stance = Stance.Roaming;
    
    public GameObject m_targetObject;
  
    // Start is called before the first frame update
    private void Start()
    {
        m_targetingRangeSqFudge = Random.Range( 0, 40f );
    }

    // Update is called once per frame
    private void Update()
    {
        float distSq;
        Vector3 pos = gameObject.transform.position;
        if( m_stance == Stance.Defensive && m_targetObject )
        {
            distSq = Vector3.SqrMagnitude(m_targetObject.transform.position - pos );
            if( distSq > m_defensiveRangeSq ) m_targetObject = null;
        }

        if( !m_targetObject )
        {
            float minDistSq = -1;
            EnemyShip potentialTarget = null;

            foreach( EnemyShip enemyShip in m_gameManager.m_enemyShips )
            {
                distSq = Vector3.SqrMagnitude( enemyShip.transform.position - pos );
                if( m_stance == Stance.Defensive && distSq > m_defensiveRangeSq ) continue;

                if( potentialTarget == null )
                {
                    minDistSq = distSq;
                    potentialTarget = enemyShip;
                }
                else
                {
                    if( distSq < minDistSq )
                    {
                        minDistSq = distSq;
                        potentialTarget = enemyShip;
                    }
                }
            }
            
            if( potentialTarget != null )
            {
                m_targetObject = potentialTarget.gameObject;
            }
        }
        
        if( m_targetObject == null ) return;

        
        distSq = Vector3.SqrMagnitude( m_targetObject.transform.position - pos );
        // Close enough
        if( distSq < m_targetingRangeSq - m_targetingRangeSqFudge ) return;
        
        // Move closer to target

        Vector3 targetDir = Vector3.Normalize( m_targetObject.transform.position - pos );
        pos = m_moveSpeed * Time.deltaTime * targetDir;
        gameObject.transform.position = pos;
    }

    protected override void RemoveFromGameManager()
    {
        m_gameManager.m_playerShips.Remove( this );
    }
}
