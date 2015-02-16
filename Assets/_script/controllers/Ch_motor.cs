using UnityEngine;
using System.Collections;

public class Ch_motor : MonoBehaviour {
	public float move_speed = 20.0f;
	public float max_speed = 10.0f;
	
	public float rotation_speed = 5.0f;
	
	protected Transform _transform;
	protected Rigidbody _rigidbody;

	protected Vector3 _direction_vector;
	protected Transform _look_at;
	protected bool _is_look_at;
	
	public Vector3 move_vector {get; set;}
	public Vector3 direction_vector {
		get{
			return _direction_vector;
		}
		set{
			_direction_vector = value;
			_is_look_at = false;
		}
	}
	public Transform look_at{
		get{
			return _look_at;
		}
		set{
			_look_at = value;
			_is_look_at = true;
		}
	}

	public virtual bool is_moving{
		get{
			return move_vector.x != 0f || move_vector.z != 0f;
		}
	}
	
	protected virtual void Awake(){
		_init_cache();
	}
	
	protected virtual void FixedUpdate(){
		_processes_rotation();
		_processes_motion();
	}
	
	protected virtual void _processes_motion(){
		add_acceleration();
		is_over_max_speed_truncate();
		if (!is_moving) // si no se esta moviendo decrementa la velocidad a la midad
			_rigidbody.velocity = _rigidbody.velocity * 0.5f;
	}
	
	protected virtual void _processes_rotation(){
		Vector3 new_rotation = Vector3.zero;
		//desicion de si esta mirando un elemento o solo girara
		if (_is_look_at)
			new_rotation = Quaternion.LookRotation(look_at.position - _transform.position).eulerAngles;
		else
			new_rotation = Quaternion.LookRotation(direction_vector - _transform.position).eulerAngles;

		new_rotation.x = new_rotation.z = 0; //quitar los valores de x, z para que solo rote en el eje y
		_transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(new_rotation),
		                                      Time.deltaTime *rotation_speed ); //rotacion sueave
	}
	
	public virtual void _init_cache(){
		_transform = transform;
		_rigidbody = rigidbody;
	}
	public virtual void add_acceleration(){
		if (move_vector.magnitude > 1)
			move_vector.Normalize();
		_rigidbody.AddForce(move_vector * move_speed, ForceMode.Acceleration);
	}
	
	public virtual void is_over_max_speed_truncate(){
		if (_rigidbody.velocity.magnitude > max_speed) //si pasa de la velocidad maxima la trunca
			_rigidbody.velocity = _rigidbody.velocity.normalized * max_speed;
	}


}
