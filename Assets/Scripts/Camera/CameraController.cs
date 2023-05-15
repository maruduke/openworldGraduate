using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    public Transform objectTofollow;
    public float followSpeed = 10f;
    public float sensitivity = 0.000000000000000000000001f;
    public float clampAngle = 70f;


    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormarized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    // Start is called before the first frame update
    
    ///wait


    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormarized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        touchScreenControl();

        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dirNormarized * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else {
            finalDistance = maxDistance;
        }

        Debug.Log(finalDistance);
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormarized * finalDistance, smoothness*Time.deltaTime);
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

                    Debug.Log("touch delta position:" + touch.deltaPosition);
   
                    rotX -= touchDeltaPosition.y * sensitivity * Time.deltaTime;
                    rotY += touchDeltaPosition.x * sensitivity * Time.deltaTime;

                    rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);


                    Debug.Log("rotX:" + rotX);
                    Debug.Log("rotY:" + rotY);

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
