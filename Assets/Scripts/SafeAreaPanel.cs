using UnityEngine;

// Этот скрипт автоматически подстраивает RectTransform под безопасную область экрана.
// Должен быть прикреплен к дочернему RectTransform внутри Canvas,
// который в свою очередь имеет Canvas Scaler с Scale With Screen Size.
public class SafeAreaPanel : MonoBehaviour
{
    private RectTransform panel;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0); // Хранит предыдущее значение Safe Area
    private Vector2 lastScreenSize = new Vector2(0, 0); // Хранит предыдущий размер экрана
    private ScreenOrientation lastOrientation = ScreenOrientation.Unknown; // Хранит предыдущую ориентацию

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        if (panel == null)
        {
            Debug.LogError("SafeAreaPanel: RectTransform component not found on this GameObject. Disabling script.");
            enabled = false;
            return;
        }

        // Вызываем обновление при старте
        RefreshSafeArea();
    }

    void Update()
    {
        // Проверяем изменения safeArea, размера экрана или ориентации
        // Это важно, так как safeArea может меняться при повороте или изменении системных настроек.
        if (Screen.safeArea != lastSafeArea ||
            (Vector2)new Vector2(Screen.width, Screen.height) != lastScreenSize ||
            Screen.orientation != lastOrientation)
        {
            RefreshSafeArea();
        }
    }

    void RefreshSafeArea()
    {
        lastSafeArea = Screen.safeArea;
        lastScreenSize = new Vector2(Screen.width, Screen.height);
        lastOrientation = Screen.orientation;

        // Получаем безопасную область в пикселях
        Rect safeArea = Screen.safeArea;

        // Преобразуем пиксельные координаты безопасной области
        // в нормализованные Viewport-координаты (от 0 до 1),
        // которые используются для якорей RectTransform.
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Применяем нормализованные якоря к RectTransform
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;

        // Сбрасываем отступы, чтобы элемент полностью заполнял якоря
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;

        Debug.LogFormat("SafeArea adjusted: Position={0}, Size={1}, AnchorMin={2}, AnchorMax={3}", safeArea.position, safeArea.size, anchorMin, anchorMax);
    }
}