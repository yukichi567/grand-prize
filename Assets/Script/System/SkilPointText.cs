using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilPointText : MonoBehaviour
{
    [SerializeField] private Text _skilPoint = default;
    
    public void SetSkilPointText(int point)
    {
        _skilPoint.text = $"SkillPoint:{point}";
    }

}
