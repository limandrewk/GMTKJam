using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Canvas m_pauseMenu;
    private Camera m_camera;
    
    public float m_moveSensitivity = 1;

    public float m_zoomSensitivity = 1;
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
        Vector3 pos = gameObject.transform.position;
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
        pos.x = Mathf.Clamp( pos.x, -100 + cameraSize, 100 - cameraSize );
        pos.y = Mathf.Clamp( pos.y, -100 + cameraSize, 100 - cameraSize );

        gameObject.transform.position = pos;
        m_camera.orthographicSize = cameraSize;
    }
}
