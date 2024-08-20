using System.Linq;
using Hmxs.Toolkit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Hmxs_GMTK.Scripts.Shape
{
    public class Photographer : SingletonMono<Photographer>
    {
        protected override void OnInstanceInit(Photographer instance) { }

        [Title("References")]
        [SerializeField] private Camera spriteCamera;
        [SerializeField] private Camera targetCamera;
        [SerializeField] private SpriteMask shapeResult;
        [SerializeField] private SpriteRenderer targetSprite;
        [SerializeField] private SpriteRenderer mapSprite;

        public SpriteRenderer TargetSprite => targetSprite;
        public SpriteMask ShapeResult => shapeResult;
        public SpriteRenderer MapSprite => mapSprite;

        [SerializeField] [ReadOnly] private float targetArea;
        [SerializeField] [ReadOnly] private float resultArea;

        [Button]
        public void GetTargetArea() => targetArea = CalculateTargetArea();

        [Button]
        public void GetResultArea()
        {
            GetShapeResult();
            resultArea = CalculateTargetArea();
            Debug.Log($"Target Area: {targetArea}, Result Area: {resultArea}");
        }

        [Button]
        public float GetResult() => 1 - resultArea / targetArea;

        private void GetShapeResult()
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(256, 256);
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, renderTexture.graphicsFormat, TextureCreationFlags.None);
            spriteCamera.targetTexture = renderTexture;

            RenderTexture.active = renderTexture;
            spriteCamera.Render();
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            Sprite resultSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            shapeResult.sprite = resultSprite;
        }

        private float CalculateTargetArea()
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(256, 256);
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, renderTexture.graphicsFormat, TextureCreationFlags.None);
            targetCamera.targetTexture = renderTexture;

            RenderTexture.active = renderTexture;
            targetCamera.Render();
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            Color[] pixels = texture.GetPixels();
            int pixelCount = pixels.Count(pixel => pixel.a > 0.1f);
            return pixelCount;
        }
    }
}