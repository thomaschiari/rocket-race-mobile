using UnityEngine;

public class CheckChosenShip : MonoBehaviour
{
    void Start()
    {
        string selectedShip = PlayerPrefs.GetString("SelectedShip", "defaultShip");
        if (selectedShip != "defaultShip")
        {
            Debug.Log("Selected Ship: " + selectedShip);
        }
        else
        {
            Debug.Log("No ship has been selected or default is being used.");
        }
    }
}