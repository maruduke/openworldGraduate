using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceLocations;

public class TerrainController : MonoBehaviour
{

    // 어드레서블의 Label을 얻어올 수 있는 필드
    public AssetLabelReference assetLabel;
    private IList<IResourceLocation> _locations;
    private List<GameObject> _gameObjects = new List<GameObject>();


    public GameObject mapTest;

    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        Scene Test = LoadMapAsset("Test");   
        assetLabel.labelString = "object";


        GetLocation();

        
        var load = Addressables.LoadAssetAsync<GameObject>("Assets/Terrain/Prefab/Tree.prefab");
        
        load.Completed += (op) =>
        {
            GameObject prefab = op.Result;
            // 로드된 프리팹에 대한 작업 수행
            GameObject newObj = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(newObj, Test);
        };

        Addressables.Release(load);


    }

    

    private Scene LoadMapAsset(string newSceneName)
    {
        Scene newScene = SceneManager.CreateScene(newSceneName);

        var load = Addressables.LoadAssetAsync<GameObject>("Assets/Terrain/Prefab/" + newSceneName +".prefab");
        
        load.Completed += (op) =>
        {
            GameObject prefab = op.Result;
            _gameObjects.Add(prefab);
            // 로드된 프리팹에 대한 작업 수행
            GameObject newObj = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(newObj, newScene);

        };

        Addressables.Release(load);

        return newScene;
    }



    private void GetLocation()
    {
        // 빌드 타켓의 경로를 가져온다.
        // 경로이기 때문에 메모리에 에셋이 로드되진 않는다.

        Addressables.LoadResourceLocationsAsync(assetLabel.labelString).Completed += 
            (handle) => {

                _locations = handle.Result;

            };

    }


}
