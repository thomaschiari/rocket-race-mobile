using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseShipController : MonoBehaviour
{
    public void ChooseShip(string shipName)
    {
        if (IsShipEnabled(shipName))
        {
            PlayerPrefs.SetString("SelectedShip", shipName); // Save the chosen ship
            SceneManager.LoadScene("Home"); // Load the Home scene
        }
        else
        {
            Debug.Log("Ship is not enabled.");
        }
    }

    private bool IsShipEnabled(string shipName)
    {
        // Here you could check conditions to determine if the ship is enabled
        // For simplicity, this example will just return true
        return true;
    }
}