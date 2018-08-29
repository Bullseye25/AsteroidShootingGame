using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    private float xaxis, yaxis;
    private Vector3 m_postion;

    void Update()
    {
        m_postion = Position();
    }

    private Vector2 Position()
    {

        xaxis = Input.GetAxis("Horizontal");
        yaxis = Input.GetAxis("Vertical");

        // if player is using the controls...
        if (Input.GetAxis("Horizontal") != 0
            ||
            Input.GetAxis("Vertical") != 0
            ||
            Input.GetAxis("Horizontal") != 0
            &&
            Input.GetAxis("Vertical") != 0)
        {
            // the virtual position will move according to the player controls
            Vector2 a_norm = new Vector2(xaxis, yaxis);

            return a_norm;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public Vector2 GetPosition()
    {
        return m_postion;
    }
}