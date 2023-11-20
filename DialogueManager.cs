using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 用于显示对话文本的TextMeshProUGUI对象
    public Button[] choiceButtons; // 用于显示选项的按钮数组
    public TextAsset inkAsset; // Ink对话内容文件

    private Story story; // Ink故事实例

    public void Start()
    {
        story = new Story(inkAsset.text); // 创建Ink故事实例并加载Ink文件内容
        DisplayNextContent(); // 显示下一段文本和选项
        // 为每个按钮添加点击事件监听器
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int choiceIndex = i; // 保存选项索引
            choiceButtons[i].onClick.AddListener(() => OnButtonClick(choiceIndex));
        }
    }          

    // 在TextMeshPro对象中显示下一段文本和选项
    void DisplayNextContent()
    {
        dialogueText.text = ""; // 清空文本
        
        while (story.canContinue)
        {
            string nextLine = story.Continue(); // 获取下一行文本
            dialogueText.text += nextLine + "\n"; // 将文本追加到TextMeshPro对象中
        }

        if (story.currentChoices.Count > 0)
        {
            DisplayChoices(); // 如果有选项，则显示选项
        }
        else // 故事结束，隐藏所有选项按钮
        {
            foreach (Button button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
    
    // 显示选项按钮
    void DisplayChoices()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < story.currentChoices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true); // 显示有选项的按钮
                string choiceText = story.currentChoices[i].text; // 获取Ink选项文本
                TextMeshProUGUI buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>(); // 获取按钮内的TextMeshPro组件
                if (buttonText != null)
                {
                    buttonText.text = choiceText; // 设置按钮文本
                }
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false); // 禁用多余的按钮
            }
        }
    }
    
    // 处理按钮点击事件
    public void OnButtonClick(int choiceIndex)
    {
       
            story.ChooseChoiceIndex(choiceIndex); // 根据按钮选择执行相应的选项
            DisplayNextContent(); // 显示下一段文本和选项
            Debug.Log("Hello ");
    }
}