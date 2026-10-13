using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private TMP_Text promptText;

    [SerializeField] private InputActionReference interactionActionRef;

    private void OnEnable()
    {
        UpdatePromptText();

        InputSystem.onActionChange += OnActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= OnActionChange;
    }
   
    public void UpdatePromptText()
    {
        if (interactionActionRef == null || interactionActionRef.action == null) return;
        
        string bindingName = interactionActionRef.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames);

        promptText.text = $"[{bindingName}]";
    }

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.BoundControlsChanged)
        {
            UpdatePromptText();
        }
    }
}
