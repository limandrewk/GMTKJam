using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipyardUIRenderer : MonoBehaviour
{
    public Image m_shipIcon;
    public Image m_laserIcon;
    public Image m_shooterIcon;
    public Image m_beamIcon;
    
    public List<Image> m_hullPips;
    public List<Image> m_armourPips;
    public List<Image> m_speedPips;

    public TMP_Text m_costLabel;
    
    public void UpdateShipyardUI( int lasers, int shooters, int beams, int hull, int armour, int speed )
    {
        foreach ( Transform child in m_shipIcon.rectTransform ) 
        {
            Destroy( child.gameObject );
        }

        // Weapon rendering
        int weaponCount = lasers + shooters + beams;
        if( weaponCount > 0 )
        {
            Vector3 offset = new Vector3( 0, 50, 0 );
            float angle = 360.0f / weaponCount;
            Quaternion angleVector = Quaternion.Euler( new Vector3( 0, 0, angle ) );

            offset = angleVector * offset;

            for( int i = 0; i < lasers; ++i )
            {
                Image icon = Instantiate( m_laserIcon, m_shipIcon.rectTransform );
                icon.rectTransform.localPosition = offset;
                
                offset = angleVector * offset;
            }

            for( int i = 0; i < shooters; ++i )
            {
                Image icon = Instantiate( m_shooterIcon, m_shipIcon.rectTransform );
                icon.rectTransform.localPosition = offset;

                offset = angleVector * offset;
            }

            for( int i = 0; i < beams; ++i )
            {
                Image icon = Instantiate( m_beamIcon, m_shipIcon.rectTransform );
                icon.rectTransform.localPosition = offset;

                offset = angleVector * offset;
            }
        }

        // Ship upgrade rendering
        for( int i = 0; i < m_hullPips.Count; ++i )
        {
            m_hullPips[i].gameObject.SetActive( hull > i );
        }
        
        for( int i = 0; i < m_armourPips.Count; ++i )
        {
            m_armourPips[i].gameObject.SetActive( armour > i );
        }
        
        for( int i = 0; i < m_speedPips.Count; ++i )
        {
            m_speedPips[i].gameObject.SetActive( speed > i );
        }
    }

    public void UpdateShipCost( float cost )
    {
        m_costLabel.text = cost.ToString();
    }
}
