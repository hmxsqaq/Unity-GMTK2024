using System;
using System.Linq;
using Hmxs_GMTK.Scripts.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Test
{
    public class Test : MonoBehaviour
    {
        public Camera renderCamera;
        public RenderTexture renderTexture;
        public LayerMask spriteLayerMask;
        public LayerMask maskLayerMask;

        [Button]
        private void CalculateOverlapArea()
        {
            renderCamera.targetTexture = renderTexture;
            renderCamera.cullingMask = spriteLayerMask | maskLayerMask;
            renderCamera.Render();

            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            Color[] pixels = texture.GetPixels();
            int overlapPixelCount = pixels.Count(pixel => pixel.a > 0.1f);

            float pixelArea = (1.0f / renderTexture.width) * (1.0f / renderTexture.height);
            float overlapArea = overlapPixelCount * pixelArea;

            Debug.Log($"Overlap Area: {overlapArea}");
        }
    }
}