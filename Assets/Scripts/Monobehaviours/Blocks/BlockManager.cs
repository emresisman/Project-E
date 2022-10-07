using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectE.PlayerScope;

namespace ProjectE
{
    public class BlockManager : MonoBehaviour
    {
        public static BlockManager Instance { get => instance; }
        private static BlockManager instance;

        private List<Block> activeBlocks = new List<Block>();

        [SerializeField] private BlockPool blockPool;
        private BoxCollider safeFallArea;
        private Block currentBlock;
        private bool isGameRunning = true;

        private void Awake()
        {
            instance = this;
            safeFallArea = Player.instance.BlockFallArea;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnBlock());
        }

        private void OnDisable()
        {
            StopCoroutine(SpawnBlock());
        }

        private void CreateBlock()
        {
            currentBlock = blockPool.GetBlock();
            currentBlock.gameObject.transform.position = GetRandomSpawnPosition();
            currentBlock.gameObject.SetActive(true);
            activeBlocks.Add(currentBlock);
        }

        public void DestroyBlock(Block block)
        {
            activeBlocks.Remove(block);
            blockPool.AddBlockToPool(block);
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 extents = safeFallArea.size / 2f;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                10f,
                Random.Range(-extents.z, extents.z)
            ) + safeFallArea.center;
            return safeFallArea.transform.TransformPoint(point);
            
        }

        IEnumerator SpawnBlock()
        {
            while (isGameRunning)
            {
                yield return new WaitForSeconds(0.5f);
                CreateBlock();
            }
        }

        private void ClearAllBlock()
        {
            foreach (Block block in activeBlocks)
            {
                DestroyBlock(block);
            }
        }
    }
}
