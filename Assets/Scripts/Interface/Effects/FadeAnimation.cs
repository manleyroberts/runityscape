﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour {
    [SerializeField]
    private Image image;
    [SerializeField]
    float fadeDuration;

    void Start() {
        StartCoroutine(FadeEffect());
    }

    IEnumerator FadeEffect() {
        float fadePerTick = (1.0f * Time.deltaTime) / fadeDuration;
        while (image != null && !image.IsDestroyed() && image.color.a > 0) {
            Util.SetImageAlpha(image, image.color.a - fadePerTick);
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }

}
