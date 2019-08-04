using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent( typeof(PlayerShipSpawnerSystem) )]
public class GameManager : MonoBehaviour
{
    public int m_resources = 0;
    public int m_gatherRate = 10;

    public float m_timer = 1.0f;

    public PlayerShipSpawnerSystem m_playerShipSpawner;
    public EnemyShipSpawnerSystem m_enemyShipSpawner;

    public List<EnemyShip> m_enemyShips;
    public List<PlayerShip> m_playerShips;
    
    public TMP_Text m_resourcesText;
    public TMP_Text m_nextWaveText;

    public Canvas m_baseUpgradesCanvas;
    public Canvas m_shipyardCanvas;

    public ShipyardUIRenderer m_shipyardRenderer;
    
    public int m_shipyardLasers;
    public int m_shipyardShooters;
    public int m_shipyardBeams;
    public int m_shipyardHull;
    public int m_shipyardArmour;
    public int m_shipyardSpeed;

    public AsteroidBase m_base;
    
    private void Start()
    {
        m_baseUpgradesCanvas.enabled = false;
        m_shipyardCanvas.enabled = false;

        m_playerShipSpawner.m_gameManager = this;
    }


    // Update is called once per frame
    private void Update()
    {
        m_timer -= Time.deltaTime;

        while( m_timer < 0 )
        {
            m_resources += m_gatherRate;

            m_timer += 1.0f;
        }

        while( m_resources >= m_playerShipSpawner.m_shipCost )
        {
            m_playerShipSpawner.SpawnShip();

            m_resources -= m_playerShipSpawner.m_shipCost;
        }
        
        m_resourcesText.text = m_resources.ToString();
        m_nextWaveText.text = Mathf.FloorToInt(m_enemyShipSpawner.m_timer).ToString();
        
        if( m_enemyShipSpawner.m_waves.Count == 0 && m_enemyShips.Count == 0 )
            SceneManager.LoadScene( "Victory", LoadSceneMode.Single);

    }

    
    //UI Buttons
    public void OnUpgradesToggleClick()
    {
        m_baseUpgradesCanvas.enabled = !m_baseUpgradesCanvas.isActiveAndEnabled;
        m_shipyardCanvas.enabled = false;
    }

    public void OnShipyardToggleClick()
    {
        m_baseUpgradesCanvas.enabled = false;
        m_shipyardCanvas.enabled = !m_shipyardCanvas.isActiveAndEnabled;
        
        m_shipyardLasers = m_playerShipSpawner.m_spawnerLasers;
        m_shipyardShooters = m_playerShipSpawner.m_spawnerShooters;
        m_shipyardBeams = m_playerShipSpawner.m_spawnerBeams;
        m_shipyardHull = m_playerShipSpawner.m_spawnerHull;
        m_shipyardArmour = m_playerShipSpawner.m_spawnerArmour;
        m_shipyardSpeed = m_playerShipSpawner.m_spawnerSpeed;
        
        UpdateShipyardUI();
    }

    public void OnSaveTemplateClick()
    {
        m_playerShipSpawner.SetSpawnerValues( m_shipyardLasers, m_shipyardShooters, m_shipyardBeams, 
            m_shipyardHull, m_shipyardArmour, m_shipyardSpeed );
        m_shipyardCanvas.enabled = false;
    }
    
    public void OnShipyardBlueButtonUpClick()
    {
        if( m_shipyardShooters + m_shipyardLasers + m_shipyardBeams >= 10 ) return;
        
        ++m_shipyardShooters;
        UpdateShipyardUI();
    }

    public void OnShipyardBlueButtonDownClick()
    {
        if( m_shipyardShooters <= 0 ) return;

        --m_shipyardShooters;
        UpdateShipyardUI();
    }

    public void OnShipyardRedButtonUpClick()
    {
        if( m_shipyardShooters + m_shipyardLasers + m_shipyardBeams >= 10 ) return;
        
        ++m_shipyardLasers;
        UpdateShipyardUI();
    }
    
    public void OnShipyardRedButtonDownClick()
    {
        if( m_shipyardLasers <= 0 ) return;

        --m_shipyardLasers;
        UpdateShipyardUI();
    }

    public void OnShipyardGreenButtonUpClick()
    {
        if( m_shipyardShooters + m_shipyardLasers + m_shipyardBeams >= 10 ) return;
        
        ++m_shipyardBeams;
        UpdateShipyardUI();
    }

    public void OnShipyardGreenButtonDownClick()
    {
        if( m_shipyardBeams <= 0 ) return;

        --m_shipyardBeams;
        UpdateShipyardUI();
    }

    private void UpdateShipyardUI()
    {
        m_shipyardRenderer.UpdateShipyardUI( m_shipyardLasers, m_shipyardShooters, m_shipyardBeams,
            m_shipyardHull, m_shipyardArmour, m_shipyardSpeed );

        float cost = m_playerShipSpawner.CalculateShipCost( m_shipyardLasers, m_shipyardShooters,
            m_shipyardBeams, m_shipyardHull, m_shipyardArmour, m_shipyardSpeed );
        m_shipyardRenderer.UpdateShipCost( cost );
    }

    public void OnShipyardHullButtonUpClick()
    {
        if( m_shipyardHull >= 6 ) return;
        ++m_shipyardHull;
        UpdateShipyardUI();

    }

    public void OnShipyardHullButtonDownClick()
    {
        if( m_shipyardHull <= 0 ) return;
        --m_shipyardHull;
        UpdateShipyardUI();

    }

    public void OnShipyardArmourButtonUpClick()
    {
        if( m_shipyardArmour >= 6 ) return;
        ++m_shipyardArmour;
        UpdateShipyardUI();

    }

    public void OnShipyardArmourButtonDownClick()
    {
        if( m_shipyardArmour <= 0 ) return;
        --m_shipyardArmour;
        UpdateShipyardUI();

    }

    public void OnShipyardSpeedButtonUpClick()
    {
        if( m_shipyardSpeed >= 6 ) return;
        ++m_shipyardSpeed;
        UpdateShipyardUI();
    }

    public void OnShipyardSpeedButtonDownClick()
    {
        if( m_shipyardSpeed <= 0 ) return;
        --m_shipyardSpeed;
        UpdateShipyardUI();
    }

}

