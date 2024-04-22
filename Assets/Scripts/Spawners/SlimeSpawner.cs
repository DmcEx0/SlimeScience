using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private GeneralSlimeFactory _slimeFactory;

    [SerializeField] private int _slimeCount;
    [SerializeField] private float _rangePosX;
    [SerializeField] private float _rangePosZ;

    private void Start()
    {
        //for (int i = 0; i < _slimeCount; i++)
        //{
        //    float randomPosX = Random.Range(-_rangePosX, _rangePosX);
        //    float randomPosZ = Random.Range(-_rangePosZ, _rangePosZ);
        //    Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ);

        //    var newSlime = _slimeFactory.Get();
        //    newSlime.transform.position = newPos;
        //}
    }
}
