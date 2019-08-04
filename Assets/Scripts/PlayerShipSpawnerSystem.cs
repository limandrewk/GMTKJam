using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipSpawnerSystem : MonoBehaviour
{
    public GameManager m_gameManager;
    
    public int m_baseCost = 25;
    [HideInInspector]
    public int m_shipCost = 0;

    public PlayerShip m_shipBase;
    public Weapon m_laserWeaponPrefab;
    public Weapon m_shooterWeaponPrefab;
    public Weapon m_beamWeaponPrefab;
    
    public int m_spawnerLasers = 0;
    public int m_spawnerShooters = 0;
    public int m_spawnerBeams = 0;
    public int m_spawnerHull = 0;
    public int m_spawnerArmour = 0;
    public int m_spawnerSpeed = 0;

    public int m_costLasers = 5;
    public int m_costShooters = 4;
    public int m_costBeams = 8;
    public int[] m_costHull = {2, 5, 9, 13, 18, 23};
    public int[] m_costArmour = {1, 2, 5, 10, 18, 28};
    public int[] m_costSpeed = {2, 5, 8, 12, 16, 20};

    // Start is called before the first frame update
    void Start()
    {
        m_shipCost = m_baseCost;
    }

    public int CalculateShipCost()
    {
        return CalculateShipCost( m_spawnerLasers, m_spawnerShooters, m_spawnerBeams,
            m_spawnerHull, m_spawnerArmour, m_spawnerSpeed );
    }
    
    public int CalculateShipCost( int lasers, int shooters, int beams, int hull, int armour, int speed )
    {
        int cost = m_baseCost;
        cost += m_costLasers * lasers;
        cost += m_costShooters * shooters;
        cost += m_costBeams * beams;
        cost += m_costHull[hull];
        cost += m_costArmour[armour];
        cost += m_costSpeed[speed];
        return cost;
    }

    public void SetSpawnerValues( int lasers, int shooters, int beams, int hull, int armour, int speed )
    {
        m_spawnerLasers = lasers;
        m_spawnerShooters = shooters;
        m_spawnerBeams = beams;
        m_spawnerHull = hull;
        m_spawnerArmour = armour;
        m_spawnerSpeed = speed;

        m_shipCost = CalculateShipCost();
    }
    
    public void SpawnShip()
    {
        PlayerShip newShip = Instantiate( m_shipBase, Vector3.zero, Quaternion.identity );
        newShip.m_health += m_spawnerHull * 10;
        newShip.m_armour = m_spawnerArmour;
        newShip.m_moveSpeed += m_spawnerSpeed;

        newShip.m_gameManager = m_gameManager;

        int lasers = m_spawnerLasers + 1;
        for( int i = 0; i < lasers; ++i )
        {
            Vector3 offset = new Vector3( Random.Range( -1f, 1f ), Random.Range( -1f, 1f ), 0 );
            Weapon weapon = Instantiate( m_laserWeaponPrefab, offset, Quaternion.identity, newShip.transform );
            weapon.m_gameManager = m_gameManager;
        }
        
        for( int i = 0; i < m_spawnerShooters; ++i )
        {
            Vector3 offset = new Vector3( Random.Range( -1f, 1f ), Random.Range( -1f, 1f ), 0 );
            Weapon weapon = Instantiate( m_shooterWeaponPrefab, offset, Quaternion.identity, newShip.transform );
            weapon.m_gameManager = m_gameManager;
        }
        
        for( int i = 0; i < m_spawnerBeams; ++i )
        {
            Vector3 offset = new Vector3( Random.Range( -1f, 1f ), Random.Range( -1f, 1f ), 0 );
            Weapon weapon = Instantiate( m_beamWeaponPrefab, offset, Quaternion.identity, newShip.transform );
            weapon.m_gameManager = m_gameManager;
        }

        if( m_spawnerShooters > lasers && m_spawnerShooters > m_spawnerBeams )
            newShip.m_targetingRangeSq = m_shooterWeaponPrefab.m_range;
        else if( m_spawnerBeams > lasers && m_spawnerBeams > m_spawnerShooters )
            newShip.m_targetingRangeSq = m_beamWeaponPrefab.m_range;
        else
            newShip.m_targetingRangeSq = m_laserWeaponPrefab.m_range;

        m_gameManager.m_playerShips.Add( newShip );
        
    }
}
