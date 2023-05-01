using UnityEngine;

namespace HDyar.SimpleSOStateMachine
{
	/// <summary>
	/// Utility MonoBehaviour Registers itself as a listener to a state, and exposes virtual OnEnterState and OnExitState functions to override.
	/// </summary>
	public class MonoStateListener : MonoBehaviour, IStateListener
	{
		[Tooltip("The State to listen to. States are sub-assets of the desired machine.")]
		[SerializeField] private State _state;

		private void Awake()
		{
			_state.RegisterListener(this);
		}

		private void OnDestroy()
		{
			_state.DeregisterListener(this);
		}

		public virtual void OnEnterState()
		{
			
		}

		public virtual void OnExitState()
		{
			
		}
	}
}