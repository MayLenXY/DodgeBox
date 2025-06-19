using UnityEngine;

// ���� ������ ������������� ������������ RectTransform ��� ���������� ������� ������.
// ������ ���� ���������� � ��������� RectTransform ������ Canvas,
// ������� � ���� ������� ����� Canvas Scaler � Scale With Screen Size.
public class SafeAreaPanel : MonoBehaviour
{
    private RectTransform panel;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0); // ������ ���������� �������� Safe Area
    private Vector2 lastScreenSize = new Vector2(0, 0); // ������ ���������� ������ ������
    private ScreenOrientation lastOrientation = ScreenOrientation.Unknown; // ������ ���������� ����������

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        if (panel == null)
        {
            Debug.LogError("SafeAreaPanel: RectTransform component not found on this GameObject. Disabling script.");
            enabled = false;
            return;
        }

        // �������� ���������� ��� ������
        RefreshSafeArea();
    }

    void Update()
    {
        // ��������� ��������� safeArea, ������� ������ ��� ����������
        // ��� �����, ��� ��� safeArea ����� �������� ��� �������� ��� ��������� ��������� ��������.
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

        // �������� ���������� ������� � ��������
        Rect safeArea = Screen.safeArea;

        // ����������� ���������� ���������� ���������� �������
        // � ��������������� Viewport-���������� (�� 0 �� 1),
        // ������� ������������ ��� ������ RectTransform.
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // ��������� ��������������� ����� � RectTransform
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;

        // ���������� �������, ����� ������� ��������� �������� �����
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;

        Debug.LogFormat("SafeArea adjusted: Position={0}, Size={1}, AnchorMin={2}, AnchorMax={3}", safeArea.position, safeArea.size, anchorMin, anchorMax);
    }
}