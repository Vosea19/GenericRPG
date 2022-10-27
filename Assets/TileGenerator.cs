using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    float width;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        width = tilePrefab.GetComponent<MeshRenderer>().bounds.size.x;
        height = tilePrefab.GetComponent<MeshRenderer>().bounds.size.z;
        print(.75 * height);
        print(width);
        for (int j = 0; j < 100; j++)
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject newTile = GameObject.Instantiate(tilePrefab);
                print(newTile.GetComponent<MeshRenderer>().bounds.size);
                if (j % 2 == 0)
                {
                    newTile.transform.position = ((Vector3.right * ((width * i) + width/2)) + (Vector3.forward * j * (.75f* height)));
                }
                else newTile.transform.position = ((Vector3.right * ((width * i))) + (Vector3.forward * j * (.75f * height)));

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
