using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpanwerManager : NetworkBehaviour
{
    // Start is called before the first frame update
    public static SpanwerManager instance;
    public List<Vector2> spawnPositions;
    public List<Vector2> availablePositions;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
      //if (IsServer)
      //  {
      //  }
        availablePositions = new List<Vector2>(spawnPositions);
        foreach(Player player in GameManager.instance.players)
        {
            var posInArray = Random.Range(0, availablePositions.Count);
            player.transform.position = spawnPositions[posInArray];
            Debug.Log(availablePositions);
            availablePositions.RemoveAt(posInArray);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
