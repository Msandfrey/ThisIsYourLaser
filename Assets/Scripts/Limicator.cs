﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limicator : MonoBehaviour
{
    public static LimicatorObject limicatorObj;
    public GameObject limicator;
    public Sprite[] sprites;

    // Use this for initialization
    void Start()
    {
        limicatorObj = new LimicatorObject(limicator, sprites);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public class LimicatorObject 
    {
        public GameObject[,] stones = new GameObject[2, 10];
        //[HideInInspector]
        public int p1StonesPlaced;
        //[HideInInspector]
        public int p2StonesPlaced;
        public Sprite[] sprites;
        public GameObject limicator;

        public LimicatorObject(GameObject limiter, Sprite[] sprits)
        {
            limicator = limiter;
            sprites = sprits;
            for(int i = 0; i< 2; i++){
                for (int j = 0; j< 10; j++)
                {
                    GameObject limit = Instantiate(limicator);    //makes transparent planes on each grid square
                    limit.transform.localPosition = i == 0 ? new Vector3(-6.3f, 0f, 3.3f - j* (.75f)) : new Vector3(6.3f, 0f, 3.3f - j* (.75f));
                    limit.transform.Rotate(90, 0, 0);
                    limit.transform.localScale = new Vector3(.1f, .1f, .1f);
                    stones[i, j] = limit;
                }
            }
            p1StonesPlaced = 0;
            p2StonesPlaced = 0;
        }
        

        public void changeStones(int i, State state)
        {
            int stonesPlaced = i == 0 ? p1StonesPlaced : p2StonesPlaced;
            if (state == State.placing)
            {
                if (i == 0)
                {
                    stones[i, p1StonesPlaced].GetComponent<SpriteRenderer>().sprite = sprites[1];
                    p1StonesPlaced += 1;
                }
                else
                {
                    stones[i, p2StonesPlaced].GetComponent<SpriteRenderer>().sprite = sprites[1];
                    p2StonesPlaced += 1;
                }
            }
            else if (state == State.removing)
            {
                if (i == 0)
                {
                    p1StonesPlaced -= 1;
                    stones[i, p1StonesPlaced].GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
                else
                {
                    p2StonesPlaced -= 1;
                    stones[i, p2StonesPlaced].GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
            }
        }
    }
}