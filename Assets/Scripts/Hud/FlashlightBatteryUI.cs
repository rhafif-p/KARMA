using Assets.Scripts.Hud;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightBatteryUI : NodeStyledProgressBar
{
    protected override int NodesAmount => 10;
    protected override void Start()
    {
        base.Start();
        GameController.Instance.Flashlight.OnFlashlightBatteryChanged += Flashlight_OnFlashlightBatteryChanged;
    }

    private void Flashlight_OnFlashlightBatteryChanged(float prevProgress, float newProgress)
    {
        SetProgress(newProgress);
    }
}

//public class FlashlightBatteryUI : MonoBehaviour
//{
//    private int NodeAmount = 10;
//    private GameObject[] Nodes;

//    private static int CalculateAmountOfNodes(float progress, int maxNodes)
//    {
//        progress = Math.Clamp(progress, 0f, 1f);
//        return (int)Mathf.Ceil(progress * maxNodes);
//    }
//    private void InitializeNodeList()
//    {
//        Nodes = new GameObject[NodeAmount];
//    }
//    private GameObject CreateNode(int index)
//    {
//        if (index < 0 || index >= NodeAmount) return null;

//        GameObject node = new($"Node{index}");
//        node.AddComponent<Image>();

//        node.transform.SetParent(this.transform, false);

//        RectTransform rectTransform = node.GetComponent<RectTransform>();
//        rectTransform.anchorMin = new Vector2(0, 0.5f);
//        rectTransform.anchorMax = new Vector2(0, 0.5f);
//        rectTransform.pivot = new Vector2(0.5f, 0.5f);

//        rectTransform.anchoredPosition = new Vector3(100 + index * 20, 0, 0);
//        rectTransform.sizeDelta = new Vector2(15, 20);

//        if (Nodes == null) InitializeNodeList();
//        Nodes[index] = node;

//        return node;
//    }
//    private void RemoveNode(int index)
//    {
//        if (index < 0 || index > NodeAmount) return;
//        if (Nodes[index] == null) return;

//        GameObject node = Nodes[index];
//        Destroy(node);

//        Nodes[index] = null;
//    }
//    private void InitializeNodes()
//    {
//        for (int i = 0; i < NodeAmount; i++) CreateNode(i);
//    }

//    private void OnProgressChanged(float prevProgress, float newProgress)
//    {
//        int nodesAmount = CalculateAmountOfNodes(newProgress, NodeAmount);

//        for (int i = 0; i < NodeAmount; i++)
//        {
//            if (i < nodesAmount && Nodes[i] == null) CreateNode(i);
//            if (i >= nodesAmount && Nodes[i] != null) RemoveNode(i);
//        }
//    }

//    private void Start()
//    {
//        InitializeNodes();
//        GameController.Instance.Flashlight.OnFlashlightBatteryChanged += OnProgressChanged;
//    }
//}
