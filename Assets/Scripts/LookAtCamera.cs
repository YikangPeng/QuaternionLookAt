using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    [Header("目标物体")]
    public Transform Target;

    [Header("需要旋转的骨骼")]
    public Transform HeadJoint;

    [Header("参考系")]
    public Transform Direction;

    [Header("转头速度")]
    public float SmootSpeed = 0.015f;

    [Header("偏头角度")]
    public float HeadAngle = 30.0f;

    //记录需要修正的旋转量
    private Quaternion rotationFix;    

    //记录上一帧结果
    private Quaternion lastFrameResult;

    // Start is called before the first frame update
    void Start()
    {
        rotationFix = Quaternion.Inverse(Direction.transform.rotation) * HeadJoint.transform.rotation;
        ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
        
        Quaternion OriginQuaternion = HeadJoint.transform.localRotation;

        //望向目标的向量
        Vector3 Lookdir = (Target.position - HeadJoint.position).normalized;

        float RotateAngle =Vector3.Dot(Lookdir, Direction.forward);

        

        //计算向上的向量
        Vector3 Directionright = Vector3.Cross(Lookdir, Vector3.up).normalized;
        Vector3 Directionup = Vector3.Cross(Directionright, Lookdir).normalized;

        //构造望向目标的向量
        Quaternion DirectionQ = Quaternion.LookRotation(Lookdir, Directionup);

        //计算偏头的角度
        float BiasAngle = Direction.InverseTransformVector(Lookdir).x * HeadAngle;
        DirectionQ = Quaternion.AngleAxis(BiasAngle, DirectionQ * Vector3.forward) * DirectionQ;


        HeadJoint.transform.rotation = DirectionQ * rotationFix;

        //角度判断，平滑过度
        if (RotateAngle > 0.7f)
        {
            HeadJoint.transform.localRotation = Quaternion.Slerp(lastFrameResult, HeadJoint.transform.localRotation, SmootSpeed);
        }
        else
        {
            HeadJoint.transform.localRotation = Quaternion.Slerp(lastFrameResult, OriginQuaternion, SmootSpeed);
        }
        


        lastFrameResult = HeadJoint.transform.localRotation;

    }
}
