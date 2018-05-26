using UnityEngine;
namespace QY.Debug{
    [AddComponentMenu("DrawCollider/DrawMeshCollider")]
    public class DrawMeshCollider : DrawColliderGeneric<MeshCollider> {
        protected override void OnDrawCollider(){
            MeshCollider meshCollider = targetCollider;
            Mesh mesh = meshCollider.sharedMesh;
            if(mesh != null){
                Vector3 position = meshCollider.transform.position;
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

                        Gizmos.DrawLine(position + vertices[index1], position + vertices[index2]);
                    }
                }
            }
        }
    }
}