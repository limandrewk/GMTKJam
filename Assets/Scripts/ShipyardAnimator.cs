using UnityEngine;

public class ShipyardAnimator : MonoBehaviour
{
    public float m_rotationSpeed = 1.0f;

    // Update is called once per frame
    private void Update()
    {
        float rotation = m_rotationSpeed * Time.deltaTime;
        gameObject.transform.Rotate( 0,0,rotation );
    }
}
