using System;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player.FightingSystem;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Logic.Units.Player
{
	[RequireComponent(typeof(ComboStateMachine))]
	public class PlayerAttack : MonoBehaviour
	{
		private int _defaultDamage;
		[field: SerializeField] public int Damage { get; set; }
		public void ResetDamage() => Damage = _defaultDamage;

		[SerializeField, SerializeReadOnly] private bool _canAttack = true;

		[SerializeReadOnly] public ComboStateMachine MeleeComboStateMachine;
		public void OnEnable()
		{
			MeleeComboStateMachine = GetComponent<ComboStateMachine>();
		}

		public void SetProperties(PlayerData data)
		{
			_defaultDamage = data.attack.Damage;
			Damage = _defaultDamage;
		}
	
		private void Update()
		{
			if (!_canAttack) return;
			//print($"{nameof(MeleeComboStateMachine)}:{MeleeComboStateMachine == null}");
			//print($"{nameof(MeleeComboStateMachine.CurrentState)}:{MeleeComboStateMachine.CurrentState == null}");
			if (Input.GetMouseButton(0) &&
			    MeleeComboStateMachine.CurrentState.GetType() == typeof(IdleState))
			{
				MeleeComboStateMachine.SetNextState(new EntryState());
			}
		}
	}
}
