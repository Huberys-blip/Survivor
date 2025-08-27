using UnityEngine;
using QFramework;
using ProjectSurvicor;

namespace Script
{
	public partial class HurtBox : GamePlayObject
	{
		public GameObject Owner;
		void Awake()
        {
            mCollider2D = GetComponent<Collider2D>();
        }
		void Start()
		{
			if (!Owner)
			{
				Owner = transform.parent.gameObject;
			}
		}
			private Collider2D mCollider2D;
		protected override Collider2D Collider2D => mCollider2D;
	}
}
