using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHandling : MonoBehaviour
{
    public static ScreenHandling instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private Vector3 initialPosition;

    public GameObject hit;

    void Start()
    {
        initialPosition = transform.position;
    }

    private IEnumerator ShakeScreen()
    {
        float elapsed = 0.0f;
        float alpha = 0f;

        while (elapsed < shakeDuration)
        {
            alpha = Mathf.Lerp(1, 0, elapsed / shakeDuration);

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), Time.deltaTime * 5f);
            hit.GetComponent<Image>().color = new Color(1, 0, 0, alpha / 3);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = initialPosition;
        hit.GetComponent<Image>().color = new Color(1, 0, 0, 0);
    }

    public void ShakeScreen(float duration = .2f, float magnitude = .5f)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        StartCoroutine(ShakeScreen());
    }
}
