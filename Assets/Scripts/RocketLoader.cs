using UnityEngine;

public class RocketLoader : MonoBehaviour
{
    public GameObject classicRocketPrefab;
    public GameObject rocket2Prefab;
    public GameObject rocket3Prefab;
    public GameObject rocket4Prefab;

    void Start()
    {
        // Obter a escolha do foguete salva no PlayerPrefs
        string selectedShip = PlayerPrefs.GetString("SelectedShip", "RocketButton1");

        // Carregar o prefab correspondente
        GameObject rocketPrefab = GetRocketPrefab(selectedShip);
        if (rocketPrefab != null)
        {
            Instantiate(rocketPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Rocket prefab not found for selected ship: " + selectedShip);
        }
    }

    private GameObject GetRocketPrefab(string shipName)
    {
        switch (shipName)
        {
            case "RocketButton1":
                return classicRocketPrefab;
            case "RocketButton2":
                return rocket2Prefab;
            case "RocketButton3":
                return rocket3Prefab;
            case "RocketButton4":
                return rocket4Prefab;
            default:
                return null;
        }
    }
}
