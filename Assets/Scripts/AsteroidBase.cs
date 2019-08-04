using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidBase : Ship
{
    public int m_healthRegen = 1;
    private float m_timer = 1f;
    
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if( m_health <= 0 )
        {
            if( m_explosionPrefab != null ) Instantiate( m_explosionPrefab, transform.position, Quaternion.identity );

            // You Lose
            SceneManager.LoadScene( "GameOver", LoadSceneMode.Single);

        }

        m_timer -= Time.deltaTime;
        while( m_timer <= 0 )
        {
            m_timer += 1;

            m_health += m_healthRegen;
        }

    }
        

    protected override void RemoveFromGameManager()
    {
        // This is held separately from the arrays
    }
}
