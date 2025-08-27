using QFramework;
using UnityEngine;

namespace ProjectSurvicor
{
    public abstract class GamePlayObject : ViewController
    {
        protected abstract Collider2D Collider2D { get; }
        private void OnBecameVisible()
        {
            Collider2D.enabled=true;
        }
        private void OecameInvisible()
        {
            Collider2D.enabled=false;
        }
    }
}