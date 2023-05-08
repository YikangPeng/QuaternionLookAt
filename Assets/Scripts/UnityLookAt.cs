using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLookAt : MonoBehaviour
{

    [Header("目标物体")]
    public Transform Target;

    [Header("需要旋转的骨骼")]
    public Transform HeadJoint;

    [Header("参考系")]
    public Transform Direction;

    //记录需要修正的旋转量
    private Quaternion rotationFix;

    // Start is called before the first frame update
    void Start()
    {
        rotationFix = Quaternion.Inverse(Direction.transform.rotation) * HeadJoint.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //旋转骨骼看向物体
        HeadJoint.transform.LookAt(Target);
        //修正旋转
        HeadJoint.transform.rotation = HeadJoint.transform.rotation * rotationFix;

    }
}
