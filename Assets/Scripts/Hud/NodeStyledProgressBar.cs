using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Hud
{
    public abstract class NodeStyledProgressBar : MonoBehaviour
    {
        private float _progress = 0f;
        public float Progress { get { return _progress; } private set { SetProgress(value); } }
        [SerializeField]
        protected abstract int NodesAmount { get; }
        private GameObject[] Nodes;

        private static int CalculateAmountOfNodes(float progress, int maxNodes)
        {
            progress = Math.Clamp(progress, 0f, 1f);
            return (int)Mathf.Ceil(progress * maxNodes);
        }
        
        private GameObject CreateNode(int index)
        {
            if (index < 0 || index >= NodesAmount || Nodes == null) return null;

            GameObject node = new($"Node{index}");
            node.AddComponent<Image>();

            node.transform.SetParent(this.transform, false);

            RectTransform rectTransform = node.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0.5f);
            rectTransform.anchorMax = new Vector2(0, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            rectTransform.anchoredPosition = new Vector3(100 + index * 20, 0, 0);
            rectTransform.sizeDelta = new Vector2(15, 20);
            
            Nodes[index] = node;

            return node;
        }
        private void RemoveNode(int index)
        {
            if (index < 0 || index > NodesAmount) return;
            if (Nodes[index] == null) return;

            GameObject node = Nodes[index];
            Destroy(node);

            Nodes[index] = null;
        }
        private void InitializeNodeList()
        {
            if (Nodes != null) foreach (var node in Nodes) Destroy(node);
            Nodes = new GameObject[NodesAmount];
        }
        private void InitializeNodes()
        {
            for (int i = 0; i < NodesAmount; i++) CreateNode(i);
        }
        private void RenderProgress()
        {
            if (Nodes == null || Nodes.Length != NodesAmount) InitializeNodeList();
            int nodesAmount = CalculateAmountOfNodes(_progress, NodesAmount);

            for (int i = 0; i < NodesAmount; i++)
            {
                if (i < nodesAmount && Nodes[i] == null) CreateNode(i);
                if (i >= nodesAmount && Nodes[i] != null) RemoveNode(i);
            }
        }
        public void SetProgress(float progress)
        {
            _progress = Math.Clamp(progress, 0f, 1f);
            RenderProgress();
        }
        protected virtual void Start()
        {
            InitializeNodeList();
            InitializeNodes();
        }
    }
}
