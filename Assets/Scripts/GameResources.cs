using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : Singleton<GameResources>
{
    [SerializeField]
    private string[] SpriteSets;

    public const string BACKGROUND1 = "background_1";
    public const string MEDIUM1 = "medium_1";
    public const string ROUND901 = "round90_1";

    private Dictionary<string, SpriteSetResource> loadedSpriteSets = new Dictionary<string, SpriteSetResource>();

    public override void Awake()
    {
        base.Awake();

        foreach (var set in SpriteSets)
        {
            loadedSpriteSets.Add(set, new SpriteSetResource());
        }

        LoadAssets();
    }

    public void LoadAssets()
    {
        foreach (var spriteSet in loadedSpriteSets)
        {
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + BACKGROUND1);
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MEDIUM1);
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + ROUND901);
        }
    }

    public Sprite GetSprite(string set, string path)
    {
        return loadedSpriteSets[set].GetSpriteResource(path);
    }

    




}
