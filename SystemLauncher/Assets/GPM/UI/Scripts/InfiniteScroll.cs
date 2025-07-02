namespace Gpm.Ui
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using System.Collections;

    public partial class InfiniteScroll : MonoBehaviour, IEndDragHandler
    {
        protected bool                          isInitialize            = false;

        protected RectTransform                 content                 = null;

        private bool                            changeValue             = false;

        [Header("Event", order = 4)]
        public ChangeValueEvent                 onChangeValue           = new ChangeValueEvent();
        public ItemActiveEvent                  onChangeActiveItem      = new ItemActiveEvent();
        public StateChangeEvent                 onStartLine             = new StateChangeEvent();
        public StateChangeEvent                 onEndLine               = new StateChangeEvent();

        private Predicate<InfiniteScrollData>   onFilter                = null;

        [Header("Snap", order = 5)]
        public bool snap = false;

        public int currentSnappedIndex { get; private set; } = 0;

        public Action<int> OnSnap;

        private void Awake()
        {
            Initialize();
        }

        protected void Initialize()
        {
            if (isInitialize == false)
            {
                scrollRect = GetComponent<ScrollRect>();
                content = scrollRect.content;
                viewport = scrollRect.viewport;

                CheckScrollAxis();
                ClearScrollContent();

                RectTransform itemTransform = (RectTransform)itemPrefab.transform;
                defaultItemPrefabSize = itemTransform.sizeDelta;

                itemObjectList.Clear();
                dataList.Clear();

                scrollRect.onValueChanged.AddListener(OnValueChanged);

                CreateNeedItem();

                CheckScrollData();

                isInitialize = true;

                needReBuildLayout = true;
            }
        }

        public void InsertData(InfiniteScrollData data, bool immediately = false)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            AddData(data);

            UpdateAllData(immediately);
        }

        public void InsertData(InfiniteScrollData data, int insertIndex, bool immediately = false)
        {
            if (insertIndex < 0 || insertIndex > dataList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (isInitialize == false)
            {
                Initialize();
            }

            InsertData(data, insertIndex);

            UpdateAllData(immediately);
        }

        public void InsertData(InfiniteScrollData[] datas, bool immediately = false)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            foreach (InfiniteScrollData data in datas)
            {
                AddData(data);
            }

            UpdateAllData(immediately);
        }
        public void InsertData(InfiniteScrollData[] datas, int insertIndex, bool immediately = false)
        {
            if (insertIndex < 0 || insertIndex > dataList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (isInitialize == false)
            {
                Initialize();
            }

            foreach (InfiniteScrollData data in datas)
            {
                InsertData(data, insertIndex++);
            }

            UpdateAllData(immediately);
        }

        public void RemoveData(InfiniteScrollData data, bool immediately = false)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            int dataIndex = GetDataIndex(data);

            RemoveData(dataIndex, immediately);
        }

        public void RemoveData(int dataIndex, bool immediately = false)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            if (IsValidDataIndex(dataIndex) == true)
            {
                selectDataIndex = -1;

                int removeShowIndex = -1;
                
                if(dataList[dataIndex].itemIndex != -1)
                {
                    removeShowIndex = dataList[dataIndex].itemIndex;
                }
                dataList[dataIndex].UnlinkItem(true);
                dataList.RemoveAt(dataIndex);
                for(int i= dataIndex; i< dataList.Count;i++)
                {
                    dataList[i].index--;

                    if(removeShowIndex != -1)
                    {
                        if (dataList[i].itemIndex != -1)
                        {
                            dataList[i].itemIndex--;
                        }
                    }
                }

                if (removeShowIndex != -1)
                {
                    if (removeShowIndex < firstItemIndex)
                    {
                        firstItemIndex--;
                    }
                    if (removeShowIndex < lastItemIndex)
                    {
                        lastItemIndex--;
                    }

                    itemCount--;
                }

                needReBuildLayout = true;

                UpdateAllData(immediately);
            }
        }

        public void ClearData(bool immediately = false)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            itemCount = 0;
            selectDataIndex = -1;

            dataList.Clear();
            lineLayout.Clear();
            layoutSize = 0;
            lineCount = 0;

            ClearItemsData();

            lastItemIndex = 0;
            firstItemIndex = 0;

            showLineIndex = 0;
            showLineCount = 0;

            isStartLine = false;
            isEndLine = false;

            needUpdateItemList = true;
            needReBuildLayout = true;
            isUpdateArea = true;

            onFilter = null;

            ClearScrollContent();

            cachedData.Clear();

            UpdateAllData(immediately);
        }

        public void Clear()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            itemCount = 0;
            selectDataIndex = -1;
            dataList.Clear();
            lineLayout.Clear();
            layoutSize = 0;
            lineCount = 0;

            ClearItems();

            lastItemIndex = 0;
            firstItemIndex = 0;

            showLineIndex = 0;
            showLineCount = 0;

            isStartLine = false;
            isEndLine = false;

            needUpdateItemList = true;
            needReBuildLayout = true;
            isUpdateArea = true;

            onFilter = null;

            cachedData.Clear();

            ClearScrollContent();
        }

        public void UpdateData(InfiniteScrollData data)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            var context = GetDataContext(data);
            if (context != null)
            {
                context.UpdateData(data);

                needReBuildLayout = true;
            }
        }

        public void UpdateAllData(bool immediately = true)
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            needReBuildLayout = true;
            isUpdateArea = true;

            CreateNeedItem();

            if (immediately == true)
            {
                UpdateShowItem(true);
            }
        }

        public void SetFilter(Predicate<InfiniteScrollData> onFilter)
        {
            this.onFilter = onFilter;
            needUpdateItemList = true;
        }

        public float GetViewportSize()
        {
            return layout.GetMainSize(viewport);
        }

        public float GetContentSize()
        {
            UpdateContentSize();

            return layout.GetMainSize(content);
        }

        public float GetContentPosition()
        {
            return layout.GetAxisPosition(content);
        }

        public void ResizeScrollView()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            UpdateContentSize();
        }
        
        public float GetItemPosition(int itemIndex)
        {
            float distance = GetItemDistance(itemIndex);

            return -layout.GetAxisPostionFromOffset(distance);
        }

        public void RefreshScroll()
        {
            if (isInitialize == false)
            {
                Initialize();
            }

            if (needUpdateItemList == true)
            {
                BuildItemList();

                needUpdateItemList = false;
            }
            if (NeedUpdateItem() == true)
            {
                UpdateShowItem();
            }
        }

        protected float GetCrossSize()
        {
            return layout.GetCrossSize(content.rect);
        }

        protected void ResizeContent()
        {
            cachedData.contentSize = GetItemTotalSize();
            content.sizeDelta = layout.GetAxisVector(-layout.padding, cachedData.contentSize);
        }

        protected void UpdateContentSize()
        {
            if (needReBuildLayout == true)
            {
                BuildLayout();
                needReBuildLayout = false;
            }
        }

        protected bool NeedUpdateItem()
        {
            CheckScrollData();

            if (needReBuildLayout == true ||
                isRebuildLayout == true ||
                isUpdateArea == true)
            {
                return true;
            }

            return false;
        }

        protected bool IsShowBeforePosition(float position, float contentPosition)
        {
            float viewPosition = position - contentPosition;
            if (viewPosition < 0)
            {
                return true;
            }

            return false;
        }

        protected bool IsShowAfterPosition(float position, float contentPosition, float viewportSize)
        {
            float viewPosition = position - contentPosition;
            if (viewPosition >= viewportSize)
            {
                return true;
            }

            return false;
        }

        private void Update()
        {
            if (isInitialize == true)
            {
                RefreshScroll();
            }
        }

        private void OnValidate()
        {
            layout.SetDefaults();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (snap)
            {
                StopScrolling();
                currentSnappedIndex = GetSnapIndex();
                MoveTo(currentSnappedIndex, MoveToType.MOVE_TO_CENTER, 0.3f);
                Invoke("ResumeScrolling", 0.3f);
            }
        }

        private int GetSnapIndex()
        {
            var snapIndex = 0;

            if(layout.IsVertical())
            {
                var axisValue = scrollRect.content.GetComponent<RectTransform>().localPosition.y;
                snapIndex = (int)(Math.Abs(axisValue) / itemPrefab.GetComponent<RectTransform>().sizeDelta.y);
                var offSet = Math.Abs(axisValue) % itemPrefab.GetComponent<RectTransform>().sizeDelta.y;
                if (offSet > itemPrefab.GetComponent<RectTransform>().sizeDelta.y / 2)
                {
                    snapIndex++;
                }
            }
            else
            {
                var axisValue = scrollRect.content.GetComponent<RectTransform>().localPosition.x;
                snapIndex = (int)(Math.Abs(axisValue) / itemPrefab.GetComponent<RectTransform>().sizeDelta.x);
                var offSet = Math.Abs(axisValue) % itemPrefab.GetComponent<RectTransform>().sizeDelta.x;
                if (offSet > itemPrefab.GetComponent<RectTransform>().sizeDelta.x / 2)
                {
                    snapIndex++;
                }
            }
            
            return snapIndex;
        }

        public void StopScrolling()
        {
            scrollRect.enabled = false;

            scrollRect.velocity = Vector2.zero;
        }

        // Method to resume scrolling
        public void ResumeScrolling()
        {
            OnSnap?.Invoke(currentSnappedIndex);

            scrollRect.enabled = true; 
        }

        [Serializable]
        public class ChangeValueEvent : UnityEvent<int, int, bool, bool>
        {
            public ChangeValueEvent()
            {
            }
        }

        [Serializable]
        public class ItemActiveEvent : UnityEvent<int, bool>
        {
            public ItemActiveEvent()
            {
            }
        }

        [Serializable]
        public class StateChangeEvent : UnityEvent<bool>
        {
            public StateChangeEvent()
            {
            }
        }
    }
}
