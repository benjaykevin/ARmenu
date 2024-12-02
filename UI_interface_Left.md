using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeModel : MonoBehaviour
{
    public ModelsManager modelsManager; // 將類型從 GameObject 改為 ModelsManager 類型
    public Sprite[] SelectedSprites;
    public Sprite[] NormalSprites;
    public Image[] SliderImages;
    public Sprite defaultNormalSprite; // 預設的 NormalSprite，當 NormalSprites 中的元素不足時使用


    // 選擇模型
    public void SelectModel(int index)
    {
        if (modelsManager != null)
        {
            modelsManager.SetModel(index); // 切換到指定的模型
        }
    }

    // 取消選中所有圖像
    void DeselectAll()
    {
        for (int i = 0; i < SliderImages.Length; i++)
        {
            // 檢查 NormalSprites 是否有足夠的元素，並且元素不是 null
            if (i < NormalSprites.Length && NormalSprites[i] != null)
            {
                SliderImages[i].sprite = NormalSprites[i];
            }
            else
            {
                // 如果 NormalSprites 不足或為 null，則使用預設的 NormalSprite
                if (SliderImages[i] != null && defaultNormalSprite != null)
                {
                    SliderImages[i].sprite = defaultNormalSprite;
                }
            }
        }
    }
}