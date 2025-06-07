using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryTextBoxUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshTop1;
    [SerializeField] TextMeshProUGUI textMeshTop2;
    [SerializeField] TextMeshProUGUI textMeshTop3;
    [SerializeField] TextMeshProUGUI textMeshBottom1;
    [SerializeField] TextMeshProUGUI textMeshBottom2;
    [SerializeField] TextMeshProUGUI textMeshBottom3;



    //set text values.
    public void SetTextboxText(string textTop1, string textTop2, string textTop3, string textBottom1, string textBottom2, string textBottom3)
    {
        textMeshTop1.text = textTop1;
        textMeshTop2.text = textTop2;
        textMeshTop3.text = textTop3;
        textMeshBottom1.text = textBottom1;
        textMeshBottom2.text = textBottom2;
        textMeshBottom3.text = textBottom3;
        
    }




}
