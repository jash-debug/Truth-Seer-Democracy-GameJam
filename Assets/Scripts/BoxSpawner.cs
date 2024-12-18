using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBox()
    {
        GameObject boxObj = Instantiate(boxPrefab);
        boxObj.transform.position = transform.position;
    }
}
