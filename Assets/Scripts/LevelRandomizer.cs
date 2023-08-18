using UnityEngine;

public class LevelRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private LevelBlockListSO levelBlockListSO;

    private void Awake()
    {
        foreach (GameObject point in points)
        {
            int randomBlock = Random.Range(0, levelBlockListSO.levelBlockList.Count);
            Instantiate(levelBlockListSO.levelBlockList[randomBlock], point.transform);
        }
    }
}
