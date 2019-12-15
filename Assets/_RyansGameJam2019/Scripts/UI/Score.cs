using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class Score : MonoBehaviour
{
    [SerializeField, BoxGroup("Atom Values"), Required] private IntReference collectedPieces;
    [SerializeField, FoldoutGroup("References"), Required] private TextMeshProUGUI txt_score;
    
    private void Update()
    {
        txt_score.SetText(collectedPieces.Value.ToString());
    }
}
}