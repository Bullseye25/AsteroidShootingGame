using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform m_ship, m_startingPoint;
    private Rigidbody2D m_rigidbody;
    private Controller m_controller;
    private Vector3 m_forward;
    public float m_speed;

    // Use this for initialization
    void OnEnable()
    {
        if (m_ship == null)
            m_ship = ShipBehavior.instance.transform;

        if (m_rigidbody == null)
            m_rigidbody = GetComponent<Rigidbody2D>();

        if (m_controller == null)
            m_controller = FindObjectOfType<Controller>();

        m_startingPoint = m_ship.GetChild(0);

        transform.position = m_startingPoint.position;

        m_forward = new Vector3(-m_startingPoint.right.y, m_startingPoint.right.x, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_forward * Speed();

        StartCoroutine(DeactivateBullet());
    }

    private float Speed()
    {
        return m_speed * Time.smoothDeltaTime;
    }

    IEnumerator DeactivateBullet()
    {
        yield return new WaitForSeconds(3f);

        this.gameObject.SetActive(false);
    }
}

