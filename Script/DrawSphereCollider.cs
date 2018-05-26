using UnityEngine;


namespace QY.Debug{
    [AddComponentMenu("DrawCollider/DrawSphereCollider")]
    public class DrawSphereCollider : DrawColliderGeneric<SphereCollider> {
        protected override void OnDrawCollider(){
            Transform transform = targetCollider.transform;

            float radius = targetCollider.radius;
            Vector3 position = transform.position;
            Vector3 colliderCenter = targetCollider.center;
            Vector3 center = position + colliderCenter;

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