using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;            
#endif

namespace QY.Debug{
    public class DrawCollider : MonoBehaviour {
        [SerializeField]
        private bool m_onlyRuningNotPlaying;
        protected bool onlyRuningNotPlaying{get{return m_onlyRuningNotPlaying;} set { m_onlyRuningNotPlaying = value;}}

        [SerializeField]
        private Color m_color = new Color(127 / 255f, 201 / 255f, 122 / 255f);
        protected Color color{get{ return m_color;} set { m_color = value;}}


        protected Camera sceneViewCamera{
            get{
                Camera camera = null;
#if UNITY_EDITOR
                SceneView sceneView = SceneView.currentDrawingSceneView;
                if(sceneView != null){
                     camera =  sceneView.camera;
                }
#endif
                return camera;
            }
        }

        
        protected void OnDrawGizmos() {
            if(m_onlyRuningNotPlaying){
                if(Application.isPlaying){
                    return;
                }
            }

            if(this.enabled){
                Gizmos.color = color;

                OnDraw();
            }
        }

        protected virtual void OnDraw(){
            OnDrawCollider();
        }

        protected virtual void OnDrawCollider(){

        }


        protected void DrawCapsule(Transform transform, Vector3 centerOffset, float radius, float height){
            //up direction
            Vector3 up = transform.up;
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            //get characterController component pos
            Vector3 position = transform.position;

            float circleOffsetY = height / 2f - radius;

            Vector3 center = position + centerOffset;

            Vector3 posUp = center + Vector3.up * circleOffsetY;
            Vector3 posDown = center + Vector3.down * circleOffsetY;

            if(circleOffsetY < 0f){
                DrawCircle(center, Vector3.up, radius );
            }else{
                //top circle
                DrawCircle(posUp, Vector3.up, radius );

                //top circle
                DrawCircle(posDown, Vector3.down, radius);

                Vector3 leftOffset = Vector3.left * radius;
                Vector3 forwardOffset = Vector3.forward * radius;

                Gizmos.DrawLine(posUp + leftOffset, posDown + leftOffset);
                Gizmos.DrawLine(posUp + -leftOffset, posDown + -leftOffset);
                
                Gizmos.DrawLine(posUp + forwardOffset, posDown + forwardOffset);
                Gizmos.DrawLine(posUp + -forwardOffset, posDown + -forwardOffset);

            }

            DrawCircleHalf(posUp, Vector3.forward, radius);
            DrawCircleHalf(posUp, Vector3.right, radius);

            
            DrawCircleHalf(posDown, Vector3.back, radius);
            DrawCircle(Mathf.PI, 2 * Mathf.PI, false, posDown , Vector3.right, radius);
        }

        
        protected void DrawCircle(Vector3 pos, Vector3 direction, float radius, float thetaValue = 0.01f){
            DrawCircle(0, 2 * Mathf.PI, true, pos, direction, radius, thetaValue);
        }
        
        protected void DrawCircleHalf(Vector3 pos, Vector3 direction, float radius, float thetaValue = 0.01f){
            DrawCircle(0, Mathf.PI, false, pos, direction, radius, thetaValue);
        }

        protected void DrawCircle(float thetaStart, float thetaEnd, bool linkStartAndEnd, Vector3 pos, Vector3 direction, float radius, float thetaValue = 0.01f){
            if (thetaValue < 0.0001f){
                thetaValue = 0.0001f;
            }

            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);

            Vector3 firstPoint = Vector3.zero;
            Vector3 lastPoint = Vector3.zero;
            bool isFrist = true;
            for (float theta = thetaStart; theta <= thetaEnd; theta += thetaValue){
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                Vector3 point = rotation * new Vector3(x, y, 0f);
                if (isFrist){
                    firstPoint = point;
                    isFrist = false;
                }else{
                    Gizmos.DrawLine(pos + lastPoint,pos + point);
                }
                lastPoint = point;
            }
            
            if(linkStartAndEnd){
                Gizmos.DrawLine(pos + firstPoint, pos + lastPoint);
            }
        }
    }
}