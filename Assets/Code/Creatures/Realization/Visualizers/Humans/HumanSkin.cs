using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    [CreateAssetMenu(fileName = "HumanSkin", menuName = "Visual/Human Skin")]
    public class HumanSkin : ScriptableObject
    {
        public List<HeadSprite> HeadSprites;
        public List<EyeSprite> LeftEyeSprites;
        public List<EyeSprite> RightEyeSprites;

        [System.Serializable]
        public class HeadSprite
        {
            public Sprite sprite;
            public float width = 0f;
            public float height = 0f;
        }

        [System.Serializable]
        public class EyeSprite
        {
            public Sprite sprite;
            public float height = 0f;
            public byte mood = 0;
        }
    }
}