using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RGJ{
public class DistanceToHomebase : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float distanceFactor;
    [SerializeField, FoldoutGroup("References"), Required] private Transform home;
    [SerializeField, FoldoutGroup("References"), Required] private Transform ball;
    [SerializeField, FoldoutGroup("References"), Required] private TextMeshProUGUI distanceText;
    
    private void Update()
    {
        var heading = home.position - ball.position;
        var distance = heading.magnitude;
        distance *= distanceFactor;
        
        var distanceInt = Mathf.RoundToInt(distance);
        
        distanceText.SetText(distanceInt.ToString());
    }
}
}