using UnityEngine;
using System.Collections;


public class DropInSpawn : MonoBehaviour
{
    public enum SpawnType { WorldObject, UI }
    [SerializeField] SpawnType spawnType;

    [SerializeField] float spawnDuration = 1f;
    [SerializeField] float startYOffset = 5f;

    private Vector3 worldTarget;
    private RectTransform rect;
    private Vector2 uiTarget;

    private void Start()
    {
        if (spawnType == SpawnType.WorldObject)
        {
            worldTarget = transform.position;
            transform.position = worldTarget + Vector3.up * startYOffset;
            StartCoroutine(WorldSpawn());
        }
        else
        {
            rect = GetComponent<RectTransform>();
            uiTarget = rect.anchoredPosition;
            rect.anchoredPosition = uiTarget + Vector2.up * startYOffset;
            StartCoroutine(UISpawn());
        }
    }

    private IEnumerator WorldSpawn()
    {
        float t = 0f;
        Vector3 start = transform.position;

        while (t < spawnDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / spawnDuration);

            transform.position = Vector3.Lerp(start, worldTarget, k);
            yield return null;
        }

        transform.position = worldTarget;
    }

    private IEnumerator UISpawn()
    {
        float t = 0f;
        Vector2 start = rect.anchoredPosition;

        while (t < spawnDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / spawnDuration);

            rect.anchoredPosition = Vector2.Lerp(start, uiTarget, k);
            yield return null;
        }

        rect.anchoredPosition = uiTarget;
    }
}
