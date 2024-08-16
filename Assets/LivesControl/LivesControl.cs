using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivesControl : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private GameObject liveObjectPrefab;

    [SerializeField]
    private int liveSpacing = 20;
    [SerializeField]
    private int liveWidth = 100;
    [SerializeField]
    private int liveHeight = 100;

    private GameObject[] liveObject;

    public UnityEvent onGameOver;
    public UnityAction onGameOverEvent;

    private int currentLives = 0;

    void Start()
    {
        liveObject = new GameObject[lives];
        currentLives = lives;
        int livesContainerWidth = (lives*liveWidth) + ((lives-1)*liveSpacing);
        GetComponent<RectTransform>().sizeDelta = new Vector2(livesContainerWidth, liveHeight);

        for (int i=0; i<lives; i++){
            GameObject liveObjectInstance = Instantiate(liveObjectPrefab);
            liveObject[i] = liveObjectInstance;
            liveObject[i].transform.SetParent(transform, false);
        }
    }

    public void loseLive(){
        if(currentLives > 0) {
            currentLives -= 1;
            liveObject[currentLives].SetActive(false);
        }
        
        if(currentLives <= 0) {
            onGameOver.Invoke();
            onGameOverEvent.Invoke();
        }
    }

    public void reset(){
        currentLives = lives;

        for(int i=0; i<lives; i++){
            liveObject[i].SetActive(true);
        }
    }
}
