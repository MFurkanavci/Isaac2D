
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("BarUI")]
    public Image hpBar;
    public Image mpBar;
    public Image expBarL, expBarR;

    public float nextHp;
    public float nextMp;
    public float nextExp;

    public float lerpSpeed = 4f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        SetHealth();
        SetMana();
        SetExp();
    }
    public void SetHealth()
    {

        hpBar.fillAmount = math.lerp(hpBar.fillAmount, nextHp, lerpSpeed * Time.deltaTime);
    }
    public void SetMana()
    {

        mpBar.fillAmount = math.lerp(mpBar.fillAmount, nextMp, lerpSpeed * Time.deltaTime);
    }
    public void SetExp()
    {
        expBarL.fillAmount = math.lerp(expBarL.fillAmount, nextExp, lerpSpeed * Time.deltaTime);
        expBarR.fillAmount = math.lerp(expBarR.fillAmount, nextExp, lerpSpeed * Time.deltaTime);

    }
}
