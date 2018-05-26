using UnityEngine;
namespace QY.Debug{
    public abstract class DrawColliderGeneric<T>  : DrawCollider where T : Collider {
        [SerializeField]
        private T m_targetCollider;

        /// <summary>
        /// get or set target collider of Generic <T>
        /// </summary>
        /// <returns></returns>
        protected T targetCollider{get{return m_targetCollider;} set { m_targetCollider = value;}}

        /// <summary>
        /// runing script attach to gameobject or click component content menu child reset menu
        /// </summary>
        protected virtual void Reset(){
            InitColliderReferences();
        }


        /// <summary>
        /// runing playing
        /// </summary>
        protected virtual void Start(){
            InitColliderReferences();
        }

        /// <summary>
        /// Init Collider References
        /// </summary>
        private void InitColliderReferences(){
            if(m_targetCollider == null){
                m_targetCollider = GetComponent<T>();
            }
        }

        /// <summary>
        /// Draw event
        /// </summary>
        protected override void OnDraw(){
            if(targetCollider != null){
                OnDrawCollider();
            }
        }
    }
}