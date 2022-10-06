using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject floor0;
    public GameObject floor1;

    public GameObject floorType;
    public int width;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        floorType = floor0;
        createFloor();
    }

    private void createFloor()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(floorType, new Vector3(i, j, 0), Quaternion.identity);
                changeFloorType();
            }
                changeFloorType();
        }
    }

    private void changeFloorType()
    {
        if (floorType.Equals(floor0))
        {
            floorType = floor1;
        }
        else
        {
            floorType = floor0;
        }
    }
}
