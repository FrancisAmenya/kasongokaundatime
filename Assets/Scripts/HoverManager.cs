using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class HoverManager : MonoBehaviour
{
    public float maxHeight, minHeight;

    float hoverHeight, hoverRange, hoverSpeed;

    public Vector3 coinPosition;

    public bool hoverStatus, hoverInitialized;

    public float timeValue = 0f;

    public int timeInt;


    // Start is called before the first frame update
    void Start()
    {

        hoverStatus = true;

        hoverInitialized = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (hoverStatus)
        {

            transform.localPosition = new Vector3(coinPosition.x, (coinPosition.y +
                (.1f * hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange)), coinPosition.z);

        }

    }

    public IEnumerator WaitForCore(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);

        hoverHeight = (maxHeight + minHeight) / 2.0f;

        hoverRange = maxHeight - minHeight;

        hoverSpeed = ((int)(Random.Range(2.9f, 3.1f) * 10)) / 10;

        coinPosition = transform.localPosition;

        hoverInitialized = true;

        //();

    }

}
