using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprouts : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invisibllize();
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
}
