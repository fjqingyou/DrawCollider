using UnityEngine;
using System;
using System.Reflection;
namespace QY.Debug{
    [AddComponentMenu("Draw Collider/Draw Character Controller")]
    public class DrawCharacterController : DrawColliderGeneric<CharacterController> {
        protected override void OnDrawCollider(){
            DrawCapsule(targetCollider.transform, targetCollider.center, targetCollider.radius, targetCollider.height);
        }
    }
}
