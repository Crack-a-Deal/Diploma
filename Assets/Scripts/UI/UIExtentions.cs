using UnityEngine.UIElements;
using UnityEngine;

public static class UIExtentions
{
    public static void Display(this VisualElement elemet, bool enabled)
    {
        if (elemet == null) return;
        elemet.style.display = enabled ? DisplayStyle.Flex : DisplayStyle.None;
    }
    public static void Open(this VisualElement elemet, VisualElement newElement)
    {
        Display(elemet, false);
        Display(newElement, true);
    }
}