using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool m_initalised = true;
    
    public Ship m_targetObject;
    public Vector3 m_targetPos;

    public Vector3 m_offset;
    public float m_speed = 10;

    public int m_damage = 5;
    
    public GameObject m_explosionPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        m_offset = new Vector3( Random.Range( -1.5f, 1.5f ), Random.Range( -1.5f, 1.5f ), 0 );
    }

    // Update is called once per frame
    void Update()
    {
        if( !m_initalised )
        {
            if( m_targetObject == null )
            {
                Destroy( gameObject );
                return;
            }
        
            m_initalised = true;
            m_targetPos = m_targetObject.transform.position;
        }

        if( m_targetObject != null ) m_targetPos = m_targetObject.transform.position;

        Vector3 pos = transform.position;
        Vector3 targetDir = m_targetPos - pos + m_offset;
        float distSq = Vector3.SqrMagnitude( targetDir );

        if( distSq < 10f )
        {
            if( m_explosionPrefab != null ) Instantiate( m_explosionPrefab, pos, Quaternion.identity );
            if( m_targetObject != null ) m_targetObject.Damage( m_damage );
            
            Destroy( gameObject );
        }

        targetDir = Vector3.Normalize( targetDir );
        pos += m_speed * Time.deltaTime * targetDir;
        transform.position = pos;
    }
}
