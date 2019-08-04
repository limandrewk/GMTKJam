using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class EnemyShip : Ship
{
    // Start is called before the first frame update
    void Start()
    {
        m_targetingRangeSqFudge = Random.Range( 0, 400f );
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
        
        float distSq;
        Vector3 pos = gameObject.transform.position;

        if( m_targetObject == null )
        {
            float minDistSq = -1;
            PlayerShip potentialTarget = null;

            foreach( PlayerShip playerShip in m_gameManager.m_playerShips )
            {
                distSq = Vector3.SqrMagnitude( playerShip.transform.position - pos );
                if( m_stance == Stance.Defensive && distSq > m_defensiveRangeSq ) continue;

                if( potentialTarget == null )
                {
                    minDistSq = distSq;
                    potentialTarget = playerShip;
                }
                else
                {
                    if( distSq < minDistSq )
                    {
                        minDistSq = distSq;
                        potentialTarget = playerShip;
                    }
                }
            }
            
            if( potentialTarget != null )
            {
                m_targetObject = potentialTarget.gameObject;
            }
        }
        
        GameObject targetObject = m_targetObject;
        if( m_targetObject == null )
        {
            targetObject = m_gameManager.m_base.gameObject;
        }

        Vector3 targetDir = targetObject.transform.position - pos;
        distSq = Vector3.SqrMagnitude( targetDir );
        targetDir = Vector3.Normalize( targetDir );
        
        // Rotate to target
        float step = m_rotationSpeed * Time.deltaTime;
        
        var angle = Mathf.Atan2( targetDir.y, targetDir.x ) * Mathf.Rad2Deg + 90;
        Quaternion targetRotation = Quaternion.AngleAxis( angle, Vector3.forward );
        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, step );
        
        if( distSq < m_targetingRangeSq - m_targetingRangeSqFudge ) return; // Close enough
        
        // Move closer to target
        pos += m_moveSpeed * Time.deltaTime * targetDir;
        transform.position = pos;
    }

    protected override void RemoveFromGameManager()
    {
        m_gameManager.m_enemyShips.Remove( this );
    }
}
