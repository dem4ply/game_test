using UnityEngine;
using System;

namespace helper {
	public static class inventory {
		public static void clone_item(GameObject item, out Item item_out, out GameObject obj_out, int amount) {
			item_out = get_Item<Item>(item);
			item_out = Items_pool.instance.pop(item_out) as Item;
			obj_out = item_out.gameObject;
			item_out.amount = amount;
		}
		
		public static void clone_item(GameObject item, out Item item_out, int amount) {
			GameObject tmp;
			clone_item(item, out item_out, out tmp, amount);
		}
		
		public static void clone_item(GameObject item, out Item item_out) {
			GameObject tmp;
			clone_item(item, out item_out, out tmp, 0);
		}
		
		
		public static void clone_item(Item item, out Item item_out, out GameObject obj_out, int amount) {
			item_out = Items_pool.instance.pop(item) as Item;
			obj_out = item_out.gameObject;
			item_out.amount = amount;
		}
		
		public static void clone_item(Item item, out Item item_out, int amount) {
			GameObject tmp;
			clone_item(item, out item_out, out tmp, amount);
		}
		
		public static void clone_item(Item item, out Item item_out) {
			GameObject tmp;
			clone_item(item, out item_out, out tmp, 0);
		}
		
		public static T get_Item<T>(GameObject obj) where T : Item {
			return obj.GetComponent<T>();
		}
		
	}
}
