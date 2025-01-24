using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button crouchingButton;
    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        // Add both press and release listeners
        crouchingButton.onClick.AddListener(OnButtonUp);
        
        // Add pointer down event
        EventTrigger trigger = crouchingButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnButtonDown(); });
        trigger.triggers.Add(entry);
    }

    public void OnButtonDown()
    {
        playerController.EnableCrouchBool();
    }

    public void OnButtonUp()
    {
        playerController.DisableCrouchBool();
    }

    private void OnDestroy()
    {
        crouchingButton.onClick.RemoveListener(OnButtonUp);
    }
}
