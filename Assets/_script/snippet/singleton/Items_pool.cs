using UnityEngine;
using System.Collections.Generic;

public class Items_pool : Singleton<Items_pool> {
	public Transform container;
	
	protected Items_pool(){}
	
	protected Dictionary<Item, Stack<Item>> _dict;
	
	
	public virtual Item pop(Item key){
		Item result = null;
		if (_dict.ContainsKey(key)) {
			Stack<Item> stack = _dict[key];
			if (stack.Count > 0)
				result = stack.Pop();
		}
		if (result == null){
			result = helper.instantiate.inactive._<Item>(key);
		}
		else
			result.transform.parent = null;
		return result;
	}
	
	public virtual void push(Item value){
		move_to_container(value);
		if (_dict.ContainsKey(value))
			_dict[value].Push(value);
		else {
			Stack<Item> stack_tmp = new Stack<Item>();
			stack_tmp.Push(value);
			_dict.Add(value ,stack_tmp);
		}
	}
	
	public virtual void prepare_objects(Item obj, int amount){
		Stack<Item> stack;
		if (_dict.ContainsKey(obj))
			stack = _dict[obj];
		else
			stack = new Stack<Item>(amount);
		for (; amount > 0; --amount){
			Item obj_tmp = helper.instantiate.inactive.parent<Item>(obj, container);
			stack.Push(obj_tmp);
		}
		_dict.Add(obj, stack);
	}
	
	
	protected virtual void move_to_container(Item obj){
		obj.transform.parent = container;
		obj.gameObject.SetActive(false);
	}
	
	protected virtual void Awake(){
		_dict = new Dictionary<Item, Stack<Item>>();
		if (!container)
			container = this.transform;
		try {
			//prepara 10 instancias de la bala simple
			prepare_objects( List_of_items.instance.test.test_1.GetComponent<Item>(), 10 );
		}
		catch(System.Exception e) {
			Debug.LogError(e);
		}
	}
}

