using UnityEngine.UI;

namespace Utils
{
    public static class ImageUtils
    {
        public static void SetOpacity(Graphic image, float alpha)
        {
            var newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
        }
    }
}