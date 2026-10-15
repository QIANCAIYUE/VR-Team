using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabVisualFeedback : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.cyan;
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color activatedColor = Color.magenta;

    // ✅新增：把【门】拖到这个框
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;

    private XRGrabInteractable grabInteractable;
    private Renderer objectRenderer;
    private Material objectMaterial;

    private bool isOpening = false;
    private Quaternion startRot;
    private Quaternion endRot;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectRenderer = GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;
        objectMaterial.color = normalColor;

        // ✅初始化门的旋转
        if (doorTransform != null)
        {
            startRot = doorTransform.rotation;
            endRot = Quaternion.Euler(doorTransform.eulerAngles + new Vector3(0, openAngle, 0));
        }
    }

    private void OnEnable()
    {
        grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        grabInteractable.hoverExited.AddListener(OnHoverExited);
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
        grabInteractable.activated.AddListener(OnActivated);
        grabInteractable.deactivated.AddListener(OnDeactivated);
    }

    private void OnDisable()
    {
        grabInteractable.hoverEntered.RemoveListener(OnHoverEntered);
        grabInteractable.hoverExited.RemoveListener(OnHoverExited);
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
        grabInteractable.activated.RemoveListener(OnActivated);
        grabInteractable.deactivated.RemoveListener(OnDeactivated);
    }

    private void Update()
    {
        // ✅持续平滑旋转门
        if (isOpening && doorTransform != null)
        {
            doorTransform.rotation = Quaternion.Lerp(doorTransform.rotation, endRot, Time.deltaTime * openSpeed);
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (!grabInteractable.isSelected)
            objectMaterial.color = hoverColor;
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (!grabInteractable.isSelected)
            objectMaterial.color = normalColor;
    }

    // ✅【抓取把手瞬间触发开门】
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        objectMaterial.color = selectedColor;
        if (!isOpening && doorTransform != null)
        {
            isOpening = true;
            Debug.Log("🖐️抓取门把手，开始开门");
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        objectMaterial.color = grabInteractable.isHovered ? hoverColor : normalColor;
    }

    private void OnActivated(ActivateEventArgs args)
    {
        objectMaterial.color = activatedColor;
    }

    private void OnDeactivated(DeactivateEventArgs args)
    {
        objectMaterial.color = grabInteractable.isSelected
            ? selectedColor
            : (grabInteractable.isHovered ? hoverColor : normalColor);
    }
}