using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraShakeEffect : MonoBehaviour
    {
        public float duration;
        public float magnitude;

        //Shake method calls CameraShakeEffect.Shake();
        public void Shake()
        {
            StartCoroutine(IShake());
        }

        IEnumerator IShake()
        {
            Vector3 originalPos = transform.position; //cache position to local vector

            float elapsed = 0.0f; //little timer

            while (elapsed < duration)
            {
                //Make shake
                float x = transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;
                float y = transform.position.y + Random.Range(-0.1f, 0.1f) * magnitude;

                transform.position = new Vector3(x, y, originalPos.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = originalPos; //Return default position
        }

    }
}