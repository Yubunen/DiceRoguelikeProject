using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DiceRogueLike/General/MaterialManager")]
[ExecuteAlways]
public class MaterialManager : ScriptableObject
{
    public Material unitMaterial;
    static Material _unitMaterial;
    public static Material UnitMaterial => _unitMaterial;

    private void OnValidate()
    {
        _unitMaterial = unitMaterial;
    }
}
