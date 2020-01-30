﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollRectSnap : MonoBehaviour
{
    public RectTransform panel;                             // to hold the scroll panel;
    public Button[] aButtons;
    //20191126 추가사항
    public GameObject Sphere;
    public Texture[] Textures;
    public string[] Names;
    public new Text name;
    public new Camera camera;
    public int Temp;
    //여기까지
    public RectTransform center;                           // center to compare the distance for each button

    public float[] aDistances;                               // all button's distance to the center..
    private bool bDragging = false;                          // will be true,  while we drag the panel
    private int iBtnDistance;                                   // will hold the distance between the buttons
    public int iMinButtonNum;                               // to hold the number of the button, with smallest distance to center

    public GameObject NoticePopup;

    // Start is called before the first frame update
    void Start()
    {
        int iBtnLength = aButtons.Length;
        aDistances = new float[iBtnLength];
        // get distance between buttons
        iBtnDistance = (int)Mathf.Abs(aButtons[1].GetComponent<RectTransform>().anchoredPosition.x - aButtons[0].GetComponent<RectTransform>().anchoredPosition.x);        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < aButtons.Length; ++i)
        {
            aDistances[i] = Mathf.Abs(center.transform.position.x - aButtons[i].transform.position.x);            
        }

        float minDistance = Mathf.Min(aDistances);                              // Get the min Distance        
        for (int a = 0; a < aButtons.Length; ++a)
        {
            if(minDistance == aDistances[a])
            {
                /*
                //20191126 변경사항
                Temp = iMinButtonNum;
                iMinButtonNum = a;
                if (Temp != iMinButtonNum)
                {
                    if (camera != null)
                    {
                        camera.transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                }
                */
                if (iMinButtonNum < Textures.Length)
                {
                    Sphere.GetComponent<Renderer>().material.mainTexture = Textures[iMinButtonNum];
                }
                if (iMinButtonNum < Names.Length)
                    name.text = Names[iMinButtonNum];
            }
        }

        if(!bDragging)
        {
            LerpToButton(iMinButtonNum * -iBtnDistance);
        }
    }


    void LerpToButton(int position)
    {
        float fPosX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(fPosX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
    }

    //================================================ EVENT TRIGGER ==================================================//
    // Event Trigger 에서 call...
    public void StartDrag()
    {
        bDragging = true;
    }

    public void EndDrag()
    {
        bDragging = false;       
    }

    public void LeftButtonDown()
    {
        iMinButtonNum--;
        if (iMinButtonNum < 0)
        {
            iMinButtonNum = 0;
        }
    }

    public void RightButtonDown()
    {
        iMinButtonNum++;
        if (iMinButtonNum >= Names.Length - 1)
        {
            iMinButtonNum = Names.Length - 1;
        }
    }

    public void NoticeButtonEvent()
    {
        NoticePopup.SetActive(false);
    }

    // stage button....
    public void StageButtonEvent()
    {
        if (0 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("Stage1");
            }
        }
        else if (1 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("Stage2");
            }
        }
        else if (2 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                //SceneManager.LoadScene("GPS_Scene");
            }
        }
        else if (3 == iMinButtonNum)
        {
            // stage 1 load...
            if (Quiz_XML_Reader.Instance.readCompleted == true && XML_Reader.Instance.readCompleted == true)
            {
                SceneManager.LoadScene("Record");
            }
        }
    }

    public void HomeButtonEvent()
    {
        SceneManager.LoadScene("Start");
    }

    public void MapButtonEvent()
    {
        if (0 == iMinButtonNum)
        {
            //36.895005, 126.206617            
            string strUrl = "https://www.google.co.kr/maps/place/%EB%8A%A5%ED%97%88%EB%8C%80%EC%A7%80/@37.4228118,126.6432839,20.25z/data=!4m8!1m2!2m1!1z64ql7ZeI64yA!3m4!1s0x357b7828c0d706cd:0x9be3b8eed7d3fec0!8m2!3d37.422852!4d126.643284?hl=ko";
            Application.OpenURL(strUrl);
        }
        else if (1 == iMinButtonNum)
        {
            string strUrl = "https://www.google.co.kr/maps/place/G%ED%83%80%EC%9B%8C/@37.3961159,126.6321385,17z/data=!3m1!4b1!4m5!3m4!1s0x357b77b9afc9fc5b:0x20c53510ab4a0319!8m2!3d37.3961117!4d126.6343272?hl=ko";
            Application.OpenURL(strUrl);
        }
        else if (2 == iMinButtonNum)
        {            
            //string strUrl = "https://www.google.com/maps/place/36.835972,126.195911";
            //Application.OpenURL(strUrl);
        }
    }




}
