using UnityEngine;
using System.Collections;
using Spine.Unity;

namespace Commando {
	public class FootSoldier : MonoBehaviour {
		[SpineAnimation("Idle")]
		public string idleAnimation;

		[SpineAnimation]
		public string attackAnimation;

		[SpineAnimation]
		public string moveAnimation;

		[SpineSlot]
		public string eyesSlot;

		[SpineAttachment(currentSkinOnly: true, slotField: "eyesSlot")]
		public string eyesOpenAttachment;

		[SpineAttachment(currentSkinOnly: true, slotField: "eyesSlot")]
		public string blinkAttachment;

		[Range(0, 0.2f)]
		public float blinkDuration = 0.05f;

		public float moveSpeed = 3;

		

		SkeletonAnimation skeletonAnimation;

		void Awake () {
			skeletonAnimation = GetComponent<SkeletonAnimation>();
			skeletonAnimation.OnRebuild += Apply;            
		}

		void Apply (SkeletonRenderer skeletonRenderer) {
			StartCoroutine("Blink");
		}
		
		public void Idle()
		{
			skeletonAnimation.AnimationName = idleAnimation;
		}
		public void Walk(bool is_left)
		{
			if(is_left)
			{
				skeletonAnimation.Skeleton.ScaleX = 1;
			}
			else
			{
				skeletonAnimation.Skeleton.ScaleX = -1;
			}
			skeletonAnimation.AnimationName = moveAnimation;
		}

		IEnumerator Blink() {
			while (true) {
				yield return new WaitForSeconds(Random.Range(0.25f, 3f));
				skeletonAnimation.Skeleton.SetAttachment(eyesSlot, blinkAttachment);
				yield return new WaitForSeconds(blinkDuration);
				skeletonAnimation.Skeleton.SetAttachment(eyesSlot, eyesOpenAttachment);
			}
		}
	}
}
