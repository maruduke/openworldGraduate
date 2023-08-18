using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;








using System;
using MySystem.SceneControl;

namespace MySystem.SceneControl{ 

    public class SceneController : MonoBehaviour
    {

        /*
            object 구조 (순서변경 X)
            Scene
                Map000_000
                    -> Map (Terrain 값 저장) 
                    -> HLOD (HLOD 오브젝트 저장)

            지형 오브젝트 -> Terrain(포함)
            HLOD 오브젝트 -> Terrain(미포함)
        */


        // 어드레서블의 Label을 얻어올 수 있는 필드.

#region variables

        public AssetLabelReference assetLabel;
    
        private IList<IResourceLocation> _locations;
        // 생성된 게임오브젝트를 Destroy하기 위해 참조값을 캐싱한다.

        
        private List<GameObject> _terrainObjects = new List<GameObject>();
        private List<GameObject> _hlodObjects = new List<GameObject>();

        public string sceneName;

        [SerializeField]
        public int terrainRange;

        private State state;

        private Vector3 pos;


        /*
        prepare: 초기 단계
        Loading: 데이터 로드, 언로드 진행중
        Unload: terrain, hlod not loaded
        Unloadhlod: hlod not loaded
        Loadhlod: all object load
        */
        enum State {
            prepare,
            Loading,
            Unload,
            Unloadhlod,
            Loadhlod,
        }

#endregion

        void Start() {   

            // 값 초기화
            state = State.prepare;
            sceneName = this.gameObject.scene.name;
            terrainRange = 500;

            var postmp = this.gameObject.transform.position;            
            pos = new Vector3(postmp.x + terrainRange, 0f , postmp.z + terrainRange);

            //hlod object loading distance
            terrainRange = 2 * (terrainRange * terrainRange); 

            StartCoroutine(GetLocation());
            StartCoroutine(TerrainInstantiateV2());
            Register();

        }

        private void OnDestroy() {
            UnRegister();
        }

        public void update(int x, int z) {
            

            int xcheck = x - (int) pos.x;
            int zcheck = z - (int) pos.z;
            int vec = (xcheck*xcheck)  + (zcheck*zcheck);

            // Debug.Log("vec:" + vec + "range: " + terrainRange);
            if(vec < terrainRange && state == State.Unloadhlod) {
                //hlod object create
                StartCoroutine(HLODInstantiateV2());

            }

            else if (vec >= terrainRange  && state == State.Loadhlod) {
                //hlod object release
                StartCoroutine(HLODReleaseV2());
            }
            
            
        }



        public void Register() {
            SceneObjectManager.Instance.Register(this);
        }

        public void UnRegister() {
            SceneObjectManager.Instance.UnRegister(this);
        }






#region Test



        IEnumerator GetLocation() {

            if(state != State.prepare)
                yield break;
                        
            state = State.Loading;


            if(_locations == null) {            
                Addressables.LoadResourceLocationsAsync(assetLabel.labelString).Completed +=
                    (handle) =>
                    {
                        if(handle.Status == AsyncOperationStatus.Succeeded) {
                            _locations = handle.Result;
                        }
                        else
                            Debug.Log("LoadResourceLocationAsync error");

                    };
            }


            yield return new WaitWhile( () => _locations == null);

            Debug.Log(_locations.Count);
            state = State.Unload;

            yield break;

        }

        IEnumerator  TerrainInstantiateV2()
        {
            if(state == State.prepare) {
                StartCoroutine(GetLocation());
            }

            yield return new WaitUntil( () => state != State.Unload);

            state = State.Loading;

            int i = 0;
            foreach( IResourceLocation loc in _locations )
            {
                

                if(loc.PrimaryKey.Contains("Terrain")) {

                    Addressables.InstantiateAsync(loc).Completed +=
                        (handle) =>
                        {
                            handle.Result.transform.localPosition = handle.Result.transform.position;
                            handle.Result.transform.parent = this.transform.GetChild(0);
                            _terrainObjects.Add(handle.Result);
                            i++;
                        };
                }

                else {
                    i ++;
                }

            }

            yield return new WaitWhile( () => i != _locations.Count);
            state = State.Unloadhlod;
            yield break;
        }

        IEnumerator HLODInstantiateV2()
        {
            yield return new WaitWhile( () => state != State.Unloadhlod);
            
            if(state == State.Loadhlod || state == State.Loading)
                yield break;


            state = State.Loading;
            int i = 0;
            foreach(IResourceLocation loc in _locations)
            {
                if(!_locations[i].PrimaryKey.Contains("Terrain")) {
                    Addressables.InstantiateAsync(_locations[i]).Completed +=
                        (handle) =>
                        {
                            handle.Result.transform.localPosition = handle.Result.transform.position;
                            handle.Result.transform.parent = this.transform.GetChild(1);
                            _hlodObjects.Add(handle.Result);
                            i++;
                        };
                }

                else {
                    i++;
                }

            }

            yield return new WaitWhile( () => i != _locations.Count);
            state = State.Loadhlod;
            yield break;
        }

        IEnumerator HLODReleaseV2() {

            yield return new WaitWhile( () => state != State.Loadhlod);


            if (state != State.Loadhlod || state == State.Loading)
                yield break;

            state = State.Loading;

            for(int i = 0; i < _hlodObjects.Count; i++ )
            {
                Addressables.ReleaseInstance(_hlodObjects[i]);
            }

            state = State.Unloadhlod;
            _hlodObjects.Clear();

            yield break;


        }

#endregion

    }
}
