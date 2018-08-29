using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidActivator : MonoBehaviour
{
    public float m_miniToPush, m_maxToPush;

    void OnTriggerEnter2D(Collider2D _object)
    {
        if (!IsColliding(_object, "Player") && IsColliding(_object, "Astro"))
            ActivateAsteroid(_object);
        else
            return;
    }

    void OnTriggerExit2D(Collider2D _object)
    {
        if (!IsColliding(_object, "Player") && IsColliding(_object, "Astro"))
            DeactivateAsteroid(_object);
        else
            return;
    }

    private bool IsColliding(Collider2D _object, string _objectName)
    {
        return _object.gameObject != null && _object.gameObject.tag == _objectName;
    }

    private void ActivateAsteroid(Collider2D _object)
    {
        var _rigidbody = _object.GetComponent<Rigidbody2D>();

        _rigidbody.WakeUp();

        float _force = Random.Range(m_miniToPush, m_maxToPush) * Time.smoothDeltaTime;

        _rigidbody.AddForce(GetRandomDirection() * _force, ForceMode2D.Impulse);
    }

    private void DeactivateAsteroid(Collider2D _object)
    {
        var _rigidbody = _object.GetComponent<Rigidbody2D>();

        _rigidbody.Sleep();

        _rigidbody.velocity = Vector2.zero;
    }

    private Vector3 GetRandomDirection()
    {
        int randomNum = Random.Range(1, 9);

        switch (randomNum)
        {
            case 1:
                return new Vector3(0, 1);
            case 2:
                return new Vector3(1, 0);
            case 3:
                return new Vector3(1, 1);
            case 4:
                return new Vector3(-1, 1);
            case 5:
                return new Vector3(0, -1);
            case 6:
                return new Vector3(-1, 0);
            case 7:
                return new Vector3(-1, -1);
            default:
                return new Vector3(1, -1);
        }
    }

}