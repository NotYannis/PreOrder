using UnityEngine;
using System.Collections;

public class scratchable : MonoBehaviour
{

    int pixelX = 0;
    int pixelY = 0;
    int iPixelX = -1;

    void Start()
    {
        Texture2D texture = new Texture2D(128, 128);
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
         * What does that even do ?
        */
        /*
        for (int x = Mathf.Max(0, pixelX - radius); x < Mathf.Min(texture.width, pixelX + radius); x++)
        {
            for (int y = Mathf.Max(0, pixelY - radius); y < Mathf.Min(texture.height, pixelY + radius); y++)
            {
                float discount = (new Vector2(pixelX - x, pixelY - y)).magnitude / (float)radius;
                if (discount < 1f)
                {
                    int index = x + y * texture.width;
                    backPixels[index] = Color.Lerp(backPixels[index], frontPixels[index], Time.deltaTime * strength * (1f - discount));
                }
            }
        }
        texture.SetPixels32(backPixels);
        texture.Apply();
        */

        // Debug.Log(Input.touchSupported);

        /*
         *  
        */
        /*
        for (int i = 0; i < Input.touches.Length; i++)
        {
            Touch touch = Input.touches[i];
            if(touch.phase == TouchPhase.Began)
            {
                // Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, Mathf.Infinity, LayerMask.NameToLayer("ScratchableSurface")))
                {
                    Debug.Log("TOUCHE");
                }
            }
        }
        */

        /*
         * Move the mouse to scratch the screen 
        */
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray))
            {
                // Texture2D texture = new Texture2D(128, 128);
                // GetComponent<Renderer>().material.mainTexture = texture;
                Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                texture.SetPixel((int)pos.x, (int)pos.y, Color.clear);

                texture.Apply();
                GetComponent<Renderer>().material.mainTexture = texture;

                /* DEBUG */
                Debug.Log(pos);
                GameObject quad = GameObject.Find("TestQuad");
                pos.z = -5;
                quad.transform.position = pos;
                /* EOF DEBUG */
            }
        }

        Debug.Log("Mouse : " + Input.mousePosition.x + " : " + Input.mousePosition.y);

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
