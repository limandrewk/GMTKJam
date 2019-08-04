using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public Vector3 m_restLocation;
    
    // Start is called before the first frame update
    private void Start()
    {
        m_targetingRangeSqFudge = Random.Range( 0, 400f );
        m_restLocation = Random.insideUnitCircle * 40;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForDeath();

        float distSq;
        Vector3 pos = gameObject.transform.position;
        if( m_stance == Stance.Defensive && m_targetObject )
        {
            distSq = Vector3.SqrMagnitude(m_targetObject.transform.position - pos );
            if( distSq > m_defensiveRangeSq ) m_targetObject = null;
        }

        if( m_targetObject == null )
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

        if( m_targetObject == null )
        {
            Vector3 targetDir = m_restLocation - pos;
            distSq = Vector3.SqrMagnitude( targetDir );
            targetDir = Vector3.Normalize( targetDir );

            // Rotate to target
            float step = m_rotationSpeed * Time.deltaTime;
        
            var angle = Mathf.Atan2( targetDir.y, targetDir.x ) * Mathf.Rad2Deg + 90;
            Quaternion targetRotation = Quaternion.AngleAxis( angle, Vector3.forward );
            transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, step );

            // Close enough
            if( distSq < 1f ) return;

            // Move closer to target

            pos += m_moveSpeed * Time.deltaTime * targetDir;
            transform.position = pos;
        }
        else
        {
            Vector3 targetDir = m_targetObject.transform.position - pos;
            distSq = Vector3.SqrMagnitude( targetDir );
            targetDir = Vector3.Normalize( targetDir );

            // Rotate to target
            float step = m_rotationSpeed * Time.deltaTime;
        
            var angle = Mathf.Atan2( targetDir.y, targetDir.x ) * Mathf.Rad2Deg + 90;
            Quaternion targetRotation = Quaternion.AngleAxis( angle, Vector3.forward );
            transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, step );

            // Close enough
            if( distSq < m_targetingRangeSq - m_targetingRangeSqFudge ) return;

            // Move closer to target

            pos += m_moveSpeed * Time.deltaTime * targetDir;
            transform.position = pos;
        }
    }
        

    protected override void RemoveFromGameManager()
    {
        m_gameManager.m_playerShips.Remove( this );
    }
}
