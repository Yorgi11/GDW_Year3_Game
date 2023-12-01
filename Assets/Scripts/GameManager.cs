using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton_template<GameManager>
{
    [SerializeField] private GameObject winLoseMenu;
    [SerializeField] private TextMeshProUGUI P1_Text;
    [SerializeField] private TextMeshProUGUI P2_Text;
    [SerializeField] private Player p2;
    public AudioSource audioSource;
    [SerializeField] private AudioClip Backmusic;
    float timer;
    float bgtime = 14.5f;

    private bool p1IsDead = false;
    private bool p2IsDead = false;
    private void Start()
    {
        audioSource.Play();
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (p1IsDead || p2IsDead)
        {
            P1_Text.text = p1IsDead ? "You Lose" : "You Win";
            P1_Text.color = p1IsDead ? Color.red : Color.green;
            P2_Text.text = p2IsDead ? "You Lose" : "You Win";
            P2_Text.color = p2IsDead ? Color.red : Color.green;

            winLoseMenu.SetActive(true);

            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, 1f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.L)) p2.TakeDamage(true, 100);

        timer += Time.deltaTime;
        if (timer > bgtime)
        {
            audioSource.Play();
            timer = 0;
        }
    }
    public void SetP(int i)
    {
        if (i == 0) p1IsDead = true;
        if (i == 1) p2IsDead = true;
    }
}
