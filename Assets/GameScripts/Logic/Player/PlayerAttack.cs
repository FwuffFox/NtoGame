using System;
using System.Collections;
using System.Collections.Generic;
using EditorScripts.Inspector;
using UnityEngine;
using GameScripts.Logic.Enemy;
using GameScripts.Logic.Player;
using GameScripts.StaticData.ScriptableObjects;

public class PlayerAttack : MonoBehaviour
{
	private int _defaultDamage;
	[field: SerializeField] public int Damage { get; set; }
	public void ResetDamage() => Damage = _defaultDamage;

	[SerializeField, SerializeReadOnly] private bool _canAttack = true;
	[field: SerializeField] public float AttackCooldown { get; set; }
	[SerializeField] private float _attackRange;
	[SerializeField] private Transform model;
	[SerializeField] private LayerMask enemyMask;

	public Action OnAttack;

	public void SetProperties(PlayerData data)
	{
		_defaultDamage = data.attack.Damage;
		Damage = _defaultDamage;
		AttackCooldown = data.attack.AttackCooldown;
		_attackRange = data.attack.AttackRange;
	}
	
	private void Update()
	{
		if (!_canAttack) return;
		if (!Input.GetMouseButtonDown(0)) return;
		var shotDirection=model.TransformDirection(Vector3.forward);
		Physics.SphereCast(transform.position,1,shotDirection,out RaycastHit hit,_attackRange,enemyMask);
		if (hit.collider!=null)
		{
			hit.transform.GetComponent<EnemyAI>().SetDamage(Damage);
		}
		OnAttack?.Invoke();
		StartCoroutine(AttackCooldownCoroutine());
	}
	
	private void OnDrawGizmos() {
		var shotDirection=model.TransformDirection(Vector3.forward);
		Gizmos.DrawWireSphere(transform.position+shotDirection,_attackRange);
	}
	
	 private IEnumerator AttackCooldownCoroutine() 
	 {
        _canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        _canAttack = true;
     }
}
