using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.XR;

[RequireComponent( typeof(SpriteRenderer) )]
public class ChangeLayerOrderAfterTime : MonoBehaviour
{
    public float m_countdown = 5f;
    public int m_newOrderInLayer = 0;
    private SpriteRenderer m_renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_countdown -= Time.deltaTime;
        if( m_countdown > 0 ) return;
        
        m_renderer.sortingOrder = m_newOrderInLayer;
        Destroy( this );
    }
}
