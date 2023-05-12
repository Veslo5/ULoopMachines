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

public enum StuffParts
{
    roadcone_1 = 0
}

public enum EditorParts
{
    ghost = 0,
    @default = 1
}

public enum EffectParts
{
    boost = 0,
    deathbox = 1,
    mysterybox = 2
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

        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.background_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.small_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.medium_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.large_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.round90_s_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.round90_m_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.transition_ms_1.ToString());
        loadedSpriteSets["Prototype"].LoadSpriteResource("Prototype/" + MapParts.transition_ml_1.ToString());


        loadedSpriteSets["Stuff"].LoadSpriteResource("Stuff/" + StuffParts.roadcone_1.ToString());

        loadedSpriteSets["Editor"].LoadSpriteResource("Editor/" + EditorParts.ghost.ToString());
        loadedSpriteSets["Editor"].LoadSpriteResource("Editor/" + EditorParts.@default.ToString());

        loadedSpriteSets["Effects"].LoadSpriteResource("Effects/" + EffectParts.boost.ToString());
        loadedSpriteSets["Effects"].LoadSpriteResource("Effects/" + EffectParts.deathbox.ToString());
        loadedSpriteSets["Effects"].LoadSpriteResource("Effects/" + EffectParts.mysterybox.ToString());

    }

    public Sprite GetSprite(string set, string path)
    {
        return loadedSpriteSets[set].GetSpriteResource(path);
    }

    public void UnloadResources()
    {



    }




}
