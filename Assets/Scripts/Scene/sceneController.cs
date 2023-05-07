using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneController : MonoBehaviour
{

    Vector3 pos;

    // 메모리에 올라간 씬 리스트
    List<string> loadedSceneList = new List<string>();

    // 터레인 크기
    int terrainRange = 10;

    // 현재 위치
    int playerX = 0; 
    int playerZ = 0;

    void Awake()
    {
        Init(playerX, playerZ);
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.gameObject.transform.position; 
        int x = ( (int) pos.x / terrainRange) * terrainRange;
        int z = ( (int) pos.z / terrainRange) * terrainRange;


        if(playerX != x || playerZ != z)
        {
            StartCoroutine(updateScene(x,z,playerX,playerZ));
            playerX = x;
            playerZ = z;
        }
        
               
    }




    // Scene 비동기 로딩
    IEnumerator LoadScene(int x, int z)
    {
        string sceneName = string.Format($"Terrain{x}_{z}");

        // 존재하지 않는 씬
        if(!Application.CanStreamedLevelBeLoaded(sceneName)) {
            yield return null;
        }

        // 이미 로딩된 씬
        else if(loadedSceneList.Contains(sceneName)) {
            yield return null;
        }

        else {
            Debug.Log(sceneName + "is Loading 요청됨");
            loadedSceneList.Add(sceneName);
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return null;
        }      
    }



    IEnumerator unloadScene(int x, int z)
    {
        string sceneName = string.Format($"Terrain{x}_{z}");
        
        if(!loadedSceneList.Contains(sceneName)) {
            yield return null;
        }

        else {
            SceneManager.UnloadSceneAsync(sceneName);
            loadedSceneList.Remove(sceneName);
            Debug.Log(sceneName +" unload");
            // 위치 변경에 따른 scene 제거 구현
        }
        yield return null;
    }


    IEnumerator updateScene(int x, int z, int playerX, int playerZ)
    {
        int xcheck = x - playerX;
        int zcheck = z - playerZ;

        int[] vec = new int[3]{-1,0,1};
        // x가 + 방향으로 이동
        if(xcheck > 0) {
            for(int i = 0 ; i<3; i++) {
                
                StartCoroutine(LoadScene(playerX+terrainRange, playerZ + terrainRange*vec[i]));
                StartCoroutine(unloadScene(playerX-terrainRange, playerZ + terrainRange*vec[i]));

            }
        }

        // x가 -방향으로 이동
        else if(xcheck < 0) {
            
            for(int i = 0 ; i<3; i++) {
                
                StartCoroutine(LoadScene(playerX - terrainRange, playerZ + terrainRange*vec[i]));
                StartCoroutine(unloadScene(playerX + terrainRange, playerZ + terrainRange*vec[i]));
                
            }
        }

        // z가 + 방향으로 이동
        else if(zcheck > 0) {
            for(int i = 0 ; i<3; i++) {
                
                StartCoroutine(LoadScene(playerX + terrainRange*vec[i], playerZ + terrainRange));
                StartCoroutine(unloadScene(playerX + terrainRange*vec[i], playerZ - terrainRange));
                
            }
        }

        // z가 -방향으로 이동
        else {
            for(int i = 0 ; i<3; i++) {
                
                StartCoroutine(LoadScene(playerX + terrainRange*vec[i], playerZ - terrainRange));
                StartCoroutine(unloadScene(playerX + terrainRange*vec[i], playerZ + terrainRange));
                
            }
        }
        yield return null;
    }


    void Init(int x,int z)
    {
        for(int i=-10 ; i <= 30; i+=10)
        {
            for(int j=-10; j<=30; j+=10)
            {
                StartCoroutine( LoadScene(x+i,z+j));
            }
        }
    }
}
