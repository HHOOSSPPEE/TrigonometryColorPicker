/* mhosster - Merry Hospelhorn
 * 2021 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHover : MonoBehaviour
{
    public Camera cam;
    public GameObject background;

    Color currentColor;

    float radius;
    float theta;


    void Start()
    {
        cam.clearFlags = CameraClearFlags.SolidColor;

        gameObject.transform.position = new Vector3(0f, 0f, 0f); //zero the object in the center
    }

    void Update()
    {

 
        RaycastHit hit;

        //if (Input.GetMouseButtonDown(0)) //use if want to click..
        //{
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50))
            {
                //find the radius
                float hyp = Mathf.Pow(hit.point.x, 2) + Mathf.Pow(hit.point.y, 2);
                radius = Mathf.Sqrt(hyp);

                //solve for theta
                float z = (hit.point.x / radius);
                theta = Mathf.Acos(z);

            //if below 
                if (hit.point.y < gameObject.transform.position.y)
                {
                    theta -= Mathf.PI;
                    theta = -theta;
                    theta += Mathf.PI;

                }   
                //Debug.Log("theta: " + theta);
                HSVMe(theta, radius);
            }
        //}
      
    }

    void HSVMe(float theta, float radius)
    {
        //determine what color that is - saturation = radius, theta = hue 
        radius = radius * 0.1f;//adjust radius to work in a 10 x 10 screen
        theta = (theta * 1.59f) * 0.1f;

        currentColor = Color.HSVToRGB(theta, radius, 1.0f);
        
        //set currentColor to that
        var bgRenderer = background.GetComponent<Renderer>();//change background color
        bgRenderer.material.SetColor("_Color", currentColor);
        var changeRenderer = gameObject.GetComponent<Renderer>();//change changeTarget's material color
        changeRenderer.material.SetColor("_Color", currentColor);
    }

}
