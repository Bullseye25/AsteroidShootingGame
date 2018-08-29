using UnityEngine;

public class AsteroidCollisionDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _object)
    {
        Debug.Log("Enter: " + _object.name);

        if (IsColliding(_object, "Bullet"))
        {
            _object.gameObject.SetActive(false);
            UIManager.instance.AddScore();
        }
        else
            return;


        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D _object)
    {
        Debug.Log("Enter: " + _object.gameObject.name);

        if (IsColliding(_object, "Player"))
        {
            _object.gameObject.SetActive(false);
            UIManager.instance.GameEndStats("GameOver");
        }
        else
            return;

        this.gameObject.SetActive(false);
    }

    private bool IsColliding(Collider2D _object, string nameOfObject)
    {
        return _object.gameObject.tag == nameOfObject;
    }

    private bool IsColliding(Collision2D _object, string nameOfObject)
    {
        return _object.gameObject.tag == nameOfObject;
    }
}
