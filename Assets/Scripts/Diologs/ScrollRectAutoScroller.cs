using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollRectAutoScroller : MonoBehaviour
{
    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void ScrollToBottom()
    {
        StartCoroutine(CoScrollToBottom());
    }

    private IEnumerator CoScrollToBottom()
    {
        yield return null; // ждём 1 кадр, чтобы UI перестроился
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
