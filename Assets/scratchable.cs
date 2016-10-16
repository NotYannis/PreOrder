using UnityEngine;
using System.Collections;

public class scratchable : MonoBehaviour
{
    int square = 20;
    int radius = 40;
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

                // Draw a square with the mouse position in center
                /*
                int minX = posX - square;
                int maxX = posX + square;
                int minY = posY - square;
                int maxY = posY + square;

                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minY; j <= maxY; j++)
                    {
                        texture.SetPixel(i, j, Color.clear);
                    }
                }
                */

                // Draw a square but use setPixels method, which optimized setPixel method
                // Same optimization issue
                /*
                int mipCount = texture.mipmapCount;
                Color[] colors = new Color[square * square];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] =  Color.clear;
                }
                texture.SetPixels(posX, posY, square, square, colors);
                */

               // Draw a circle with the mouse position in center
               // We repeat the algorithm with a lesser radius each time
               
               int tempRadius = radius;
               while(tempRadius >= 0)
               {
                   // Andres circle algorithm
                   int x = 0;
                   int y = tempRadius;
                   int d = tempRadius - 1;

                   while (y >= x)
                   {
                       texture.SetPixel(posX + x, posY + y, Color.clear);
                       texture.SetPixel(posX + y, posY + x, Color.clear);
                       texture.SetPixel(posX - x, posY + y, Color.clear);
                       texture.SetPixel(posX - y, posY + x, Color.clear);
                       texture.SetPixel(posX + x, posY - y, Color.clear);
                       texture.SetPixel(posX + y, posY - x, Color.clear);
                       texture.SetPixel(posX - x, posY - y, Color.clear);
                       texture.SetPixel(posX - y, posY - x, Color.clear);

                       if (d >= 2 * x)
                       {
                           d -= 2 * x + 1;
                           x++;
                       }
                       else if (d < 2 * (tempRadius - y))
                       {
                           d += 2 * y - 1;
                           y--;
                       }
                       else
                       {
                           d += 2 * (y - x - 1);
                           y--;
                           x++;
                       }
                   }

                   tempRadius--;
               }
               

               texture.Apply();
                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }
    }
}
