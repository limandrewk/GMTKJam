using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent( typeof(PlayerShipSpawnerSystem) )]
public class GameManager : MonoBehaviour
{
    public int m_resources = 0;
    public int m_gatherRate = 10;

    public float m_timer = 1.0f;

    public PlayerShipSpawnerSystem m_playerShipSpawner;

    public List<EnemyShip> m_enemyShips;
    public List<PlayerShip> m_playerShips;
    
    public TMP_Text m_resourcesText;

    public Canvas m_baseUpgradesCanvas;
    public Canvas m_shipyardCanvas;
    
    private void Start()
    {
        m_baseUpgradesCanvas.enabled = false;
        m_shipyardCanvas.enabled = false;
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
    }

    public void OnSaveTemplateClick()
    {
        m_shipyardCanvas.enabled = false;
    }
}

