using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class RiverOffsetMov : MonoBehaviour
{
    [SerializeField]Terrain terrain;
    [SerializeField]TerrainLayer water;

    [SerializeField] float x,y;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(TerrainPaintUtility.FindTerrainLayerIndex(terrain, water));
        if(water != null)
            water.tileOffset = new Vector2(5 * Time.time, 0 );
    }
}
