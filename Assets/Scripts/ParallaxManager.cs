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
        public GameObject spawnReference;
        public int sortingOrder;
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
    [SerializeField] private ParallaxLayer bushLayer;
    [SerializeField] private ParallaxLayer treeLayer;
    [SerializeField] private ParallaxLayer blimpLayer;

    private Dictionary<GameObject, ParallaxLayer> objectLayerMap = new Dictionary<GameObject, ParallaxLayer>();
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
        HandleLayer(bushLayer);
        HandleLayer(treeLayer);
        HandleLayer(blimpLayer);
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
        if (layer.prefabs.Length == 0 || layer.spawnReference == null) return;

        float spawnX = mainCamera.transform.position.x + screenWidth;
        Vector3 spawnPosition = new Vector3(spawnX, layer.spawnReference.transform.position.y, 10);
        
        GameObject prefab = layer.prefabs[Random.Range(0, layer.prefabs.Length)];
        GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
        
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (layer == foregroundLayer)
                spriteRenderer.sortingLayerName = "Foreground";
            else if (layer == nearLayer)
                spriteRenderer.sortingLayerName = "Background_Buildings";
            else if (layer == midLayer)
                spriteRenderer.sortingLayerName = "Background_Buildings_Silhouettes";
            else if (layer == farLayer)
                spriteRenderer.sortingLayerName = "Background_Weather";
            else if (layer == bushLayer)
                spriteRenderer.sortingLayerName = "Background_Bushes";
            else if (layer == treeLayer)
                spriteRenderer.sortingLayerName = "Background_Trees";
            else if (layer == blimpLayer)
                spriteRenderer.sortingLayerName = "Background_Blimp";

            spriteRenderer.sortingOrder = layer.sortingOrder;
        }

        objectLayerMap.Add(obj, layer);
        activeObjects.Add(obj);
    }

    private void MoveLayerObjects(ParallaxLayer layer)
    {
        foreach (GameObject obj in activeObjects)
        {
            if (obj != null && objectLayerMap.TryGetValue(obj, out ParallaxLayer objLayer) && objLayer == layer)
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
                objectLayerMap.Remove(activeObjects[i]);
                Destroy(activeObjects[i]);
                activeObjects.RemoveAt(i);
            }
        }
    }
}
