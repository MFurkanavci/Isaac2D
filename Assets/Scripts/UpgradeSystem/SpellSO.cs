using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipable/SpellSO", order = 1)]
public class SpellSO : ScriptableObject {
    
    public List<Upgrade> upgrades;
}
