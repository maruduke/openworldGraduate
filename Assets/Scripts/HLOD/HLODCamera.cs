using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.HLODSystem.Streaming;
using Unity.HLODSystem;

public class HLODCamera : MonoBehaviour
{

#region variables

    public HLODManager hlodManager;

    private  int HLODlayerMask;
    private int deactivateHLODlayerMask;

    private Transform tr;

    [SerializeField]
    public float radius = 5f;


#endregion

    // private void OnDrawGizmosSelected() {

    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(this.transform.position, radius);        
    // }
    



#region lifecycle


    void Start()
    {
        HLODlayerMask = LayerMask.NameToLayer("HLOD");
        deactivateHLODlayerMask = LayerMask.NameToLayer("DeactivateHLOD");

        tr = GetComponent<Transform>();
        hlodManager = HLODManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        detectDeactivateHLOD();
    }

#endregion




    void detectDeactivateHLOD() {
        
        
        // Collider[] colls = Physics.OverlapSphere(tr.position, radius, 1 << deactivateHLODlayerMask );

        Collider[] testResult = new Collider[3];
        Collider[] results = new Collider[3];


        Physics.OverlapSphereNonAlloc(tr.position, radius, results, 1 << deactivateHLODlayerMask);


        // foreach(Collider result in results)
        // {
        //     if(result != null)
        //     {
        //         GameObject obj = result.transform.root.gameObject;
        //         HLODControllerBase controller = obj.GetComponent<HLODControllerBase>();
        //         hlodManager.Register(controller);
        //     }
        // }
        
        
        // foreach(Collider coll in colls) 
        // {
        //     GameObject obj = coll.transform.root.gameObject;
            
        //     obj.GetComponent<AddressableHLODController>().selfChangeLayers(HLODlayerMask);

            // if(currentHLODObjs.Contains(obj)) {
            //     continue;
            // }
            // else {
            //     currentHLODObjs.Add(obj);
            //     obj.GetComponent<AddressableHLODController>().LoadLowObjectAll(null);
            //     Debug.Log(obj.name +" is Loaded");
            // }

    }
        
    




}
