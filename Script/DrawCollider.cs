using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;            
#endif

namespace QY.Debug{
    /// <summary>
    /// Draw Collider base Class 
    /// </summary>
    public class DrawCollider : MonoBehaviour {
        [SerializeField]
        [FormerlySerializedAs("m_onlyRuningNotPlaying")]//Compatible with the old version
        private bool m_onlyNotPlaying;

        /// <summary>
        /// get or set only Not Playing status
        /// if true playing mode not draw wire frame
        /// </summary>
        /// <returns></returns>
        protected bool onlyNotPlaying{get{return m_onlyNotPlaying;} set { m_onlyNotPlaying = value;}}

        [SerializeField]
        private Color m_color = new Color(127 / 255f, 201 / 255f, 122 / 255f);

        /// <summary>
        /// get or set Collider wire frame Color
        /// </summary>
        /// <returns></returns>
        protected Color color{get{ return m_color;} set { m_color = value;}}


        /// <summary>
        /// get scene view camera component
        /// <para>if this view not exists or runing not editor mode, return null</para>
        /// </summary>
        /// <returns></returns>
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

        
        /// <summary>
        /// Unity OnDrawGizmos
        /// </summary>
        protected void OnDrawGizmos() {
            if(m_onlyNotPlaying){
                if(Application.isPlaying){
                    return;
                }
            }

            if(this.enabled){
                Gizmos.color = color;

                OnDraw();
            }
        }

        /// <summary>
        /// Draw event
        /// </summary>
        protected virtual void OnDraw(){
            OnDrawCollider();
        }

        /// <summary>
        /// Draw Collider in this method body
        /// </summary>
        protected virtual void OnDrawCollider(){

        }


        /// <summary>
        /// Draw Capsule wire frame
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="centerOffset"></param>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        protected void DrawCapsule(Transform transform, Vector3 centerOffset, float radius, float height){
            //up direction
            Vector3 up = transform.up;
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            //get characterController component pos
            Vector3 position = transform.position;

            Vector3 scale = transform.lossyScale;

            float circleSacleValue = Mathf.Max(scale.x, scale.z);

            radius *= circleSacleValue;

            float circleOffsetY = height / 2f  * scale.y - radius;//;

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

        /// <summary>
        /// Draw half circle
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        protected void DrawCircleHalf(Vector3 pos, Vector3 direction, float radius){
            DrawCircle(0, Mathf.PI, false, pos, direction, radius);
        }


        /// <summary>
        /// Draw circle
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        protected void DrawCircle(Vector3 pos, Vector3 direction, float radius){
            DrawCircle(0, 2 * Mathf.PI, true, pos, direction, radius);
        }
        
        /// <summary>
        /// Draw circle
        /// </summary>
        /// <param name="thetaStart"></param>
        /// <param name="thetaEnd"></param>
        /// <param name="linkStartAndEnd"></param>
        /// <param name="pos"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        protected void DrawCircle(float thetaStart, float thetaEnd, bool linkStartAndEnd, Vector3 pos, Vector3 direction, float radius){
            float perimeter = 2 * Mathf.PI * radius;
            float pixelPerimeter = perimeter / 100f;

            DrawCircle(thetaStart, thetaEnd, linkStartAndEnd, pos, direction, radius, pixelPerimeter);
        }

        /// <summary>
        /// Dircle circle implement
        /// </summary>
        /// <param name="thetaStart"></param>
        /// <param name="thetaEnd"></param>
        /// <param name="linkStartAndEnd"></param>
        /// <param name="pos"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        /// <param name="thetaValue"></param>
        private void DrawCircle(float thetaStart, float thetaEnd, bool linkStartAndEnd, Vector3 pos, Vector3 direction, float radius, float thetaValue){
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