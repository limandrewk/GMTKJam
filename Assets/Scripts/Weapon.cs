using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameManager m_gameManager;
    public EnemyShip m_enemyShip;

    public enum Owner { Player, Enemy }
    public int m_weaponType;

    public Projectile m_projectilePrefab;
    
    public int m_range;
    public int m_damage;
    public int m_splashDamage;
    public float m_splashRadius;

    private float m_timer;
    public float m_fireRate;

    public Ship m_target;
    public Vector3 m_hitOffset;

    public Owner m_owner = Owner.Player;

    private bool m_initialised = false;

    // Start is called before the first frame update
    private void Start()
    {
        m_timer = m_fireRate;
    }

    private void Awake()
    {
        if( m_gameManager == null && m_enemyShip != null ) m_gameManager = m_enemyShip.m_gameManager;
    }

    // Update is called once per frame
    private void Update()
    {
        if( !m_initialised )
        {
            if( m_gameManager == null && m_enemyShip != null ) m_gameManager = m_enemyShip.m_gameManager;
            m_initialised = true;
        }
        
        m_timer -= Time.deltaTime;

        if( m_timer < 0 )
        {
            if( !FindTarget() )
            {
                m_timer = 0;
                return;
            }

            Vector3 pos = transform.position;
            Vector3 targetDir = Vector3.Normalize( m_target.transform.position - pos );
            var angle = Mathf.Atan2( targetDir.y, targetDir.x ) * Mathf.Rad2Deg + 90;
            Quaternion rotation = Quaternion.AngleAxis( angle, Vector3.forward );

            Projectile projectile = Instantiate( m_projectilePrefab, pos, rotation );
            projectile.m_targetObject = m_target;
            projectile.m_damage = m_damage;
            
            m_timer += m_fireRate;
        }
    }

    private bool FindTarget()
    {
        float distSq;
        Vector3 pos = transform.position;
        if( m_target != null )
        {
            distSq = Vector3.SqrMagnitude(m_target.transform.position - pos );
            if( distSq > m_range ) m_target = null;
        }

        if( m_target != null ) return true;
        
        float minDistSq = -1;
        m_target = null;

        if( m_owner == Owner.Player )
        {
            foreach( EnemyShip enemyShip in m_gameManager.m_enemyShips )
            {
                distSq = Vector3.SqrMagnitude( enemyShip.transform.position - pos );
                if( distSq > m_range ) continue;

                if( m_target == null )
                {
                    minDistSq = distSq;
                    m_target = enemyShip;
                }
                else
                {
                    if( distSq < minDistSq )
                    {
                        minDistSq = distSq;
                        m_target = enemyShip;
                    }
                }
            }
        }
        else
        {
            foreach( PlayerShip playerShip in m_gameManager.m_playerShips )
            {
                distSq = Vector3.SqrMagnitude( playerShip.transform.position - pos );
                if( distSq > m_range ) continue;

                if( m_target == null )
                {
                    minDistSq = distSq;
                    m_target = playerShip;
                }
                else
                {
                    if( distSq < minDistSq )
                    {
                        minDistSq = distSq;
                        m_target = playerShip;
                    }
                }
            }

            if( m_target == null )
            {
                distSq = Vector3.SqrMagnitude( m_gameManager.m_base.transform.position - pos );
                if( distSq < m_range ) m_target = m_gameManager.m_base;
            }
        }

        return m_target != null;
    }
}
