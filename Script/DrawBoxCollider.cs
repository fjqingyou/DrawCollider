using UnityEngine;

namespace QY.Debug{
    
    [AddComponentMenu("DrawCollider/DrawBoxCollider")]
    public class DrawBoxCollider : DrawColliderGeneric<BoxCollider> {
        protected override void OnDrawCollider(){
            Vector3 position = targetCollider.transform.position;
            Vector3 colliderCenter = targetCollider.center;
            Vector3 colliderSize = targetCollider.size;
            Vector3 colliderSizeHalf = colliderSize / 2f;

            Vector3 upOffset = Vector3.up * colliderSizeHalf.y;
            Vector3 rightOffset = Vector3.left * colliderSizeHalf.x;
            Vector3 forwardOffset = Vector3.forward * colliderSizeHalf.z;

            Vector3 center = position + colliderCenter;
            Vector3 bottomCenter = center + -upOffset;


            Vector3 bottomLeftBack = bottomCenter + -rightOffset + -forwardOffset;
            Vector3 bottomRightback = bottomCenter + rightOffset + -forwardOffset;
            Vector3 bottomLeftForward = bottomCenter + -rightOffset + forwardOffset;
            Vector3 bottomRightForward = bottomCenter + rightOffset + forwardOffset;

            Gizmos.DrawLine(bottomLeftBack, bottomRightback);
            Gizmos.DrawLine(bottomRightback, bottomRightForward);
            Gizmos.DrawLine(bottomRightForward, bottomLeftForward);
            Gizmos.DrawLine(bottomLeftForward, bottomLeftBack);

            
            Vector3 dd = Vector3.up * colliderSize.y;;
            Vector3 topLeftBack = bottomLeftBack + dd;
            Vector3 topRightback = bottomRightback + dd;
            Vector3 topLeftForward = bottomLeftForward + dd;
            Vector3 topRightForward = bottomRightForward + dd;
            
            Gizmos.DrawLine(topLeftBack, topRightback);
            Gizmos.DrawLine(topRightback, topRightForward);
            Gizmos.DrawLine(topRightForward, topLeftForward);
            Gizmos.DrawLine(topLeftForward, topLeftBack);



            Gizmos.DrawLine(topLeftBack, bottomLeftBack);
            Gizmos.DrawLine(topRightback, bottomRightback);
            Gizmos.DrawLine(topRightForward, bottomRightForward);
            Gizmos.DrawLine(topLeftForward, bottomLeftForward);

        }
    }
}