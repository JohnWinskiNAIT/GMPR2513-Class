using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] GameObject dialogPanel;
    [SerializeField] TextMeshProUGUI dialogText;
    // Start is called before the first frame update
    void Start()
    {
        MyEvents.StartDialog.AddListener(OpenPanel);
        MyEvents.EndDialog.AddListener(ClosePanel);
        MyEvents.UpdateDialog.AddListener(UpdatePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenPanel()
    {
        dialogPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        dialogPanel.SetActive(false);
    }

    void UpdatePanel(string text)
    {
        dialogText.text = text;
    }
}
