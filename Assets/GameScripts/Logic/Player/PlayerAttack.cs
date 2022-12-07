using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScripts.Logic.Enemy;
using GameScripts.Logic.Player;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private int _damage=35;
	private bool _canAttack=true;
	[SerializeField] private float _attackCooldown=0.6f;
	[SerializeField] private float _attackRange=2.0f;
	[SerializeField] private Transform model;
	[SerializeField] private PlayerAnimator _playerAnimator;
	
	private void Update()
	{
		if (!_canAttack) return;
		if (!Input.GetMouseButtonDown(0)) return;
		var shotDirectrion=model.TransformDirection(Vector3.forward);
		RaycastHit hit;
		Physics.Raycast(transform.position, shotDirectrion, out hit, _attackRange);
		if (hit.collider!=null&&hit.collider.gameObject.layer==8) {
			hit.transform.GetComponent<EnemyAI>().SetDamage(_damage);
		}
		_playerAnimator.SetAttack();
		StartCoroutine(AttackCooldown());
	}
	
	 private IEnumerator AttackCooldown() {
            _canAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
     }
}
