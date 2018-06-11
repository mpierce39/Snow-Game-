using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowShovelFillAmount : MonoBehaviour {

    public Paddle paddle;
    public Transform target;
    Image image;
    private void Awake()
    {
        target = GameObject.FindWithTag("Shovel").transform.GetChild(3);
    }

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    void Update()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(target.position);
        gameObject.transform.rotation = target.rotation;
    }
    public void UpdateFillAmount(float volume)
    {
        Debug.Log(volume);
        image.fillAmount = volume / 100;
    }
}

