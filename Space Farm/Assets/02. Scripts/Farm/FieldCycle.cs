using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FieldCycle : MonoBehaviour
{
    public GameObject popUp;

    GameObject seeds;
    GameObject sprout;
    GameObject adult;

    float time;

    GameManager gmInstace;
    enum State
    {
        none,
        seed,
        sprout,
        crops
    }
    State state;

    // Start is called before the first frame update
    void Awake()
    {
        gmInstace = FindObjectOfType<GameManager>();

        seeds = GetComponentInChildren<Seeds>().gameObject;
        sprout = GetComponentInChildren<Sprouts>().gameObject;
        //Crops = GetComponentInChildren<ICrops>();

        state = State.none;
        time = 0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.none || state == State.crops) return;

        time += Time.deltaTime;

        if (time > 4f /* 4320 * Crops.GetGrowDay()*/)
        {
            seeds.SetActive(false);
            Grow();
            time = 0f;
        }
    }

    public void Sowing(SeedData _seed)
    {
        state = State.seed;

        seeds = _seed.Seed;
        sprout = _seed.Sprout;
        adult = _seed.Adult;
    }
    void Grow()
    {
        if (state == State.seed)
        {
            seeds.GetComponent<Seeds>().Invisibllize();
            sprout.GetComponent<Sprouts>().Visulalize();
            state = State.sprout;
        }
        else if(state == State.sprout)
        {
            //Crops.Grow();
            sprout.GetComponent<Sprouts>().Invisibllize();
        }
    }

    private void OnMouseDown()
    {
        if (gmInstace.toolState != ToolState.trowel) return;
        popUp.SetActive(true);
        popUp.transform.position = Input.mousePosition;
    }
}
