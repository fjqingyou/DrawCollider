using UnityEngine;


namespace QY.Debug{
    [AddComponentMenu("Draw Collider/Draw Sphere Collider")]
    public class DrawSphereCollider : DrawColliderGeneric<SphereCollider> {
        protected override void OnDrawCollider(){
            Transform transform = targetCollider.transform;

            Vector3 position = transform.position;
            Vector3 scale = transform.lossyScale;
            Vector3 colliderCenter = targetCollider.center;
            Vector3 center = position + colliderCenter;

            float absX = Mathf.Abs(scale.x);
            float absY = Mathf.Abs(scale.y);
            float absZ = Mathf.Abs(scale.z);
            float scaleSign = Mathf.Sign(absX + absY + absZ);

            float scaleValue = Mathf.Max(absX, Mathf.Max(absY, absZ)) * scaleSign;
            float radius = targetCollider.radius * scaleValue;

            DrawCircle(center, transform.forward, radius);
            DrawCircle(center, transform.up, radius);
            DrawCircle(center, transform.right, radius);

            Camera sceneViewCamera = this.sceneViewCamera;
            if(sceneViewCamera != null){
                DrawCircle(center, sceneViewCamera.transform.forward, radius);
            }
        }
    }
}