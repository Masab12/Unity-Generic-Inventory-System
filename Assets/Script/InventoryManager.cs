using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SingletonBehaviour<InventoryManager>
{

    public GameObject itemPrefab;
    public ScrollRect scrollRect;

    public Text ItemName;
    public Image ItemImage;
    public Text Bonus;
    public Text color;
    public Text slot;
    public Text yetiType;

    public GameObject[] OptionListGO;
    public GameObject[] OptionBtnGO;

    

    Inventory[] inventoryItems; // Load from Scriptable Object
    Inventory selectedInv;

    List<Inventory> FullItemList; // Load from Database or dynamic Create
    Buttonbtn activeButton;
    // no need if no improvement
    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        inventoryItems = Resources.LoadAll<Inventory>("ItemObjects");
        FullItemList = ItemObtainFromDatabase();
        CreateItem();
        if (FullItemList.Count > 0) { ShowItemInfo(FullItemList[0]); }
        ItemInfoSetup();
        activeButton = OptionBtnGO[0].GetComponent<Buttonbtn>();
        activeButton.Click();
    }

    private List<Inventory> ItemObtainFromDatabase()
    {
        List<Inventory> IVlst = new List<Inventory>();
        // just test obtain from database 
        // database might need to add how to obtain from items
        // then update
        IVlst.Add(new Inventory(1, 4));
        
        return IVlst;
    }

    private void CreateItem()
    {
        foreach (GameObject ele in OptionListGO)
        {
            ele.SetActive(true);
        }
        foreach (Inventory ele in FullItemList)
        {
            if (ele.GetInventoryCount() > 0)
            {
                ele.SetInventoryItem(InvFind(ele.ID));
                GameObject itemGO = Instantiate(itemPrefab) as GameObject;
                itemGO.transform.SetParent(GameObject.Find(ele.slot).transform, false);// set item to the related parent 
                BindtoItems(itemGO, ele);
            }
        }
        foreach (Inventory ele in FullItemList)
        { // to show all
            if (ele.GetInventoryCount() > 0)
            {
                ele.SetInventoryItem(InvFind(ele.ID));
                GameObject itemGO = Instantiate(itemPrefab) as GameObject;
                itemGO.transform.SetParent(GameObject.Find("OptionList5").transform, false);
                BindtoItems(itemGO, ele);
            }
        }
        foreach (GameObject ele in OptionListGO)
        {
            ele.SetActive(false);
        }
        foreach (GameObject ele in OptionBtnGO)
        {
            ele.GetComponent<Buttonbtn>().ReClicked();
        }
    }

    void DeleteAllItem()
    {
        string[] OptionList = { "OptionList1", "OptionList2", "OptionList3", "OptionList4", "OptionList5" };
        for (int i = 0; i < OptionList.Length; i++)
        {
            GameObject gt = OptionListGO[i];
            for (int j = 0; j < gt.transform.childCount; j++)
            {
                Destroy(gt.transform.GetChild(j).gameObject);
            }
        }
    }

    Inventory InvFind(int id)
    { // this id get from db
        int i;
        for (i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].ID == id)
            {
                break;
            }
        }
        return inventoryItems[i];
    }

    void BindtoItems(GameObject item, Inventory inv)
    {
        item.GetComponent<InvItem>().BindView(inv);
    }

    void ItemInfoSetup()
    {
        if (FullItemList.Count > 0) { ShowItemInfo(FullItemList[0]); } else { HideItemInfo(); }
    }
    // public called by single items
    public void ShowItemInfo(Inventory inv)
    {
        selectedInv = inv;
       
        ItemName.text = selectedInv.Name;
        ItemImage.sprite = selectedInv.Image;
        Bonus.text = "Bonus: " + selectedInv.GetInventoryCount().ToString();
        
    }
    void HideItemInfo()
    {
        selectedInv = null;
       
        ItemName.text = "No Item Selected";

       
        ItemImage.sprite = null;
        
    }
    
    public void ChangeCategory(GameObject button)
    {
        Buttonbtn highlightableButton = button.GetComponent<Buttonbtn>();
        scrollRect.content = highlightableButton.optionList.GetComponent<RectTransform>();
        highlightableButton.Click();
        activeButton.Click();
        activeButton = highlightableButton;
    }

    // UI buttons action events




}