using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject floor0;
    public GameObject floor1;

    public GameObject floorType;
    public float width;
    public float height;
    public float tileDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        floorType = floor0;
        createFloor();
    }

    private void createFloor()
    {
        for (float i = 0; i < width; i+=tileDistance)
        {
            for (float j = 0; j < height; j+=tileDistance)
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
