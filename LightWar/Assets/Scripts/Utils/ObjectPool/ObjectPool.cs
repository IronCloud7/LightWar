using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Utils
{
    public class ObjectPool
    {
        private readonly RecyclableObject _prefab;
        private readonly HashSet<RecyclableObject> _instantiateObjects;
        private Queue<RecyclableObject> _recycledObjects;

        public ObjectPool(RecyclableObject prefab)
        {
            _prefab = prefab;
            _instantiateObjects = new HashSet<RecyclableObject>();
        }

        public void Init(int numberOfInitialObjects)
        {
            _recycledObjects = new Queue<RecyclableObject>(numberOfInitialObjects);

            for (var i = 0; i < numberOfInitialObjects; i++)
            {
                var instance = InstantiateNewInstance();
                instance.gameObject.SetActive(false);
                _recycledObjects.Enqueue(instance);
            }
        }

        private RecyclableObject InstantiateNewInstance()
        {
            var instance = Object.Instantiate(_prefab);
            instance.Configure(this);
            return instance;
        }

        public T Spawn<T>(Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
        {
            var recyclableObject = GetInstance();
            _instantiateObjects.Add(recyclableObject);
            recyclableObject.transform.position = position;
            recyclableObject.transform.rotation = rotation;
            recyclableObject.gameObject.SetActive(true);
            recyclableObject.Init();
            return recyclableObject.GetComponent<T>();
        }

        private RecyclableObject GetInstance()
        {
            if (_recycledObjects.Count > 0)
            {
                return _recycledObjects.Dequeue();
            }

            //Debug.LogWarning($"No hay suficientes objetos {_prefab.name}, considera aumentar el máximo de objetos iniciales.");
            var instance = InstantiateNewInstance();
            return instance;
        }

        public void RecycleGameObject(RecyclableObject gameObjectToRecycle)
        {
            var wasInstantiated = _instantiateObjects.Remove(gameObjectToRecycle);
            Assert.IsTrue(wasInstantiated, $"{gameObjectToRecycle.name} no estaba instanciado en la pool.");

            gameObjectToRecycle.gameObject.SetActive(false);
            gameObjectToRecycle.Release();
            _recycledObjects.Enqueue(gameObjectToRecycle);
        }
    }
}
