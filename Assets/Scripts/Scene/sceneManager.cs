using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Exceptions;

using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using System;

using MySystem.SceneControl;

public class sceneManager : MonoBehaviour
{

#region variables

    SceneObjectManager sceneObjectManager;

    Vector3 pos;    
    // 터레인 크기
    int terrainRange = 100;

    // 현재 플레이어 위치
    int playerX = 0; 
    int playerZ = 0;
    
    // 메모리에 올라간 씬 리스트
    // private Dictionary<string, SceneInstance> sceneInstances = new Dictionary<string, SceneInstance>();
    private Dictionary<string, Scene> sceneInstances = new Dictionary<string, Scene>();
    
    [SerializeField] 
    private GameObject LoadObjects; 


#endregion


#region lifecycle

    void Awake()
    {
        sceneObjectManager = SceneObjectManager.Instance;
        Init(playerX, playerZ);
        StartCoroutine(updateScene());
    }

#endregion




#region CoreMethod

    // 처음 실행시 scene loading 
    private void Init(int x,int z)
    {
        for(int i= -terrainRange ; i <= terrainRange; i += terrainRange )
        {
            for(int j= -terrainRange; j <= terrainRange; j += terrainRange )
            {
                sceneLoad(x + i , z + j);
            }
        }
    }

    // 실시간 scene 업데이트
    private IEnumerator updateScene()
    {
        while(true)
        {

            pos = this.gameObject.transform.position;

            sceneObjectManager.update((int) pos.x, (int)pos.z); 
            int x = ( (int) pos.x / terrainRange) * terrainRange;
            int z = ( (int) pos.z / terrainRange) * terrainRange;


            if(playerX == x && playerZ == z)
            {
                yield return null;
            }

            else {
                int xcheck = x - playerX;
                int zcheck = z - playerZ;

                int[] vec = new int[3]{-1,0,1};

                Debug.Log("______________________________________________________________");
                // x방향으로 +,- 방향으로 이동
                if(xcheck != 0) {

                    xcheck = xcheck > 0 ? 1 : -1;
                    Debug.Log($"playerX: {playerX}");
                    for(int i = 0 ; i<3; i++) {
                        sceneLoad(x + (terrainRange*xcheck), playerZ + terrainRange*vec[i]);
                        sceneUnload(playerX - (terrainRange*xcheck), playerZ + terrainRange*vec[i]);
                    }
                }

                // z방향으로 +,- 방향으로 이동
                if(zcheck != 0) {
                    zcheck = zcheck > 0 ? 1 : -1;
                    for(int i = 0 ; i<3; i++) {
                        sceneLoad(playerX + terrainRange*vec[i], z + (terrainRange * zcheck));
                        sceneUnload(playerX + terrainRange*vec[i], playerZ - (terrainRange * zcheck));
                    }
                }
                Debug.Log("______________________________________________________________");

                playerX = x;
                playerZ = z;


                yield return new WaitForSecondsRealtime( 0.1f );
            }
        }
    }   

#endregion


#region assist method
    // addressable asset system scene loading
    private void sceneLoad(int x, int z)
    {

        string sceneName = sceneNameCreate(x , z);

        if( sceneInstances.ContainsKey(sceneName) )
            return;
    
        
        Debug.Log("scene Loading: " + sceneName);
        Scene newScene = SceneManager.CreateScene(sceneName);
                
        sceneInstances.Add(sceneName, newScene);

                // 프리팹 인스턴스 생성
        GameObject instantiatedPrefab = Instantiate(LoadObjects);
        instantiatedPrefab.transform.position = new Vector3(x,0,z);

        // 생성된 프리팹 인스턴스를 새로운 장면에 추가
        SceneManager.MoveGameObjectToScene(instantiatedPrefab, newScene);


    } 


    private void sceneUnload(int x, int z)
    {
        string sceneName = sceneNameCreate(x , z);

        Scene sceneInstance;

        if(sceneInstances.TryGetValue(sceneName, out sceneInstance)) {
            var loader = SceneManager.UnloadSceneAsync(sceneInstance);

            loader.completed += (handle) => {

                if(handle.isDone) {
                    Debug.Log("scene unload: " + sceneName);
                    sceneInstances.Remove(sceneName);
                    System.GC.Collect();
                }
            };

        }

    }


    private string sceneNameCreate(int x, int z)
    {
        return string.Format($"Map{x}_{z}");
    }

#endregion



}
