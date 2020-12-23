using System;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Main radiusStorage;
        
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private uint count = 10;

        [SerializeField]
        private Transform objectsParent;
        
        public event Action<GameObject> OnPrefabSpawn;

        private Transform _ground;

        private void Start()
        {
            Debug.Assert(prefab != null, $"prefab in {this.DebugObjectName()} must not be null!");
            Debug.Assert(radiusStorage != null, $"radius storage [{nameof(Main)}] == null in {this.DebugObjectName()}!");
            
            CreateGround();
            SpawnAll();
        }

        private void CreateGround()
        {
            _ground = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            _ground.localScale = new Vector3(radiusStorage.Radius * 2, 1, radiusStorage.Radius * 2);
        }

        private void SpawnAll()
        {
            for (var i = 0; i < count; i++)
                SpawnPrefab(prefab, GetRandomPos(radiusStorage.Radius));
        }

        private Vector3 GetRandomPos(float radius)
        {
            return new Vector3(
                Random.Range(-radius, radius), 
                _ground.transform.position.y + _ground.localScale.y / 2, 
                Random.Range(-radius, radius)
                );
        }

        private void SpawnPrefab(GameObject original, Vector3 pos)
        {
            var go = Instantiate(original, pos, Quaternion.identity, objectsParent);
            OnPrefabSpawn?.Invoke(go);
        }
    }
}
