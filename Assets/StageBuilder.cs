using System;
using UnityEngine;

public class StageBuilder : MonoBehaviour
{
    public GameObject blockPrefab;

    public GameObject enemyPrefab;

    public GameObject itemPrefab;

    private float blockSize = 1.0f;
    private Vector2 stageOffset = new Vector2(-3f, -3f);
    private int[,] stageData = new int[,]
    {

        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
    };

    void Start()
    {
        BuildStage();
    }


    private void BuildStage()
    {
        int height = stageData.GetLength(0);
        int width = stageData.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int cellType = stageData[y, x];

                Vector3 position = new Vector3(
                    x * blockSize + stageOffset.x,
                    y * blockSize + stageOffset.y,
                    0
                );

                switch (cellType)
                {
                    case 1:
                        SpawnObject(blockPrefab, position, "Blocks");
                        break;
                    case 2:
                        SpawnObject(enemyPrefab, position, "Enemies");
                        break;
                    case 3:
                        SpawnObject(itemPrefab, position, "Items");
                        break;
                }
            }
        }
    }

    private void SpawnObject(GameObject prefab, Vector3 position, string parentName)
    {
        if (prefab == null)
        {
            Debug.LogWarning($"Prefabが設定されていません: {parentName}");
            return;
        }
        Transform parent = GetOrCreateParent(parentName);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, parent);
        obj.name = $"{prefab.name}_{position.x}_{position.y}";
    }

    private Transform GetOrCreateParent(string parentName)
    {
        GameObject parent = GameObject.Find(parentName);
        if (parent == null)
        {
            parent = new GameObject(parentName);
        }
        return parent.transform;
    }
    private void OnDrawGizmos()
    {
        if (stageData == null) return;

        int height = stageData.GetLength(0);
        int width = stageData.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int cellType = stageData[y, x];
                Vector3 position = new Vector3(
                    x * blockSize + stageOffset.x,
                    y * blockSize + stageOffset.y,
                    0
                );

                switch (cellType)
                {
                    case 1:
                        Gizmos.color = Color.gray;
                        Gizmos.DrawWireCube(position, Vector3.one * blockSize * 0.9f);
                        break;
                    case 2:
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(position, blockSize * 0.4f);
                        break;
                    case 3:
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawWireSphere(position, blockSize * 0.3f);
                        break;
                }
            }
        }
    }
}
