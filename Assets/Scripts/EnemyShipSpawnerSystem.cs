using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawnerSystem : MonoBehaviour
{
    public GameManager m_gameManager;
    
    public EnemyShip m_redShip;
    public EnemyShip m_redPlusShip;
    public EnemyShip m_blueShip;
    public EnemyShip m_bluePlusShip;
    public EnemyShip m_greenShip;
    public EnemyShip m_bossShip;

    public struct WaveInfo
    {        
        public int m_waveTime;

        public int m_rNum;
        public int m_rpNum;
        public int m_bNum;
        public int m_bpNum;
        public int m_gNum;
        public int m_bossNum;

        public WaveInfo( int t, int r, int rp, int b, int bp, int g, int boss )
        {
            m_waveTime = t;
            m_rNum = r;
            m_rpNum = rp;
            m_bNum = b;
            m_bpNum = bp;
            m_gNum = g;
            m_bossNum = boss;
        }
    }

    public List<WaveInfo> m_waves;
    public float m_timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        m_waves = new List<WaveInfo>
        {
            new WaveInfo( 20, 10, 0, 0, 0, 0, 0 ),
            new WaveInfo( 50, 0, 0, 10, 2, 0, 0 ),
            new WaveInfo( 40, 10, 0, 0, 0, 10, 0 ),
            new WaveInfo( 60, 10, 0, 10, 0, 0, 0 ),
            new WaveInfo( 60, 10, 10, 0, 0, 0, 0 ),
            new WaveInfo( 60, 10, 10, 10, 0, 10, 1 )
        };
        m_timer = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if( m_waves.Count == 0 )
        {
            m_timer = 0;
            return;
        }

        if( m_waves.Count == 1 ) m_timer = 0;
        
        m_timer -= Time.deltaTime;

        if( m_timer < 0 )
        {
            if( m_waves.Count > 1 ) m_timer = m_waves[1].m_waveTime;

            WaveInfo info = m_waves[0];
            
            for( int i = 0; i < info.m_rNum; ++i )
            {
                EnemyShip ship = Instantiate( m_redShip, GenerateSpawnPosition(), Quaternion.identity );
                ship.m_gameManager = m_gameManager;
                m_gameManager.m_enemyShips.Add( ship );
            }
        
            for( int i = 0; i < info.m_rpNum; ++i )
            {
                EnemyShip ship = Instantiate( m_redPlusShip, GenerateSpawnPosition(), Quaternion.identity );
                ship.m_gameManager = m_gameManager;
                m_gameManager.m_enemyShips.Add( ship );
            }

            for( int i = 0; i < info.m_bNum; ++i )
            {
                EnemyShip ship = Instantiate( m_blueShip, GenerateSpawnPosition(), Quaternion.identity );
                ship.m_gameManager = m_gameManager;
                m_gameManager.m_enemyShips.Add( ship );
            }

            for( int i = 0; i < info.m_bpNum; ++i )
            {
                EnemyShip ship = Instantiate( m_bluePlusShip, GenerateSpawnPosition(), Quaternion.identity );
                ship.m_gameManager = m_gameManager;
                m_gameManager.m_enemyShips.Add( ship );
            }
            
            for( int i = 0; i < info.m_gNum; ++i )
            {
                EnemyShip ship = Instantiate( m_greenShip, GenerateSpawnPosition(), Quaternion.identity );
                ship.m_gameManager = m_gameManager;
                m_gameManager.m_enemyShips.Add( ship );
            }
            
            for( int i = 0; i < info.m_bossNum; ++i )
            {
                EnemyShip ship = Instantiate( m_bossShip, GenerateSpawnPosition(), Quaternion.identity );
                ship.m_gameManager = m_gameManager;
                m_gameManager.m_enemyShips.Add( ship );
            }
            
            m_waves.RemoveAt( 0 );
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        Vector3 value = new Vector3( Random.Range( -20f, 20f ), Random.Range( -20f, 20f ), 0 );
        int dir = Random.Range( 0, 4 );
        
        if( dir == 0 ) value.x -= 200;
        else if( dir == 1 ) value.x += 200;
        else if( dir == 2 ) value.y -= 200;
        else value.y += 200;
        
        
        
        return value;
    }
    
}
