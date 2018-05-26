using UnityEngine;
namespace QY.Debug{
    [AddComponentMenu("Draw Collider/Draw Mesh Collider")]
    public class DrawMeshCollider : DrawColliderGeneric<MeshCollider> {
        protected override void OnDrawCollider(){
            MeshCollider meshCollider = targetCollider;
            Mesh mesh = meshCollider.sharedMesh;
            if(mesh != null){
                Transform transform = meshCollider.transform;
                Vector3 position = transform.position;
                Vector3 scale = transform.lossyScale;

                Vector3 [] vertices = mesh.vertices;
                int [] triangles = mesh.triangles;
                for(int i = 0; i < triangles.Length; i += 3){
                    for(int j = 0 ; j < 3; j++){
                        int triangleIndex = i + j;
                        int index1 = triangles[triangleIndex];
                        int index2;

                        if(j < 2){
                            index2 = triangles[triangleIndex + 1];
                        }else{
                            index2 = triangles[i];
                        }

                        Vector3 vector1 = vertices[index1];
                        Vector3 vector2 = vertices[index2];

                        vector1.Scale(scale);
                        vector2.Scale(scale);

                        Gizmos.DrawLine(position + vector1, position +  vector2);
                    }
                }
            }
        }
    }
}