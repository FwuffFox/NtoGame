using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player.FightingSystem;
using GameScripts.Logic.Weapons;
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
		
		public bool CanAttack = true;
		
		public bool IsWeaponActive
		{
			get => Sword.IsColliderActive;
			set => Sword.IsColliderActive = value;
		}

		[SerializeReadOnly] public ComboStateMachine MeleeComboStateMachine;

		public Sword Sword;
		public void OnEnable()
		{
			MeleeComboStateMachine = GetComponent<ComboStateMachine>();
			Sword.PlayerAttack = this;
		}

		public void SetProperties(PlayerData data)
		{
			_defaultDamage = data.attack.Damage;
			Damage = _defaultDamage;
		}
	
		private void Update()
		{
			if (Time.timeScale == 0f) return;
			if (!CanAttack) return;
			if (Input.GetMouseButton(0) &&
			    MeleeComboStateMachine.CurrentState.GetType() == typeof(IdleState))
			{
				MeleeComboStateMachine.SetNextState(new EntryState());
			}
		}
	}
}
