using UnityEngine;

public class VictoryHouse : MonoBehaviour
{
    
    private void Start()
    {
        Finish.OnFinish += Finish_OnFinish;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Finish.OnFinish -= Finish_OnFinish;
    }

    private void Finish_OnFinish(Finish obj)
    {
        gameObject.SetActive(true);
    }
}
