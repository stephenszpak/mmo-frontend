using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/// <summary>
/// Creates placeholder objects for the TestArena scene at runtime.
/// </summary>
public static class SceneBootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void CreateSceneObjects()
    {
        // Ground plane
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";

        // Directional light
        GameObject lightObj = new GameObject("Directional Light");
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        // Player capsule
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0f, 1f, 0f);
        var controller = player.AddComponent<CharacterController>();
        player.AddComponent<PlayerController>();
        var targeting = player.AddComponent<Targeting>();
        player.AddComponent<DummyNetworkManager>();

        // Camera root and Cinemachine virtual camera
        GameObject cameraRoot = new GameObject("CameraRoot");
        cameraRoot.transform.SetParent(player.transform);
        cameraRoot.transform.localPosition = Vector3.zero;
        var camObj = new GameObject("Main Camera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.tag = "MainCamera";
        camObj.transform.SetParent(cameraRoot.transform);
        camObj.transform.localPosition = new Vector3(0f, 2f, -4f);
        camObj.transform.localRotation = Quaternion.identity;
        var vcam = camObj.AddComponent<CinemachineVirtualCamera>();
        vcam.Follow = cameraRoot.transform;

        var pc = player.GetComponent<PlayerController>();
        pc.cameraRoot = cameraRoot.transform;
        pc.followCamera = vcam;

        // NPC target
        GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        npc.name = "Enemy";
        npc.tag = "Enemy";
        npc.transform.position = new Vector3(2f, 1f, 5f);

        // UI setup
        GameObject canvasObj = new GameObject("Canvas");
        var canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        GameObject panel = new GameObject("TargetUI");
        panel.transform.SetParent(canvasObj.transform);
        var panelRect = panel.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.5f, 1f);
        panelRect.anchorMax = new Vector2(0.5f, 1f);
        panelRect.anchoredPosition = new Vector2(0f, -40f);
        panelRect.sizeDelta = new Vector2(200f, 40f);

        GameObject textObj = new GameObject("Name");
        textObj.transform.SetParent(panel.transform);
        var nameText = textObj.AddComponent<Text>();
        nameText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        nameText.alignment = TextAnchor.MiddleCenter;
        var nameRect = nameText.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0f, 0.5f);
        nameRect.anchorMax = new Vector2(1f, 1f);
        nameRect.offsetMin = Vector2.zero;
        nameRect.offsetMax = Vector2.zero;

        GameObject sliderObj = new GameObject("HP");
        sliderObj.transform.SetParent(panel.transform);
        var slider = sliderObj.AddComponent<Slider>();
        var sliderRect = slider.GetComponent<RectTransform>();
        sliderRect.anchorMin = new Vector2(0f, 0f);
        sliderRect.anchorMax = new Vector2(1f, 0.5f);
        sliderRect.offsetMin = Vector2.zero;
        sliderRect.offsetMax = Vector2.zero;

        var targetUI = panel.AddComponent<TargetUI>();
        targetUI.targeting = targeting;
        targetUI.nameLabel = nameText;
        targetUI.hpBar = slider;
    }
}

