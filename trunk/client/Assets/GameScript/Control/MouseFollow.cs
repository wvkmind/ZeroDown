using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    Vector3 screenPosition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionInWorld;
    Vector3 OldPos;
    // Start is called before the first frame update
    public Cinemachine.CinemachineVirtualCamera cv_camera;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Jump")>0)
        {
            var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            cv_camera.m_Follow.position =  new Vector3(pos.x,pos.y,transform.position.z);
            cv_camera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight=0.1f;
            cv_camera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth=0.1f;
            return; 
        }
        if (Input.GetAxis("Mouse ScrollWheel") <0)
        {
           if(cv_camera.State.Lens.OrthographicSize <=10)
           {
               cv_camera.m_Lens.OrthographicSize= cv_camera.State.Lens.OrthographicSize+0.5f;
               return ;
           }
        }
	    if (Input.GetAxis("Mouse ScrollWheel") > 0)
	    {
	      if(cv_camera.State.Lens.OrthographicSize >2)
          {
              cv_camera.m_Lens.OrthographicSize= cv_camera.State.Lens.OrthographicSize-0.5f;
              return; 
          }
	    }
        mousePositionOnScreen = Input.mousePosition;
        
        if(mousePositionOnScreen.x<0)
        {
            mousePositionOnScreen.x = 0;
        }else if(mousePositionOnScreen.x>Screen.width)
        {
            mousePositionOnScreen.x = Screen.width;
        }

        if(mousePositionOnScreen.y<0)
        {
            mousePositionOnScreen.y = 0;
        }else if(mousePositionOnScreen.y>Screen.height)
        {
            mousePositionOnScreen.y = Screen.height;
        }

        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        mousePositionOnScreen.z = screenPosition.z;

        if(((mousePositionOnScreen.x>=Screen.width-5||mousePositionOnScreen.x<=5||mousePositionOnScreen.y>=Screen.height-5||mousePositionOnScreen.y<=5)))
        {
            transform.position = new Vector3(mousePositionInWorld.x,mousePositionInWorld.y,transform.position.z);

            cv_camera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight=0.98f;
            cv_camera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth=0.98f;

            cv_camera.m_Follow=transform;
            OldPos = mousePositionOnScreen;
        }    
    }
}
