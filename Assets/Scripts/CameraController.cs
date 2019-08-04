using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Canvas m_pauseMenu;
    private Camera m_camera;
    
    public float m_moveSensitivity = 1;

    public float m_zoomSensitivity = 1;
    public float m_cameraBounds = 150;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if( m_pauseMenu != null && m_pauseMenu.isActiveAndEnabled ) return;

        float deltaTime = Time.deltaTime;
        Vector3 pos = transform.position;
        float cameraSize = m_camera.orthographicSize;
        
        if( Sinput.GetButtonRaw( "Left" ) )
        {
            pos.x -= m_moveSensitivity * deltaTime;
        }
        if( Sinput.GetButtonRaw( "Right" ) )
        {
            pos.x += m_moveSensitivity * deltaTime;
        }
        if( Sinput.GetButtonRaw( "Up" ) )
        {
            pos.y += m_moveSensitivity * deltaTime;
        }
        if( Sinput.GetButtonRaw( "Down" ) )
        {
            pos.y -= m_moveSensitivity * deltaTime;
        }
        if( Sinput.GetButtonRaw( "ZoomIn" ) )
        {
            cameraSize -= m_zoomSensitivity * deltaTime;
        }
        if( Sinput.GetButtonRaw( "ZoomOut" ) )
        {
            cameraSize += m_zoomSensitivity * deltaTime;
        }

        cameraSize = Mathf.Clamp( cameraSize, 10, 75 );
        pos.x = Mathf.Clamp( pos.x, -m_cameraBounds + cameraSize, m_cameraBounds - cameraSize );
        pos.y = Mathf.Clamp( pos.y, -m_cameraBounds + cameraSize, m_cameraBounds - cameraSize );

        transform.position = pos;
        m_camera.orthographicSize = cameraSize;
    }
}
