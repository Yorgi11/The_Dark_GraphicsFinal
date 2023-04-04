using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHub : MonoBehaviour
{
    private int enemiesKilled = 0;

    private bool disableMouse = false;

    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject[] clues;
    [SerializeField] private GameObject[] icons;
    [SerializeField] private Transform[] spaces;

    private GameObject[] collectedClues;

    private int spaceIndex = 0;
    private int numClues = 0;
    private void Start()
    {
        collectedClues = new GameObject[clues.Length];
    }
    private void Update()
    {
        if (disableMouse && Input.GetKeyDown(KeyCode.Escape)) disableMouse = false;
        if (disableMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab)) journal.SetActive(journal.activeInHierarchy);
    }

    public void SetJournal(GameObject clue)
    {
        for (int i = 0; i < collectedClues.Length; i++)
        {
            if (collectedClues[i] == null) collectedClues[i] = clue;
            if (collectedClues[i] == null && collectedClues[i + 1] == null && i + 1 < collectedClues.Length - 1) break;
        }
        for (int i = 0; i < clues.Length; i++)
        {
            if (spaceIndex < spaces.Length)
            {
                if (collectedClues[i].name.Equals(clues[i].name)) GameObject.Instantiate(icons[i], spaces[spaceIndex].position, Quaternion.identity, spaces[spaceIndex++]);
            }
            else Debug.LogWarning("spaces are full. spaceIndex >= spaces.length -- #MainHub 57");
        }
    }

    public GameObject currentClue
    {
        set { 
            collectedClues[numClues] = value;
            numClues++;
        }
    }

    public bool DisableMouse
    {
        get { return disableMouse; }
        set { disableMouse = value; }
    }
    public int EnemiesKilled
    {
        get { return enemiesKilled; }
        set { enemiesKilled = value; }
    }
}
