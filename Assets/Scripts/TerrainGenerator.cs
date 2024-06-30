using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviourPunCallbacks
{
    [SerializeField] private SpriteRenderer backgroundRenderer;

    [SerializeField] private List<Sprite> treeSprites;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private int treeCount;

    [SerializeField] private List<Sprite> rockSprites;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private int rockCount;

    private GameObject _treeGroup = null;
    private GameObject _rockGroup = null;

    //private void Start()
    //{
    //    if (PhotonNetwork.PlayerList.Length == 1)
    //    {
    //        GenerateTerrain();
    //    }
    //}

    public void GenerateTerrain()
    {
        GenerateGameObjectGroup(ref _treeGroup, treeSprites, treePrefab, treeCount);
        GenerateGameObjectGroup(ref _rockGroup, rockSprites, rockPrefab, rockCount);
    }

    private void GenerateGameObjectGroup(ref GameObject group, List<Sprite> sprites, GameObject prefab, int count)
    {
        if (group != null)
        {
            DestroyImmediate(group);
        }
        group = new GameObject("Group");
        group.transform.parent = transform;

        for (int i = 0; i < count; ++i)
        {
            var randomSprite = sprites[Random.Range(0, sprites.Count)];

            var halfWidth = backgroundRenderer.bounds.size.x / 2;
            var halfHeight = backgroundRenderer.bounds.size.y / 2;
            var randomX = Random.Range(-halfWidth, halfWidth);
            var randomY = Random.Range(-halfHeight, halfHeight);

            GameObject gameObject = Instantiate(prefab, new Vector3(randomX, randomY, prefab.transform.position.z), Quaternion.identity);

            gameObject.GetComponent<SpriteRenderer>().sprite = randomSprite;
            gameObject.transform.parent = group.transform;

            var randomScale = Random.Range(0.35f, 0.5f);
            gameObject.transform.localScale = new Vector3(randomScale, randomScale, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        }
    }
}