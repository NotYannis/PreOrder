using UnityEngine;
using System.Collections;

public class scratchable : MonoBehaviour
{

    int pixelX = 0;
    int pixelY = 0;
    int iPixelX = -1;

    void Start()
    {
        // Texture2D texture = new Texture2D(1750, 800);
        Texture2D texture = new Texture2D(400, 400);
        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // Color color = ((x & y) != 0 ? Color.white : Color.gray);
                Color color = Color.gray;
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

                // DEBUG
                // Debug.Log(posX + " : " + posY);
                // Debug.Log("H : " + texture.height + "| W : " + texture.width);
                // GameObject quad = GameObject.Find("TestQuad");
                // pos.z = -5;
                // quad.transform.position = pos;
                // EOF DEBUG

                Debug.Log("Pos : " + posX + " : " + posY);
            }

            Debug.Log("Mouse : " + Input.mousePosition.x + " : " + Input.mousePosition.y);
        }
        
        /*
         * Slide effect from edges to the center of the quad 
        */
        /*
        Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;

        if (iPixelX == -1)
        {
            iPixelX = texture.width;
        }

        if (this.pixelX <= texture.width / 2)
        {
            for (int i = 0; i < texture.height; i++)
            {
                texture.SetPixel(this.pixelX, i, Color.clear);
            }
        }

        if (this.iPixelX >= texture.width / 2)
        {
            for (int i = 0; i < texture.height; i++)
            {
                texture.SetPixel(this.iPixelX, i, Color.clear);
            }
        }

        texture.Apply();
        this.pixelX++;
        this.iPixelX--;

        Debug.Log(Input.mousePosition);
        */

        /*
         * Diagonal auto 
        */
        /*
        Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
        texture.SetPixel(this.pixelX, this.pixelY, Color.clear);
        texture.Apply();
        GetComponent<Renderer>().material.mainTexture = texture;
        this.pixelX ++;
        this.pixelY ++;
        */

        /*
         * Erase few pixels manually to test erasing of neighbor pixels 
        */
        /*
        Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
        // texture.SetPixel(this.pixelX, this.pixelY, Color.clear);
        // texture.SetPixel(0, 0, Color.clear);
        // texture.SetPixel(0, 127, Color.clear);
        texture.Apply();
        GetComponent<Renderer>().material.mainTexture = texture;
        */       

        /*
         * Handle touch input on a mobile project 
        */
        /*
        for (var touch : Touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    // Create a particle if hit
                    Instantiate(particle, transform.position, transform.rotation);
                }
            }
        }
        */
    }
}
