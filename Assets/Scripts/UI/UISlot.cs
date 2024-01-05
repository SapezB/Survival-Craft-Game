
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private Slot assignedSlot;

    private Button button;

    public Slot AssignedSlot => assignedSlot;

    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();

        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(Slot slot)
    {
        assignedSlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(Slot slot)
    {
        if(slot.ItemData != null) {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;
            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }

      
    }

    public void UpdateUISlot()
    {
        if (assignedSlot != null) UpdateUISlot(assignedSlot);
    }

    public void ClearSlot()
    {
        assignedSlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }

}
