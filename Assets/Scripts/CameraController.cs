using UnityEngine;

namespace PathCreation.Examples
{
	// Moves along a path at constant speed.
	// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
	public class CameraController : MonoBehaviour
	{
		public PathCreator pathCreator;
		public PathFollower player;
		public EndOfPathInstruction endOfPathInstruction;
		private Vector3 pos;

		private Vector3 smoothPos;
		public float dist;
	
		float distanceTravelled;

		void Update()
		{
			if (pathCreator != null)
			{
                distanceTravelled = player.getDistance();
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                pos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
				smoothPos= Vector3.Lerp(transform.position, new Vector3(pos.x, 25, pos.z),0.9f);
				transform.position = smoothPos + (transform.forward * -100);

			}
		}
	}
}