using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{    
        public static LoadAndSaveData instance;

        private void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
                return;
            }

            instance = this;
        }

    void Start()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();

        /*int currentHealth = PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = currentHealth;
        PlayerHealth.instance.healthBar.SetHealth(currentHealth);*/


        // Chargemenr des items
        string[] itemsSaved = PlayerPrefs.GetString("inventoryItems", "").Split(',');

        for (int i = 0; i < itemsSaved.Length; i++)
        {
            if(itemsSaved[i] != "")
            {
                // Ajouter l'item à l'inventaire
                int id = int.Parse(itemsSaved[i]);
                Item currentItem = ItemsDatabase.instance.allItems.Single(x => x.id == id);
                Inventory.instance.content.Add(currentItem);
            }
        }
        Inventory.instance.UpdateInventoryUI();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);

        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }

        // PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.currentHealth);


        // Sauvegarde des items
        string itemsInInventory = string.Join(",", Inventory.instance.content.Select(x => x.id));
        //Debug.Log("Les Items sauvés sont" + itemsInInventory);
        PlayerPrefs.SetString("inventoryItems", itemsInInventory);
    }
}
