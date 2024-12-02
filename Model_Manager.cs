using System.Collections;
using UnityEngine;
using Vuforia;

public class ModelsManager : MonoBehaviour
{
    public GameObject[] Models; // 要切換的模型列表
    private int currentIndex = -1; // 當前顯示的模型索引

    void Start()
    {
        // 禁用所有模型
        DisableAll();

        // 顯示第一個模型
        ShowFirstModel();

    }

    void DisableAll()
    {
        foreach (var model in Models)
        {
            model.SetActive(false); // 禁用所有模型
        }
    }

    void ShowFirstModel()
    {
        if (Models.Length > 0)
        {
            SetModel(0); // 顯示第一個模型
        }
        else
        {
            Debug.LogWarning("Models 陣列中沒有模型。");
        }
    }


    public void SetModel(int index)
    {
        // 確保索引在範圍內且不等於當前索引
        if (index < 0 || index >= Models.Length || index == currentIndex) return;

        // 禁用當前顯示的模型
        if (currentIndex >= 0 && currentIndex < Models.Length)
        {
            Models[currentIndex].SetActive(false);
        }

        // 更新當前索引並啟用新的模型
        currentIndex = index;
        Models[currentIndex].SetActive(true);

        // 開始檢查新模型是否被追蹤到
        StartCoroutine(EnsureModelTracked(Models[currentIndex]));
    }

    private IEnumerator EnsureModelTracked(GameObject model)
    {
        ObserverBehaviour observer = model.GetComponentInChildren<ObserverBehaviour>();
        if (observer == null)
        {
            Debug.LogWarning("模型缺少 ObserverBehaviour：" + model.name);
            yield break;
        }

        // 持續檢查直到模型被追蹤到
        while (observer.TargetStatus.Status != Status.TRACKED &&
               observer.TargetStatus.Status != Status.EXTENDED_TRACKED)
        {
            yield return new WaitForSeconds(0.5f); // 每0.5秒檢查一次追蹤狀態
            model.SetActive(false); // 重新啟用模型以強制刷新追蹤
            yield return null;
            model.SetActive(true);
        }

        Debug.Log("模型已成功追蹤：" + model.name);
    }

}   