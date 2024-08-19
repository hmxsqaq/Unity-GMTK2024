using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Hmxs_GMTK.Scripts.Test
{
    public class Test : MonoBehaviour
    {
        public Camera renderCamera;
        public SpriteRenderer spriteRenderer;

        // [Button]
        // private void CalculateOverlapArea()
        // {
        //
        //     renderCamera.Render();
        //
        //     Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
        //     RenderTexture.active = renderTexture;
        //     texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        //     texture.Apply();
        //
        //     Color[] pixels = texture.GetPixels();
        //     int overlapPixelCount = pixels.Count(pixel => pixel.a > 0.1f);
        //
        //     float pixelArea = (1.0f / renderTexture.width) * (1.0f / renderTexture.height);
        //     float overlapArea = overlapPixelCount * pixelArea;
        //
        //     Debug.Log($"Overlap Area: {overlapArea}");
        // }

        [Button]
        private void TestArea()
        {
            RenderTexture renderTexture = renderCamera.targetTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, renderTexture.graphicsFormat, TextureCreationFlags.None);
            RenderTexture.active = renderTexture;
            renderCamera.Render();
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            spriteRenderer.sprite = sprite;
        }
    }
}