using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapParts
{
    background_1 = 0,
    small_1 = 1,
    medium_1 = 2,
    large_1 = 3,
    round90_s_1 = 4,
    round90_m_1 = 5, 
    transition_ms_1 = 6,
    transition_ml_1 = 7

}

public class GameResources : Singleton<GameResources>
{
    [SerializeField]
    private string[] SpriteSets;

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
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.background_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.small_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.medium_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.large_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.round90_s_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.round90_m_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.transition_ms_1.ToString());
            spriteSet.Value.LoadSpriteResource(spriteSet.Key + "/" + MapParts.transition_ml_1.ToString());
        }
    }

    public Sprite GetSprite(string set, string path)
    {
        return loadedSpriteSets[set].GetSpriteResource(path);
    }

    public void UnloadResources(){

        

    }




}
