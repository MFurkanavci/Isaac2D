
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("BarUI")]
    public Image hpBar;
    public Image mpBar;
    public float nextHp;
    public float nextMp;

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
    void Update()
    {
        SetHealthToDamage();
        SetMana();
    }
    public void SetHealthToDamage()
    {

        hpBar.fillAmount = math.lerp(hpBar.fillAmount, nextHp, lerpSpeed * Time.deltaTime);
    }
    public void SetMana() 
    {
        
        mpBar.fillAmount = math.lerp(mpBar.fillAmount,nextMp,lerpSpeed*Time.deltaTime);
    }


}
