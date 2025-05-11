using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    public Transform minPos, maxPos;

    [Header("Prefabs to Spawn")]
    public GameObject[] objects;

    [Header("Timing")]
    public float timeBetweenSpawns = 1f;

    private float _spawnCounter;
    private List<GameObject> _validPrefabs;

    void Awake()
    {
        _spawnCounter = timeBetweenSpawns;

        // Monta a lista só com prefabs não-nulos
        _validPrefabs = new List<GameObject>();
        if (objects != null)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                    _validPrefabs.Add(objects[i]);
                else
                    Debug.LogWarning($"ObjectSpawner: slot {i} de 'objects' está nulo — remova no Inspector.");
            }
        }}

    void Update()
    {
        // Se não tiver limites ou prefabs válidos, não faz nada
        if (minPos == null || maxPos == null || _validPrefabs.Count == 0)
            return;

        _spawnCounter -= Time.deltaTime;
        if (_spawnCounter > 0f) 
            return;

        _spawnCounter = timeBetweenSpawns;

        // Escolhe um prefab aleatório (sempre não-nulo)
        var prefab = _validPrefabs[Random.Range(0, _validPrefabs.Count)];
        if (prefab == null) 
            return;

        // Instancia o clone
        var clone = Instantiate(prefab);

        // Posiciona apenas se as referências ainda existirem
        if (minPos != null && maxPos != null)
        {
            float x = Random.Range(minPos.position.x, maxPos.position.x);
            float y = minPos.position.y;
            clone.transform.position = new Vector3(x, y, prefab.transform.position.z);
        }
    }
}
