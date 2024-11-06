using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipable/WeaponSO", order = 0)]
public class WeaponSO : ScriptableObject {
    
    public List<Upgrade> upgrades;
}
