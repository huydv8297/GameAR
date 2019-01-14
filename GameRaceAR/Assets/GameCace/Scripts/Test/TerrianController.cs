using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TerrianController : MonoBehaviour {

    public Terrain terr; // terrain to modify
    int hmWidth; // heightmap width
    int hmHeight; // heightmap height

    int posXInTerrain; // position of the game object in terrain width (x axis)
    int posYInTerrain; // position of the game object in terrain height (z axis)

    public int size = 10; // the diameter of terrain portion that will raise under the game object
    public float desiredHeight = 0; // the height we want that portion of terrain to be
    public float percentSmooth = 0f;
    void Start()
    {

        terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapWidth;
        hmHeight = terr.terrainData.heightmapHeight;

    }

    public void Dig(Vector3 position)
    {
        

            // get the normalized position of this game object relative to the terrain
        Vector3 tempCoord = (position - terr.gameObject.transform.position);
        Vector3 coord;
        coord.x = tempCoord.x / terr.terrainData.size.x;
        coord.y = tempCoord.y / terr.terrainData.size.y;
        coord.z = tempCoord.z / terr.terrainData.size.z;

        // get the position of the terrain heightmap where this game object is
        posXInTerrain = (int)(coord.x * hmWidth);
        posYInTerrain = (int)(coord.z * hmHeight);

        // we set an offset so that all the raising terrain is under this game object
        int offset = size / 2;

        // get the heights of the terrain under this game object
        float[,] heights = terr.terrainData.GetHeights(posXInTerrain - offset, posYInTerrain - offset, size, size);

        // we set each sample of the terrain in the size to the desired height
        
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                //heights[i, j] = 0.6f *(i/size)* desiredHeight;
                    if (i < size * percentSmooth || i > (size * (1 -percentSmooth)) ||
                        j < size * percentSmooth || i > (size * (1 - percentSmooth)))
                    {
                        heights[i, j] = ((float)i / size) * desiredHeight;
                    }
                    else
                            heights[i, j] = desiredHeight;
                   }


            // go raising the terrain slowly
            //desiredHeight += Time.deltaTime;

            // set the new height
           // Smooth(heights);
            
            terr.terrainData.SetHeights(posXInTerrain - offset, posYInTerrain - offset, heights);
        
        
        

    }


    void Smooth(float[,] heights)
    {
        StringBuilder temp = new StringBuilder();
        float sum = 0;
        for (int i = 1; i < size - 1; i++)
        { 
            for (int j = 1; j < size - 1; j++)
            {
                sum = heights[i - 1, j - 1] + heights[i - 1, j] + heights[i - 1, j + 1] + heights[i + 1, j - 1] + heights[i + 1, j] + heights[i + 1, j + 1] + heights[i, j - 1] + heights[i, j + 1];
                heights[i, j] = sum/8;
                temp.Append(heights[i, j] + " ");
            }
            temp.Append("\n");
        }
        Debug.Log(temp.ToString());
    }
}
