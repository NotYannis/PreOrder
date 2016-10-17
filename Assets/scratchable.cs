using UnityEngine;
using System.Collections;

public class scratchable : MonoBehaviour
{

	public int textHeight;
    int radius = 40;
    int nTouch = 0;
    
	Touch inputTouch = new Touch();


    Vector3 lastMousePos;
    Vector3 lastTouchPos;

    void Start()
    {
        Texture2D texture = new Texture2D(1750, textHeight);
        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = ((x & y) != 0 ? Color.white : Color.gray);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
	{
		nTouch = 0;
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{

			}
			inputTouch = Input.touches[0];
			nTouch++;
		}
		mouseInput();
        lastMousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        if(nTouch > 1){
        	lastTouchPos = Camera.main.WorldToScreenPoint(inputTouch.position);
        }
    }

	/*
    * Draw circle on player input 
    */
	void mouseInput(){
		
		if(Input.GetMouseButton(0))
		{
			Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			int posX = Mathf.RoundToInt(pos.x);
			int posY = Mathf.RoundToInt(pos.y);

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
		/*
		if(Input.GetMouseButton (0)) {
			Vector3 mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);

			Vector3 move = mousePos - lastMousePos;
			Camera.main.transform.position += new Vector3(move.x, move.y, 0.0f);
		}*/
	}

	/*
    * Mobile input 
    */
	void touchInput(){
		/*
         * Move the screen when there is more than one touch
        */
		if(nTouch > 1)
		{
			Vector3 touchPos = Camera.main.WorldToScreenPoint(inputTouch.position);

			Vector3 move = touchPos - lastTouchPos;
			Camera.main.transform.position += new Vector3(move.x, move.y, 0.0f);
		}
		else if (nTouch == 1)
		{
			Ray ray = Camera.main.ScreenPointToRay(inputTouch.position);

			if (Physics.Raycast(ray))
			{
				Texture2D texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
				Vector3 pos = Camera.main.ScreenToWorldPoint(inputTouch.position);

				int posX = Mathf.RoundToInt(pos.x);
				int posY = Mathf.RoundToInt(pos.y);


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
