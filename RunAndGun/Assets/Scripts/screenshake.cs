using UnityEngine;

public class screenshake : MonoBehaviour
{
    public static screenshake instance;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    public float rotationMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        if(shakeTimeRemaining >0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float lenght,float power)
    {
        shakeTimeRemaining = lenght;
        shakePower = power;

        shakeFadeTime = power / lenght;
        shakeRotation = power * rotationMultiplier;
    }
}
