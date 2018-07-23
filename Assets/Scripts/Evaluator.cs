using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evaluator : MonoBehaviour {

    int[][] allPixels;

    public RenderTexture vision;
    Texture2D converted;

    public Text disp;

    public float totalScore;

    public Light spotlight;

	// Use this for initialization
	void Start () {
        lightsOff();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void lightsOn()
    {


        spotlight.intensity = 1;
    }

    public void lightsOff()
    {
        spotlight.intensity = 0;
    }

    public void check()
    {
        lightsOn();
        getTex();
        Score();
        disp.text = totalScore.ToString();
        

    }

    void getTex()
    {
        RenderTexture.active = vision;
        converted = new Texture2D(vision.width, vision.height);

        converted.ReadPixels(new Rect(0,0, vision.width, vision.height), 0, 0);


        allPixels = new int[vision.height][];

        for(int row = 0; row < vision.height; row++)
        {
            allPixels[row] = new int[vision.width];
            

            for(int pix = 0; pix < vision.width; pix++)
            {
                Color c = converted.GetPixel(pix, row);
                
                allPixels[row][pix] = 0;

                //is it blue?
                if(c.b > .5f && c.g < .4f && c.r < .4f)
                {
                    allPixels[row][pix] = 2;
                    
                }
                else if(c.b < .5f && c.g < .5f && c.r < .5f)
                {
                    //it's gray
                    allPixels[row][pix] = 1;


                }
                

            }


        }

    }

    void Score()
    {
        float scored = 0;
        float total = vision.width * vision.height;

        for(int rowIndex = 0; rowIndex < vision.height; rowIndex++)
        {
            bool inBounds = false;
            bool prevBlue = false;
            for(int pixIndex = 0; pixIndex < vision.width; pixIndex++)
            {
                int pixColor = allPixels[rowIndex][pixIndex];
                
                switch(pixColor)
                {
                    case 0:
                        if(!inBounds)
                        {
                            scored += 1;
                            
                        }
                        if(prevBlue)
                        {
                            prevBlue = false;
                            
                        }
                        if(inBounds)
                        {
                            scored -= 1;

                        }
                        break;

                    case 1:
                        if (inBounds)
                        {
                            scored += 1;

                        }
                        else
                        {
                           // scored -= 1;


                        }
                        if(prevBlue)
                        {
                            prevBlue = false;
                            
                        }
                        break;

                    case 2:
                        scored += 1;
                        if(inBounds && !prevBlue)
                        {
                            inBounds = false;
                            
                        }
                        else if (!prevBlue)
                        {
                            inBounds = !inBounds;
                        }
                        if(!prevBlue)
                        {

                            prevBlue = true;

                        }
                        break;

                    default:
                        break;





                }



            }


        }

        totalScore = ((scored / total)*100);
        //totalScore += ((100 - totalScore) / 1.5f);

    }
}
