using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the selected target's name and health bar.
/// </summary>
public class TargetUI : MonoBehaviour
{
    [Tooltip("Targeting component to read target changes from.")]
    public Targeting targeting;

    [Tooltip("UI text element showing the target name.")]
    public Text nameLabel;

    [Tooltip("Slider representing target HP.")]
    public Slider hpBar;

    private void Awake()
    {
        if (targeting != null)
        {
            targeting.OnTargetChanged += OnTargetChanged;
        }
        ClearUI();
    }

    /// <summary>
    /// Updates the UI when a new target is selected.
    /// </summary>
    /// <param name="obj">Selected target GameObject.</param>
    private void OnTargetChanged(GameObject obj)
    {
        if (obj == null)
        {
            ClearUI();
            return;
        }

        nameLabel.text = obj.name;
        hpBar.value = 1f; // Placeholder full health
        nameLabel.enabled = true;
        hpBar.gameObject.SetActive(true);
    }

    /// <summary>
    /// Clears the target UI.
    /// </summary>
    private void ClearUI()
    {
        nameLabel.text = string.Empty;
        hpBar.value = 0f;
        nameLabel.enabled = false;
        hpBar.gameObject.SetActive(false);
    }
}

