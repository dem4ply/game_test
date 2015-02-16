using UnityEngine;
using System.Collections;

public class Ch_motor_player_sp : Ch_motor {

	public float max_tilt = 0f;
	public Boundary boundary;

	protected override void _processes_motion(){
		if (move_vector.magnitude > 1f)
			move_vector.Normalize();
		_rigidbody.velocity = move_vector * move_speed;
		_is_on_boundary_truncate();
	}

	protected override void _processes_rotation(){
		_rigidbody.rotation = Quaternion.Euler(0f, 0f, _rigidbody.velocity.x * -max_tilt);
	}

	protected virtual void _is_on_boundary_truncate(){
		_rigidbody.position = new Vector3(
			Mathf.Clamp(_rigidbody.position.x, boundary.x_min, boundary.x_max),
			0f,
			Mathf.Clamp(_rigidbody.position.z, boundary.y_min, boundary.y_max)
		);
	}
}

[System.Serializable]
public class Boundary{
	public float x_min, x_max, y_min, y_max;
}