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
            float scaleValue = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
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