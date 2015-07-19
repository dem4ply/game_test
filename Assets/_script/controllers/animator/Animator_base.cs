using UnityEngine;
using System.Collections;

public class Animator_base : MonoBehaviour {
	public Animator animator; 

	protected void Awake() {
		_init_cache();
	}

	protected void _init_cache() {
		if ( animator == null )
			animator = GetComponent<Animator>();
	}
}
