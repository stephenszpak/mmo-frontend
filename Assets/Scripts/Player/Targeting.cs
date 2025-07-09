using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Provides simple tab-target functionality for nearby enemies.
/// </summary>
public class Targeting : MonoBehaviour
{
    [Tooltip("Radius used to search for enemies when tabbing.")]
    public float targetRadius = 20f;

    [Tooltip("Material color applied to the current target.")]
    public Color highlightColor = Color.red;

    private List<GameObject> targets = new();
    private int currentIndex = -1;
    private GameObject currentTarget;
    private Color originalColor;

    private InputAction tabAction;

    /// <summary>
    /// Fired when the selected target changes.
    /// </summary>
    public System.Action<GameObject> OnTargetChanged;

    private void Awake()
    {
        var map = new InputActionMap("Targeting");
        tabAction = map.AddAction("Tab", binding: "<Keyboard>/tab");
        map.Enable();
    }

    private void Update()
    {
        if (tabAction.triggered)
        {
            AcquireTargets();
            CycleTarget();
        }
    }

    /// <summary>
    /// Finds all enemy objects within the target radius.
    /// </summary>
    private void AcquireTargets()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, targetRadius);
        targets.Clear();
        foreach (var col in cols)
        {
            if (col.CompareTag("Enemy"))
            {
                targets.Add(col.gameObject);
            }
        }

        targets.Sort((a, b) =>
            Vector3.Distance(transform.position, a.transform.position)
                .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
    }

    /// <summary>
    /// Selects the next target in the list.
    /// </summary>
    private void CycleTarget()
    {
        if (targets.Count == 0)
        {
            ClearTarget();
            return;
        }

        currentIndex = (currentIndex + 1) % targets.Count;
        SetTarget(targets[currentIndex]);
    }

    /// <summary>
    /// Applies highlight to the specified GameObject.
    /// </summary>
    /// <param name="target">Target to select.</param>
    private void SetTarget(GameObject target)
    {
        ClearTarget();
        currentTarget = target;
        Renderer r = currentTarget.GetComponentInChildren<Renderer>();
        if (r != null)
        {
            originalColor = r.material.color;
            r.material.color = highlightColor;
        }
        OnTargetChanged?.Invoke(currentTarget);
    }

    /// <summary>
    /// Removes highlight from the current target.
    /// </summary>
    private void ClearTarget()
    {
        if (currentTarget != null)
        {
            Renderer r = currentTarget.GetComponentInChildren<Renderer>();
            if (r != null)
            {
                r.material.color = originalColor;
            }
        }
        currentTarget = null;
        OnTargetChanged?.Invoke(null);
    }
}

