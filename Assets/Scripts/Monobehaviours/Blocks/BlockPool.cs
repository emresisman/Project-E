using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectE
{
    public class BlockPool : MonoBehaviour
    {
        private Queue<Block> blocks = new Queue<Block>();

        [SerializeField] private List<Block> blockPrefabs;
        [SerializeField] private int poolCount;
        [SerializeField] private Transform poolPosition;

        private void Start()
        {
            InstantiateBlocks();
        }

        private void InstantiateBlocks()
        {
            int i = 0;
            while (i < poolCount)
            {
                Block bl = Instantiate(GetRandomPrefab());
                bl.transform.parent = this.transform;
                blocks.Enqueue(bl);
                ResetBlock(bl);
                i++;
            }
        }

        private Block GetRandomPrefab()
        {
            return blockPrefabs[Random.Range(0, blockPrefabs.Count)];
        }

        private void ResetBlock(Block block)
        {
            block.ResetVelocity();
            block.gameObject.transform.position = poolPosition.position;
            block.gameObject.SetActive(false);
        }

        public Block GetBlock()
        {
            return blocks.Dequeue();
        }

        public void AddBlockToPool(Block block)
        {
            blocks.Enqueue(block);
            ResetBlock(block);
        }
    }
}
