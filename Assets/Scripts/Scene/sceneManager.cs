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
    private Dictionary<string, SceneInstance> sceneInstances = new Dictionary<string, SceneInstance>();


#endregion


#region lifecycle

    void Awake()
    {
        sceneObjectManager = SceneObjectManager.Instance;
        Init(playerX, playerZ);
        StartCoroutine(updateScene());
    }

    // Update is called once per frame
    void Update()
    {
                   
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
                AddressableSceneLoad(x + i , z + j);
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

                // x방향으로 +,- 방향으로 이동
                if(xcheck != 0) {

                    xcheck = xcheck > 0 ? 1 : -1;
                    for(int i = 0 ; i<3; i++) {
                        AddressableSceneLoad(playerX + (terrainRange*xcheck), playerZ + terrainRange*vec[i]);
                        AddressableSceneUnload(playerX - (terrainRange*xcheck), playerZ + terrainRange*vec[i]);
                    }
                }

                // z방향으로 +,- 방향으로 이동
                else if(zcheck != 0) {
                    zcheck = zcheck > 0 ? 1 : -1;
                    for(int i = 0 ; i<3; i++) {
                        AddressableSceneLoad(playerX + terrainRange*vec[i], playerZ + (terrainRange * zcheck));
                        AddressableSceneUnload(playerX + terrainRange*vec[i], playerZ - (terrainRange * zcheck));
                    }
                }


                playerX = x;
                playerZ = z;


                yield return new WaitForSecondsRealtime( 0.1f );
            }
        }
    }   

#endregion


#region assist method
    // addressable asset system scene loading
    private void AddressableSceneLoad(int x, int z)
    {

        string sceneName = sceneNameCreate(x , z);

        Debug.Log(sceneName);
        if( sceneInstances.ContainsKey(sceneName) ) {
            return;
        }
        
 
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).Completed += 
        (handle) => {
        
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                sceneInstances.Add(sceneName, handle.Result);
            }

            else {
                //AddressablesImpl LogException() 수정
                Debug.LogWarning(sceneName + " is not founded");
            }
        
        };

    
    
    } 


    private void AddressableSceneUnload(int x, int z)
    {
        string sceneName = sceneNameCreate(x , z);
        SceneInstance sceneInstance;
    
        if(sceneInstances.TryGetValue(sceneName, out sceneInstance)) {
            
            Addressables.UnloadSceneAsync(sceneInstance).Completed += (handle) => {
                Resources.UnloadUnusedAssets();
                sceneInstances.Remove(sceneName);

            };

        }

    }


    private string sceneNameCreate(int x, int z)
    {
        return string.Format($"Assets/Scenes/map{x}_{z}.unity");
    }

#endregion



}
