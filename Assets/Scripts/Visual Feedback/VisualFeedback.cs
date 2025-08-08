using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour
{
    public GameObject addedScorePrefab;
    public GameObject oldFruitPrefab;
    public GameObject newFruitPrefab;
    public AudioSource audioSource;
    public AudioClip effectClip;
    public static VisualFeedback Instance { get; set; }

    void Awake()
    {
        Instance = this;
    }
   
    public void ShowAddedScore(int amount)
    {
        Vector2 offset = new Vector2(0.5f,1.0f);
        Vector2 scorePosition = FruitCombiner.Instance.collisionPoint + offset;
        Instantiate(addedScorePrefab, scorePosition, Quaternion.identity, transform);
        addedScorePrefab.GetComponent<TMP_Text>().text = amount.ToString();
    }
    public void ShowMergeEffects()
    {
        // Particle 1
        GameObject instance1 = Instantiate(oldFruitPrefab, FruitCombiner.Instance.collisionPoint, Quaternion.identity, transform);
        ParticleSystem particle1 = instance1.GetComponent<ParticleSystem>();
        /*
        GameObject fruitPrefab1 = FruitSelector.instance.Fruits[ThrowFruitController.instance.currentFruitIndex];
        Sprite chosenSprite1 = fruitPrefab1.GetComponent<SpriteRenderer>().sprite;
        var textureSheet1 = particle1.textureSheetAnimation;
        textureSheet1.mode = ParticleSystemAnimationMode.Sprites;
        // Clear old sprites and add the new one
        if (textureSheet1.spriteCount > 0)
        {
            textureSheet1.RemoveSprite(0); // Clear existing
        }
        textureSheet1.AddSprite(chosenSprite1);
        */
        particle1.Play();

        // particle 2
        GameObject instance2 = Instantiate(newFruitPrefab, FruitCombiner.Instance.collisionPoint, Quaternion.identity, transform);
        ParticleSystem particle2 = instance2.GetComponent<ParticleSystem>();
        /*
        GameObject fruitPrefab2 = FruitSelector.instance.Fruits[ThrowFruitController.instance.currentFruitIndex];
        Sprite chosenSprite2 = fruitPrefab2.GetComponent<SpriteRenderer>().sprite;
        var texturesheet2 = particle2.textureSheetAnimation;
        texturesheet2.mode = ParticleSystemAnimationMode.Sprites;
        // Clear old sprites and add the new one
        if (texturesheet2.spriteCount > 0)
        {
            texturesheet2.RemoveSprite(0);
        }
        texturesheet2.AddSprite(chosenSprite2);
        */
        particle2.Play();
    }
    public void AudioEffects()
    {
        audioSource.PlayOneShot(effectClip);
    }
}
