using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.partho.games.utilities
{
	public class GameObjectPool : MonoBehaviour 
	{
		[SerializeField]
		private GameObject _template;
		[SerializeField]
		private int _poolBufferSize = 5;
		[SerializeField]
		private string _poolName = "";

		protected List<GameObject> mAvailableObjects = new List<GameObject>();
		protected List<GameObject> mObjectsInUse = new List<GameObject>();
		public string pName { get { return (string.IsNullOrEmpty(_poolName) ? gameObject.name : _poolName); } }

		public GameObject GetObject()
		{
			if(mAvailableObjects.Count <= 0)
			{
				for(int i = 0; i < _poolBufferSize; ++i)
					mAvailableObjects.Add(CreateNewObject());
			}
			GameObject assignedObject = mAvailableObjects[0];
			mAvailableObjects.RemoveAt(0);
			mObjectsInUse.Add(assignedObject);
			assignedObject.SetActive(true);
			assignedObject.GetComponent<IPoolGameObject>().OnActivated();
			return assignedObject;
		}

		public void ReleaseObject(GameObject go)
		{
			IPoolGameObject poolObject = go.GetComponent<IPoolGameObject>();
			if(poolObject == null)
				throw new UnityException("Object is not a pool Object");
			poolObject.OnDeactivated();
			go.transform.parent = transform;
			go.SetActive(false);
			mObjectsInUse.Remove(go);
			mAvailableObjects.Add(go);
		}
		
		protected GameObject CreateNewObject()
		{
			GameObject go = GameObject.Instantiate<GameObject>(_template, transform);
			go.SetActive(false);
			IPoolGameObject poolObject = go.GetComponent<IPoolGameObject>();
			if(poolObject == null)
			{
				GameObject.DestroyImmediate(go);
				go = null;
				throw new UnityException("Object template is not a Pool Object: " + gameObject.name);
			}
			poolObject.OnCreated(this);
			return go;
		}
	}
}
