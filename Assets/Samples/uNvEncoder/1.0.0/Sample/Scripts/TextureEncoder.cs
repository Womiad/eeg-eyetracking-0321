using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

namespace uNvEncoder.Examples
{
    public class TextureEncoder : MonoBehaviour
    {
        public Encoder encoder = new Encoder();
        public Texture texture = null;
        public EncoderDesc setting = new EncoderDesc
        {
            width = 1920,
            height = 1080,
            frameRate = 60,
            format = uNvEncoder.Format.R8G8B8A8_UNORM,
            bitRate = 8000000,
            maxFrameSize = 8000000 / 60,
        };
        public bool forceIdrFrame = true;

        void OnEnable()
        {
            Assert.IsNotNull(texture);
            setting.width = texture.width;
            setting.height = texture.height;

            // 动态设置帧率
            setting.frameRate = Application.targetFrameRate > 0 ? Application.targetFrameRate : 60;
            setting.maxFrameSize = setting.bitRate / setting.frameRate;

            encoder.Create(setting);
            StartCoroutine(EncodeLoop());
        }

        void OnDisable()
        {
            StopAllCoroutines();
            encoder.Destroy();
        }

        IEnumerator EncodeLoop()
        {
            for (;;)
            {
                yield return new WaitForEndOfFrame();
                Encode();
            }
        }

        void Encode()
        {
            if (!texture) return;

            encoder.Update();
            encoder.Encode(texture, forceIdrFrame);
        }
    }
}
