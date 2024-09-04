using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour, ICrops
{
    GameObject model;
    int growDay
    {
        get
        {
            return _growDay++;
        }
        set
        {
            _growDay = value;
        }
    }
    int _growDay = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        Invisibllize();

        growDay = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Invisibllize()
    {
        foreach (Transform o in GetComponentInChildren<Transform>())
        {
            o.gameObject.SetActive(false);
        }
    }

    public void Visulalize()
    {
        foreach (Transform o in GetComponentInChildren<Transform>())
        {
            o.gameObject.SetActive(true);
        }
    }

    public int GetGrowDay()
    {
        return growDay;
    }

    public void Grow()
    {
        Visulalize();
    }

    public void Harvest()
    {

    }
}
