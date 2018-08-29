using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    public static ShipBehavior instance = null;

    public float m_speed, m_amountOfBullets, m_camMovementSmoothness, m_shootingTimeInSeconds;

    private List<GameObject> m_bullets = new List<GameObject>();

    private Controller m_controller;

    private ObjectManager m_objectManager;

    private Rigidbody2D m_rigidbody;

    private Vector2 m_targetDir, m_tempPosition;

    private Quaternion m_dir;

    private Camera m_cam;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        m_cam = FindObjectOfType<Camera>();
        m_controller = FindObjectOfType<Controller>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_objectManager = GetComponent<ObjectManager>();

        m_bullets = m_objectManager.GetPool(Pool.Bullet, (int)m_amountOfBullets);

        StartCoroutine(Shoot());

        StartCoroutine(CamMovement());
    }

    void Update()
    {
        Controls();     // allow to use controller  
    }

    private void Controls()
    {
        // if controller is in use.. // and not in the state of get hit
        if (Is_usingControls())
        {
            //allow Ship to rotate
            Locomote();

            //allow Ship to move
            Move();
        }
    }

    private GameObject GetBullet()
    {
        foreach (GameObject bullet in m_bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }

    IEnumerator Shoot()
    {
        var m_bullet = GetBullet();

        if (m_bullet != null)
        {
            m_bullet.SetActive(true);
        }

        yield return new WaitForSeconds(m_shootingTimeInSeconds);
        StartCoroutine(Shoot());
    }

    IEnumerator CamMovement()
    {
        m_tempPosition = new Vector2(transform.position.x, transform.position.y);
        m_cam.transform.position = m_tempPosition;

        yield return new WaitForSeconds(m_camMovementSmoothness);

        StartCoroutine(CamMovement());
    }


    //following will be used to check either player is using movement controls or not
    private bool Is_usingControls()
    {
        return m_controller.GetPosition() != Vector2.zero;
    }

    ////** following vector wil be the virtual position which will always be one step ahead of the position of the Ship,
    //// making the movement of the character more smooth and realistic
    private Vector2 VirtualPosition()
    {
        return new Vector2
            (
                //getting the current x axis of the Ship, and adding +1 to the x axis.. returns one step a head of the current position of the Ship
                m_rigidbody.velocity.x + m_controller.GetPosition().x
                ,
                m_rigidbody.velocity.y + m_controller.GetPosition().y
            );
    }

    ////following method is being used for rotating the Ship
    private void Locomote()
    {
        //getting the distance between the virtual position(Whic is one step a head of the current position of the Ship) and the current position of the protagonsit
        m_targetDir = VirtualPosition() - m_rigidbody.velocity;

        float angle = Mathf.Atan2(-m_targetDir.x, m_targetDir.y) * Mathf.Rad2Deg;

        //only forward, backward, left and right direction are required here
        m_dir = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, m_dir, Speed() * m_speed);
    }

    private void Move()
    {
        // This will move the character according to the controller
        transform.position += new Vector3
            (
                (m_controller.GetPosition().x * Speed()) //updating horizontal movement
                ,
                (m_controller.GetPosition().y * Speed())
            );
    }

    private float Speed()
    {
        return m_speed * Time.deltaTime;
    }

}