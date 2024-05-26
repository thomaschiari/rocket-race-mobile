using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChooseShipController : MonoBehaviour
{
    public Button[] shipButtons; // Referências para os botões das naves
    public TextMeshProUGUI mineralCountText; // Texto para exibir o contador de minerais
    public Button watchAdButton; // Botão para assistir ao anúncio

    private void Start()
    {
        PlayerPrefs.SetInt("AdWatched", 0); // Reiniciar o contador de anúncios assistidos

        // Inicializar o texto do contador de minerais
        UpdateMineralCountText();

        // Desativar todos os botões de naves, exceto o da nave clássica
        foreach (Button button in shipButtons)
        {
            string shipName = button.name; // Supondo que o nome do botão é o mesmo que o nome da nave
            if (shipName != "RocketButton1" && !IsShipEnabled(shipName))
            {
                button.interactable = false; // Desativar o botão
            }

            // Adicionar listener aos botões de nave
            button.onClick.AddListener(() => ChooseShip(shipName));
        }

        // Adicionar listener ao botão de assistir ao anúncio
        watchAdButton.onClick.AddListener(() => ShowAdAndRewardMinerals());

        // Inscrever-se no evento de anúncio assistido
        RewardedAdsButton.OnAdWatched += OnAdWatched;
    }

    private void Update()
    {
        // Atualizar o texto do contador de minerais
        UpdateMineralCountText();
        UpdateAdButtonState();

        // Verificar se o jogador tem minerais suficientes para desbloquear naves
        int minerals = PlayerPrefs.GetInt("MineralCount", 0);
        foreach (Button button in shipButtons)
        {
            string shipName = button.name;
            if (!IsShipEnabled(shipName) && minerals >= GetRequiredMineralsForShip(shipName))
            {
                button.interactable = true; // Ativar o botão se o jogador tiver minerais suficientes
            }
        }
    }

    private void OnDestroy()
    {
        // Desinscrever-se do evento de anúncio assistido
        RewardedAdsButton.OnAdWatched -= OnAdWatched;
    }

    public void ChooseShip(string shipName)
    {
        int requiredMinerals = GetRequiredMineralsForShip(shipName);

        if (IsShipEnabled(shipName))
        {
            PlayerPrefs.SetString("SelectedShip", shipName); // Salvar a nave escolhida
            SceneManager.LoadScene("Home"); // Carregar a cena Home
        }
        else if (PlayerPrefs.GetInt("MineralCount", 0) >= requiredMinerals)
        {
            // Deduzir o número de minerais necessários do contador
            int minerals = PlayerPrefs.GetInt("MineralCount", 0);
            PlayerPrefs.SetInt("MineralCount", minerals - requiredMinerals);
            PlayerPrefs.SetInt(shipName + "_enabled", 1); // Habilitar a nave
            PlayerPrefs.Save();

            // Atualizar o texto do contador de minerais
            UpdateMineralCountText();

            // Ativar o botão da nave desbloqueada
            foreach (Button button in shipButtons)
            {
                if (button.name == shipName)
                {
                    button.interactable = true; // Ativar o botão
                }
            }
        }
        else
        {
            Debug.Log("Not enough minerals to unlock this ship.");
        }
    }

    private bool IsShipEnabled(string shipName)
    {
        // Verificar se a nave está habilitada
        return PlayerPrefs.GetInt(shipName + "_enabled", 0) == 1;
    }

    private int GetRequiredMineralsForShip(string shipName)
    {
        // Defina o número necessário de minerais para cada nave
        switch (shipName)
        {
            case "RocketButton2":
                return 90;
            case "RocketButton3":
                return 150;
            case "RocketButton4":
                return 125;
            default:
                return 0;
        }
    }

    private void ShowAdAndRewardMinerals()
    {
        // Implementar a lógica para assistir ao anúncio
        RewardedAdsButton adButton = FindObjectOfType<RewardedAdsButton>();
        if (adButton != null)
        {
            adButton.ShowAd();
        }
    }

    private void OnAdWatched()
    {
        // Atualizar a visibilidade do botão de assistir ao anúncio
        watchAdButton.interactable = false;
        // Atualizar texto do botão de assistir ao anúncio
        watchAdButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ad watched!";

        // Verificar os botões de nave e ativar os que podem ser comprados
        foreach (Button button in shipButtons)
        {
            string shipName = button.name;
            if (PlayerPrefs.GetInt(shipName + "_enabled", 0) == 1)
            {
                button.interactable = true;
            }
        }

        PlayerPrefs.SetInt("AdWatched", 1); // Marcar que o anúncio foi assistido
        // Atualizar o texto do contador de minerais
        UpdateMineralCountText();
    }

    private void UpdateMineralCountText()
    {
        int minerals = PlayerPrefs.GetInt("MineralCount", 0);
        mineralCountText.text = "Minerals: " + minerals;
    }

    private void UpdateAdButtonState()
    {
        if (PlayerPrefs.GetInt("AdWatched", 0) == 1)
        {
            watchAdButton.interactable = false;
            watchAdButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ad watched!";
        }
    }
}
