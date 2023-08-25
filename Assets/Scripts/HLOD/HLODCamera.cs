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


#endregion




        
    




}
