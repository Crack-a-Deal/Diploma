using UnityEngine.UIElements;

public static class UIExtentions
{
    public static void Display(this VisualElement elemet, bool enabled)
    {
        if (elemet == null) return;
        elemet.style.display = enabled ? DisplayStyle.Flex : DisplayStyle.None;
    }
}