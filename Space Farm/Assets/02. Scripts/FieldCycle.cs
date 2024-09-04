using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FieldCycle : MonoBehaviour
{
    GameObject seeds;
    GameObject sprout;
    ICrops Crops;

    float time;

    enum State
    {
        none,
        seed,
        sprout,
        crop
    }
    State state;

    // Start is called before the first frame update
    void Awake()
    {
        seeds = GetComponentInChildren<Seeds>().gameObject;
        sprout = GetComponentInChildren<Sprouts>().gameObject;
        Crops = GetComponentInChildren<ICrops>();

        state = State.none;
        time = 0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.none || state == State.crop) return;

        time += Time.deltaTime;

        if (time > 4f /* 4320 * Crops.GetGrowDay()*/)
        {
            seeds.SetActive(false);
            Grow();
            time = 0f;
        }
    }

    public void Sowing() // ����
    {
        Debug.Log("����");
        state = State.seed;
        seeds.GetComponent<Seeds>().Visulalize();
    }
    void Grow() // ����
    {
        if (state == State.seed) // �� > ��
        {
            Debug.Log("����");
            seeds.GetComponent<Seeds>().Invisibllize();
            sprout.GetComponent<Sprouts>().Visulalize();
            state = State.sprout;
        }
        else if(state == State.sprout) // �� > ����Ϸ�
        {
            Crops.Grow();
            sprout.GetComponent<Sprouts>().Invisibllize();
        }
    }

    public void OnMouseDown()
    {
        Sowing();
    }
}
