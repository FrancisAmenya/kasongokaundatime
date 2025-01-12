using UnityEngine;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public GameObject[] prefabs;
        public float speed;
        public float spawnInterval;
        public float spawnHeight;
        private float timer;

        public void UpdateTimer()
        {
            timer += Time.deltaTime;
        }

        public bool CanSpawn()
        {
            return timer >= spawnInterval;
        }

        public void ResetTimer()
        {
            timer = 0f;
        }
    }

    [Header("Layer Configuration")]
    [SerializeField] private ParallaxLayer foregroundLayer;
    [SerializeField] private ParallaxLayer nearLayer;
    [SerializeField] private ParallaxLayer midLayer;
    [SerializeField] private ParallaxLayer farLayer;

    private List<GameObject> activeObjects = new List<GameObject>();
    private float screenWidth;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
    }

    private void Update()
    {
        HandleLayer(foregroundLayer);
        HandleLayer(nearLayer);
        HandleLayer(midLayer);
        HandleLayer(farLayer);
        CleanupOffscreenObjects();
    }

    private void HandleLayer(ParallaxLayer layer)
    {
        layer.UpdateTimer();

        if (layer.CanSpawn())
        {
            SpawnObject(layer);
            layer.ResetTimer();
        }

        MoveLayerObjects(layer);
    }

    private void SpawnObject(ParallaxLayer layer)
    {
        if (layer.prefabs.Length == 0) return;

        float spawnX = mainCamera.transform.position.x + screenWidth;
        Vector3 spawnPosition = new Vector3(spawnX, layer.spawnHeight, 0);
        
        GameObject prefab = layer.prefabs[Random.Range(0, layer.prefabs.Length)];
        GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
        activeObjects.Add(obj);
    }

    private void MoveLayerObjects(ParallaxLayer layer)
    {
        foreach (GameObject obj in activeObjects)
        {
            if (obj != null)
            {
                obj.transform.Translate(Vector3.left * layer.speed * Time.deltaTime);
            }
        }
    }

    private void CleanupOffscreenObjects()
    {
        float destroyX = mainCamera.transform.position.x - screenWidth;
        
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            if (activeObjects[i] == null) continue;
            
            if (activeObjects[i].transform.position.x < destroyX)
            {
                Destroy(activeObjects[i]);
                activeObjects.RemoveAt(i);
            }
        }
    }
}
