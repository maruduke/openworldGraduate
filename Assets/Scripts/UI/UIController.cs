using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{


#region variables

    private float rotX;
    private float rotY;
    public float sensitivity = 0.000000000000000000000001f;
    public float clampAngle = 70f;

#endregion

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;
    }
    // Update is called once per frame
    void Update()
    {
        touchScreenControl();
    }

        // 화면 터치를 통한 화면 회전 및 줌 인 줌 아웃 구현
    private void touchScreenControl()
    {

        // 손가락 하나만 인식
        if(Input.touchCount > 0) {
            
            int touchCnt = 0;
            int getTouch = 0;
            Touch touch;

            for(int i=0; i<Input.touchCount;i++) {
                
                //UI 터치 확인
                if(EventSystem.current.IsPointerOverGameObject(i) == false) {                 
                    touchCnt += 1;
                    getTouch = i;
                }
            }

            // 화면 전환 
            if(touchCnt == 1) {
             
                touch = Input.GetTouch(getTouch);
                


                if(touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = touch.deltaPosition;

   
                    rotX -= touchDeltaPosition.y * sensitivity * Time.deltaTime;
                    rotY += touchDeltaPosition.x * sensitivity * Time.deltaTime;

                    rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

                    Quaternion rot = Quaternion.Euler(rotX, rotY, 0);

                    transform.rotation = rot;
                }

            }

            // 줌인, 줌 아웃 체크
            else if(touchCnt == 2){

            }


        }

        
        // 둘 이상 인식 zoom in, zoom out 구현
    }

    
}
