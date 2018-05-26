using UnityEngine;
namespace QY.Debug{
    public class DrawColliderGeneric<T>  : DrawCollider where T : Collider {
        [SerializeField]
        private T m_targetCollider;

        protected T targetCollider{get{return m_targetCollider;} set { m_targetCollider = value;}}

        protected virtual void Reset(){
            InitCharacterControllerReferences();
        }

        protected virtual void Start(){
            InitCharacterControllerReferences();
        }
        private void InitCharacterControllerReferences(){
            if(m_targetCollider == null){
                m_targetCollider = GetComponent<T>();
            }
        }

        protected override void OnDraw(){
            if(targetCollider != null){
                OnDrawCollider();
            }
        }
    }
}