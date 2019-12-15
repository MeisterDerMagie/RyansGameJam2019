using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class DeactivateGameObjectOnTriggerEnter : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private string collisionTag;
    [SerializeField, FoldoutGroup("References"), Required] private GameObject gameObjectToDeactivate;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(collisionTag)) return;

        gameObject.SetActive(false);
        gameObjectToDeactivate.transform.DOScale(Vector3.zero, 0.5f).OnComplete(DeactivateGameObject).SetEase(Ease.InCubic);
    }

    private void DeactivateGameObject()
    {
        gameObjectToDeactivate.SetActive(false);
    }
}
}