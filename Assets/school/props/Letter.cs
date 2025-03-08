using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Letter : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text letterText;
    public float moveInterval = 0.0001f;
    public float moveSpeed = 500f;
    private Vector3 targetPosition;

    private float lyfeCycle = 1f;
    private float timer = 0f;

    void Start()
    {
        StartCoroutine(MoveTextAtIntervals());        
    }

    void Update()
    {
        letterText.transform.position = Vector3.Lerp(letterText.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        
        if(timer < lyfeCycle)
        {
            timer += Time.deltaTime;
            UnityEngine.Debug.Log($"Letter caught, count = {timer}");
        } else {
            CatchLetter.Instance.NotifyLetterDestroy(this);
        }
    }

    public void Initialize(int id, string letter, float lyfe){
        name = $"Letter {id}";
        letterText.text = letter;
        lyfeCycle = lyfe;
    }

    void MoveText()
    {
        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1000, 0, Camera.main.nearClipPlane)).x;

        float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1000, Camera.main.nearClipPlane)).y;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPosition = new Vector3(randomX, randomY, letterText.transform.position.z);
        
    }
    IEnumerator MoveTextAtIntervals()
    {
        while (true)
        {
            MoveText();

            yield return new WaitForSeconds(moveInterval);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CatchLetter.Instance.NotifyLetterCaught(this);
    }
}
