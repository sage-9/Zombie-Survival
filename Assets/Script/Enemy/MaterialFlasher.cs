using UnityEngine;
using System.Collections;

public class MaterialFlasher : MonoBehaviour
{
    public void FlashRed(Material material, float duration = 0.1f)
    {
        StartCoroutine(FlashRedCoroutine(material, duration));
    }

    private IEnumerator FlashRedCoroutine(Material material, float duration)
    {
        if (material == null)
        {
            Debug.LogError("Material is null, cannot flash.");
            yield break;
        }

        // Store the original color
        Color originalColor = material.color;

        // Set the material color to red
        material.color = Color.red;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert back to the original color
        material.color = originalColor;
    }
}