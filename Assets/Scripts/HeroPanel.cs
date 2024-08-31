using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPanel : MonoBehaviour
{
    public GameObject prefab;
    public List<HeroSO> heroes = new List<HeroSO>();
    void Start()
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:HeroSO");
        foreach (var guid in guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            HeroSO hero = UnityEditor.AssetDatabase.LoadAssetAtPath<HeroSO>(path);
            heroes.Add(hero);
        }

        CreateHeroPanel();
    }

    public void CreateHeroPanel()
    {
        foreach (var hero in heroes)
        {
            GameObject go = Instantiate(prefab, transform);
            go.AddComponent<HeroPanelItem>();
            go.GetComponent<HeroPanelItem>().Initialize(hero, gameObject);
        }
    }
}

internal class HeroPanelItem: MonoBehaviour
{
    private HeroSO hero;

    public GameObject panel;

    public void Initialize(HeroSO hero, GameObject panel)
    {
        this.hero = hero;
        this.panel = panel;
        transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = hero.heroName;

        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
        gameObject.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = hero.heroSprite;
    }

    public void OnClick()
    {
        Player.Instance.hero = hero;
        Player.Instance.InitializePlayerStats(hero);
        panel.SetActive(false);
    }
}