using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    float bufferRadius;
    float viewRadius;
    float atkRadius;
    [Range(0, 360)]
    float atkAngle;
    [Range(0, 360)]
    float rotAngle;
    Vector3 initialViewAngleA;
    Vector3 initialViewAngleB;

    public float GetBufferRadius() { return bufferRadius; }
    public float GetViewRadius() { return viewRadius; }
    public float GetAtkRadius() { return atkRadius; }
    public float GetAtkAngle() { return atkAngle; }
    public float GetRotAngle() { return rotAngle; }
    public Vector3 GetViewAngleA() { return initialViewAngleA; }
    public Vector3 GetViewAngleB() { return initialViewAngleB; }


    public void Start()
    {
        bufferRadius = this.GetComponent<ShooterEnemy>().GetBufferDist();
        viewRadius = this.GetComponent<ShooterEnemy>().GetDetctRadius();
        atkRadius = this.GetComponent<ShooterEnemy>().GetAtkDist();
        atkAngle = this.GetComponent<ShooterEnemy>().GetAtkAngle();
        rotAngle = this.GetComponent<ShooterEnemy>().GetRotAngle();
        initialViewAngleA = DirFromAngle(-rotAngle / 2, false);
        initialViewAngleB = DirFromAngle(rotAngle / 2, false);
    }

    public void Update()
    {
        viewRadius = this.GetComponent<ShooterEnemy>().GetDetctRadius();
        atkRadius = this.GetComponent<ShooterEnemy>().GetAtkDist();
        atkAngle = this.GetComponent<ShooterEnemy>().GetAtkAngle();
    }


    public bool FindVisibleTargets()
    {
        bool foundTarget = false;
        Collider[] TargetsInViewRad = Physics.OverlapSphere(this.transform.position, atkRadius);

        for(int i = 0; i < TargetsInViewRad.Length; i++)
        {
            if(TargetsInViewRad[i].tag == "Player")
            {
                //Debug.Log("Player in Sphere");

                Transform target = TargetsInViewRad[i].transform;
                Vector3 dirToTarget = (target.position - this.transform.position).normalized;
                if (Vector3.Angle(this.transform.forward, dirToTarget) < atkAngle / 2)
                {
                    //Debug.Log("Player in Cone");

                    float distToTarget = Vector3.Distance(this.transform.position, target.position);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(this.transform.position, dirToTarget, out hitInfo, distToTarget))
                    {
                        if (hitInfo.collider.tag != "Wall")
                        {
                            foundTarget = true;
                        }                       

                        break;
                    }
                }
            }
        }

        return foundTarget;
    }

    public Vector3 DirFromAngle(float angleInDeg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDeg += this.transform.rotation.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }

}
