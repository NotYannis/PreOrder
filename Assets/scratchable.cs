using UnityEngine;
using System.Collections;

public class scratchable : MonoBehaviour
{
    int square = 10;
    int pixelX = 0;
    int pixelY = 0;
    int iPixelX = -1;

    void Start()
    {
        Texture2D texture = new Texture2D(1750, 800);
        // Texture2D texture = new Texture2D(400, 400);
        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = ((x & y) != 0 ? Color.white : Color.gray);
                // Color color = Color.gray;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }

    void Update()
    {
        /*
         * Move the mouse to scratch the screen 
        */
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray))
            {
                Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                int posX = Mathf.RoundToInt(pos.x);
                int posY = Mathf.RoundToInt(pos.y);
                
                texture.SetPixel(posX, posY, Color.clear);
                
                texture.Apply();
                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }
    }
}
