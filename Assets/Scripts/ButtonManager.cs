using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public Button fireButton;
    public EventTrigger leftButton;
    public EventTrigger rightButton;
    public EventTrigger upButton;
    public EventTrigger downButton;

    private IRocket currentRocket;

    void Start()
    {
        FindRocketAndConfigureButtons();
    }

    void Update()
    {
        if (currentRocket == null)
        {
            FindRocketAndConfigureButtons();
        }
    }

    private void FindRocketAndConfigureButtons()
    {
        MonoBehaviour[] allObjects = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in allObjects)
        {
            if (obj is IRocket)
            {
                currentRocket = (IRocket)obj;
                ConfigureButtons(currentRocket);
                break;
            }
        }
    }

    private void ConfigureButtons(IRocket rocket)
    {
        if (rocket is MonoBehaviour rocketController)
        {
            // Configure the fire button
            fireButton.onClick.RemoveAllListeners();
            fireButton.onClick.AddListener(() => rocketController.Invoke("Fire", 0));

            // Configure the direction buttons
            ConfigureButton(leftButton, "MoveLeft", "StopMovement", rocketController);
            ConfigureButton(rightButton, "MoveRight", "StopMovement", rocketController);
            ConfigureButton(upButton, "MoveUp", "StopMovement", rocketController);
            ConfigureButton(downButton, "MoveDown", "StopMovement", rocketController);
        }
    }

    private void ConfigureButton(EventTrigger button, string pointerDownMethod, string pointerUpMethod, MonoBehaviour rocketController)
    {
        button.triggers.Clear();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => rocketController.Invoke(pointerDownMethod, 0));
        button.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((eventData) => rocketController.Invoke(pointerUpMethod, 0));
        button.triggers.Add(pointerUpEntry);
    }
}
