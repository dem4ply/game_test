using UnityEngine;
using System.Collections;

public class Ch_controller : MonoBehaviour {

	protected Transform _transform;
	protected Ch_motor _motor;

	protected virtual void Awake() {
		_init_cache();
	}

	public void change_moving_vector(Vector3 moving){
		_motor.move_vector = moving;
	}

	protected virtual void _init_cache(){
		_transform = transform;
		_motor = GetComponent<Ch_motor>();
	}
}
