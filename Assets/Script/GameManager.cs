using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
   

    [SerializeField]
    public Grid grid;

    public GameObject m_asteroid;
    public int m_spaceBetweenAsteroids;
    public int m_amountOfAsteroides;
    public float m_shipStartingTime, m_asteroidDelayTime;

    public GameObject m_border;

    private Vector3[] m_positions;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Screen.SetResolution(grid.m_width, grid.m_height, false);
    }

    // Use this for initialization
    void Start()
    {
        //var resolution = Screen.currentResolution;

        var x = grid.m_width;//resolution.width;

        var y = grid.m_height;//resolution.height;

        x *= 4;

        y *= 4;

        ArrangeBorders(x, y);

        ManagePlayerPosition(x, y);

        x = grid.m_width;//resolution.width;

        y = grid.m_height;//resolution.height;

        m_amountOfAsteroides = x * y;

        m_positions = GetPoistions();

        StartCoroutine(ManageAsteroids(m_asteroidDelayTime));

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
        yield return new WaitForSeconds(m_shipStartingTime - 1);
        UIManager.instance.m_countDownPanal.SetActive(false);
    }

    Vector3[] GetPoistions()
    {
        var matrix = (int)Mathf.Sqrt(m_amountOfAsteroides);

        var x = transform.position.x;
        var y = transform.position.y;

        var a_positions = new List<Vector3>();

        for (int row = 0; row < matrix - 1; row++)
        {
            for (int column = 0; column < matrix - 1; column++)
            {
                a_positions.Add(new Vector3(x, y, 2f));

                x += m_spaceBetweenAsteroids;
            }

            x = transform.position.x;
            y += m_spaceBetweenAsteroids;
        }

        return a_positions.ToArray();
    }

    int d = 0;

    IEnumerator ManageAsteroids(float wait)
    {
        for (int i = 0; i < m_positions.Length;)
        {
            yield return new WaitForSeconds(wait);

            for (int _i = 0; _i < m_positions.Length / 5; _i++)
            {
                if (i >= m_positions.Length)
                    break;

                Instantiate(m_asteroid, m_positions[i], Quaternion.identity);
                d++;
                i++;
            }
        }

        Debug.Log("Done: " + d);
    }

    private void ManagePlayerPosition(int x, int y)
    {
        transform.position = new Vector3(x / -1.5f, y / 5, 0);

        ShipBehavior.instance.gameObject.SetActive(false);

        ShipBehavior.instance.transform.position = new Vector3(0, (y + x) / 2, 2);
        Camera.main.transform.position = new Vector2(0, (y + x) / 2);
        UIManager.instance.transform.position = new Vector2(0, (y + x) / 2);
    }

    private void ArrangeBorders(int x, int y)
    {
        var topBorderPosition = new Vector2(0, y + x);
        var footerBorderPosition = new Vector2(0, 0);
        var rightBorderPosition = new Vector2(x, y);
        var leftBorderPosition = new Vector2(-x, y);

        var borderSize = new Vector3(x * 2, y / 10, 1);

        var top = Instantiate(m_border, topBorderPosition, Quaternion.identity);
        top.transform.localScale = borderSize;
        top.name = "Top";

        var down = Instantiate(m_border, footerBorderPosition, Quaternion.identity);
        down.transform.localScale = borderSize;
        down.name = "Down";

        var right = Instantiate(m_border, rightBorderPosition, Quaternion.Euler(0, 0, 90));
        right.transform.localScale = borderSize;
        right.name = "Right";

        var left = Instantiate(m_border, leftBorderPosition, Quaternion.Euler(0, 0, 90));
        left.transform.localScale = borderSize;
        left.name = "Left";

    }
}

[System.Serializable]
public class Grid
{
    public int m_width, m_height;
}