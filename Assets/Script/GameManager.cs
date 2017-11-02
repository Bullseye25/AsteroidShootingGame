using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    
    [Header("Asteroid Prefab")]
    public GameObject m_asteroid;

    [Header("")]
    public int m_spaceBetweenAsteroids, m_matrix;
    public float m_shipStartingTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start () 
    {
        ShipBehavior.instance.gameObject.SetActive(false);

        Screen.SetResolution(640, 480, true);

        StartCoroutine(ManageAsteroids(m_shipStartingTime/20));

        StartCoroutine(CountDownDisable());

        StartCoroutine(ActiveController());
	}

    IEnumerator ActiveController()
    {
        yield return new WaitForSeconds(m_shipStartingTime);
        ShipBehavior.instance.gameObject.SetActive(true);
    }

    IEnumerator CountDownDisable()
    {
        yield return new WaitForSeconds(m_shipStartingTime-1);
        UIManager.instance.m_countDownPanal.SetActive(false);
    }
	
    IEnumerator ManageAsteroids(float wait)
    {
        yield return new WaitForSeconds(wait);

        int
        matrix = (int)Mathf.Sqrt(m_matrix),
        x = 0,
        y = 0;

        for (int row = 0; row < matrix; row++)
        {
            yield return new WaitForSeconds(0.0005f);
            for (int column = 0; column < matrix; column++)
            {
                Instantiate(m_asteroid, new Vector3(x, y, 2f), Quaternion.identity);

                x += m_spaceBetweenAsteroids;
            }

            x = 0;
            y += m_spaceBetweenAsteroids;
        }
    }
}
