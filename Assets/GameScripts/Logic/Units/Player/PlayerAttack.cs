using System;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player.FightingSystem;
using GameScripts.Logic.Weapons;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
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
			PlayerInputSystem.InGame.AttackButton.performed += AttackButton_Pressed;
			Sword.PlayerAttack = this;
		}

		public void SetProperties(PlayerData data)
		{
			_defaultDamage = data.attack.Damage;
			Damage = _defaultDamage;
		}
	
		private void AttackButton_Pressed(InputAction.CallbackContext context)
		{
			if (Time.timeScale == 0f) return;
			if (!CanAttack) return;	 
			if (MeleeComboStateMachine.CurrentState.GetType() == typeof(IdleState))
			{
				MeleeComboStateMachine.SetNextState(new EntryState());
			}
		}

		private void OnDisable()
		{
			PlayerInputSystem.InGame.AttackButton.performed -= AttackButton_Pressed;
		}
	}
}
