using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int m_weaponType;

    public GameObject m_projectilePrefab;
    
    public int m_range;
    public int m_damage;
    public int m_splashDamage;
    public float m_splashRadius;

    private float m_timer;
    public float m_fireRate;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;

        if( m_timer < 0 )
        {
            //TODO:Shooting things
            
            
            m_timer += m_fireRate;
        }
    }
}
