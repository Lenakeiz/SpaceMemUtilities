using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMem.UI
{
    [CreateAssetMenu(menuName = "Space Memory Utilities/Color Palette")]
    public class ColorPalette : ScriptableObject
    {
        [SerializeField] public List<Color> colorPalette = new List<Color>() { 
            new Color(173f / 255f, 216f / 255f, 230f / 255f, 1f), 
            new Color(152f / 255f, 251f / 255f, 152f / 255f, 1f),
            new Color(255f / 255f, 218f / 255f, 185f / 255f, 1f),
            new Color(230f / 255f, 230f / 255f, 250f / 255f, 1f)
        };
    }
}


