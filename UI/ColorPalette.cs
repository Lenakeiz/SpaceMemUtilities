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
            new Color(230f / 255f, 230f / 255f, 250f / 255f, 1f),
            new Color(255f / 255f, 182f / 255f, 193f / 255f, 1f), // Pastel Pink
            new Color(176f / 255f, 224f / 255f, 230f / 255f, 1f), // Powder Blue
            new Color(255f / 255f, 255f / 255f, 224f / 255f, 1f), // Pale Yellow
            new Color(216f / 255f, 191f / 255f, 216f / 255f, 1f)  // Thistle
        };
    }
}


