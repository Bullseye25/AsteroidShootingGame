using UnityEngine;

public class AsteroidCollisionDetection : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D _object)
    {
        if (Is(_object, "Bullet"))
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
        if (Is(_object, "Player"))
        {
            _object.gameObject.SetActive(false);   
            UIManager.instance.GameEndStats("GameOver");
        }
        else
            return;

        this.gameObject.SetActive(false);
    }

    private bool Is(Collider2D _object, string nameOfObject)
    {
        return _object.gameObject.tag == nameOfObject;
    }

    private bool Is(Collision2D _object, string nameOfObject)
    {
        return _object.gameObject.tag == nameOfObject;
    }
}
