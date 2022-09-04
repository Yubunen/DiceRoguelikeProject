using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike.Strategy;

public class ActionSelectUI : MonoBehaviour
{
    private static ActionSelectUI _inst;
    public static ActionSelectUI Inst => _inst;

    [SerializeField] private UnitActionUI prefab;
    [SerializeField] private Button turnEnd;
    private readonly float width = Screen.width;
    private UnitActionUI[] infoUIs;

    private void Awake()
    {
        _inst = this;
    }

    private void Update()
    {
        if (infoUIs != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                turnEnd.onClick.Invoke();
            }
        }
    }

    public void SetActionUI(List<StrategyAction> actions, System.Action<int> returnAction)
    {
        float xInterval = width / (actions.Count + 2);
        infoUIs = new UnitActionUI[actions.Count];

        turnEnd.gameObject.SetActive(true);
        turnEnd.onClick.RemoveAllListeners();
        turnEnd.onClick.AddListener(() =>
        {
            returnAction(-1);
            SelectEnd();
        });
        {
            var pos = turnEnd.transform.localPosition;
            pos.x = (actions.Count + 1) * xInterval;
            turnEnd.transform.localPosition = pos;
        }

        for (int i = 0; i < actions.Count; i++)
        {
            var j = i;
            infoUIs[j] = Instantiate(prefab, transform);
            var pos = infoUIs[j].transform.localPosition;
            pos.x = (j + 1) * xInterval;
            infoUIs[j].transform.localPosition = pos;
            infoUIs[j].SetInfo(actions[j], () =>
            {
                returnAction(j);
                SelectEnd();
            });
        }
    }

    public void SelectEnd()
    {
        turnEnd.gameObject.SetActive(false);
        foreach (var uis in infoUIs)
        {
            Destroy(uis.gameObject);
        }
        infoUIs = null;
    }
}
