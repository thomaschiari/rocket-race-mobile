using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChooseShipController : MonoBehaviour
{
    public Button[] shipButtons; // Referências para os botões das naves
    public TextMeshProUGUI mineralCountText; // Texto para exibir o contador de minerais
    public Button watchAdButton; // Botão para assistir ao anúncio
    public TextMeshProUGUI watchAdButtonText; // Texto para exibir no botão de assistir ao anúncio

    private void Start()
    {
        PlayerPrefs.SetInt("AdWatched", 0); // Definir o valor inicial para o anúncio assistido (0 = não assistido)
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
        watchAdButton.onClick.AddListener(() => ShowAd());

        // Inscrever-se no evento de anúncio assistido
        AdManager.OnAdWatched += OnAdWatched;
        // Inscrever-se no evento de anúncio carregado
        AdManager.OnAdLoaded += OnAdLoaded;

        // Inicializar o estado do botão de anúncio
        UpdateAdButtonState();
    }

    private void Update()
    {
        // Atualizar o texto do contador de minerais
        UpdateMineralCountText();

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

        UpdateAdButtonState();
    }

    private void OnDestroy()
    {
        // Desinscrever-se dos eventos de anúncio assistido e carregado
        AdManager.OnAdWatched -= OnAdWatched;
        AdManager.OnAdLoaded -= OnAdLoaded;
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

    private void ShowAd()
    {
        // Chamar o AdManager para mostrar o anúncio
        AdManager adManager = FindObjectOfType<AdManager>();
        if (adManager != null)
        {
            adManager.ShowAd();
        }
    }

    private void OnAdWatched()
    {
        // Aumentar a quantidade de minerais após assistir ao anúncio
        int minerals = PlayerPrefs.GetInt("MineralCount", 0);
        PlayerPrefs.SetInt("MineralCount", minerals + 10); // Recompensa de 10 minerais por anúncio assistido
        PlayerPrefs.Save();

        // Atualizar a visibilidade do botão de assistir ao anúncio
        watchAdButton.interactable = false;
        // Atualizar texto do botão de assistir ao anúncio
        watchAdButtonText.text = "Ad watched!";

        // Verificar os botões de nave e ativar os que podem ser comprados
        foreach (Button button in shipButtons)
        {
            string shipName = button.name;
            if (PlayerPrefs.GetInt(shipName + "_enabled", 0) == 1)
            {
                button.interactable = true;
            }
        }

        PlayerPrefs.SetInt("AdWatched", 1); // Definir o anúncio assistido como verdadeiro

        // Atualizar o texto do contador de minerais
        UpdateMineralCountText();
        UpdateAdButtonState();
    }

    private void UpdateMineralCountText()
    {
        int minerals = PlayerPrefs.GetInt("MineralCount", 0);
        mineralCountText.text = "Minerals: " + minerals;
    }

    private void OnAdLoaded()
    {
        // Habilitar o botão de assistir ao anúncio quando um anúncio for carregado
        watchAdButton.interactable = true;
        watchAdButtonText.text = "Watch Ad :)";
    }

    private void UpdateAdButtonState()
    {
        if (PlayerPrefs.GetInt("AdWatched", 0) == 1)
        {
            watchAdButton.interactable = false;
            watchAdButtonText.text = "Ad watched!";
        }
        else
        {
            AdManager adManager = FindObjectOfType<AdManager>();
            if (adManager != null && adManager.IsAdLoaded())
            {
                watchAdButton.interactable = true;
                watchAdButtonText.text = "Watch Ad :)";
            }
            else
            {
                watchAdButton.interactable = false;
                watchAdButtonText.text = "No Ads Available";
            }
        }
    }
}
