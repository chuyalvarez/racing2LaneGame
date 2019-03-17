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
		public float dist;
	
		float distanceTravelled;

		void Update()
		{
			if (pathCreator != null)
			{
				pos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
				distanceTravelled = player.getDistance()-dist;
				transform.position = new Vector3(pos.x, 25, pos.z);
				transform.LookAt(player.transform.position);
			}
		}
	}
}