using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class ItemUI : MonoBehaviour
{
    private static ItemUI _instance;
    public static ItemUI Instance => _instance ?? (_instance = new ItemUI());

    [SerializeField] private InfoUI weaponInfo, armInfo, bodyInfo, legInfo;
    private List<DiceInfoUI> dicesInfo;
    private List<InfoUI> itemsInfo;
}
