using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class RiverOffsetMov : MonoBehaviour
{
    [SerializeField]Terrain terrain;
    [SerializeField] TerrainLayer water;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(TerrainPaintUtility.FindTerrainLayerIndex(terrain, water));
        water.tileOffset = new Vector2(0, 5 * Time.time);
    }
}
