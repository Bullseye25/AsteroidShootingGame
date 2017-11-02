using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pool
{
    Bullet,
};

public class ObjectManager : MonoBehaviour 
{
    public GameObject[] m_objects;

    public static ObjectManager instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    internal List<GameObject> GetPool(Pool a_pool, int a_amount)
    {
        //creating a new gameobject to hold ammos in as a gameobject
        List<GameObject> a_list = new List<GameObject>();

        //checking each object provided in the array
        for (int i = 0; i < m_objects.Length; i++)
        {
            //if one of object's name matches
            if (m_objects[i].name == a_pool.ToString())
            {
                //following loop will create certain amount of ammos
                for (int j = 0; j < a_amount; j++)
                {
                    // creating the object
                    GameObject _object = Instantiate(m_objects[i]) as GameObject;

                    //placing it inside the holder
                    a_list.Add(_object);

                    //disable the object
                    _object.SetActive(false);
                }

                return a_list;
            }
        }
        return null;
    }
}

