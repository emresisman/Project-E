using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;

namespace ProjectE.Projetiles
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float3 spawnPosition;
        [SerializeField] private int spawnSize;
        [SerializeField] private Vector2 moveSpeedLimit;

        private BlobAssetStore blobAsset;

        private void Start()
        {
            blobAsset = new BlobAssetStore();
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAsset);
            var entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(projectilePrefab, settings);
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            for (int i = 0; i < spawnSize; i++)
            {
                var instance = entityManager.Instantiate(entity);

                entityManager.AddComponent<Active>(instance);
                entityManager.AddComponent<MovementSpeed>(instance);
                entityManager.AddComponent<Destination>(instance);
                float moveSpeed = UnityEngine.Random.Range(moveSpeedLimit.x, moveSpeedLimit.y);
                entityManager.SetComponentData<Translation>(instance, new Translation { Value = spawnPosition });
                entityManager.SetComponentData<MovementSpeed>(instance, new MovementSpeed { Value = moveSpeed });
                entityManager.SetComponentData<Destination>(instance, new Destination { Value = float3.zero });
            }
        }

        private void OnDestroy()
        {
            blobAsset.Dispose();
        }
    }
}
