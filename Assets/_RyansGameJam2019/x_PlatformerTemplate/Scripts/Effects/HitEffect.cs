using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class HitEffect : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        public Material normalMaterial, hitMaterial;

        public float effectDuration;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void PlayEffect()
        {
            StartCoroutine(IHitEffect());
        }

        IEnumerator IHitEffect()
        {
            spriteRenderer.material = hitMaterial; //set hit material

            yield return new WaitForSeconds(effectDuration); //wait time

            spriteRenderer.material = normalMaterial; //return to default
        }

    }
}
