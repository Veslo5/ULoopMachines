using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

public class MapEditorResources : Singleton<MapEditorResources>
{

    public Sprite BasicTexture;

    public Sprite Grass001;
    public Sprite Road001;


    private void Start()
    {
        loadAddressables();
    }

    private void loadAddressables()
    {
        var addresable = Addressables.LoadAssetAsync<Sprite>("Assets/Sprites/default.png");
        BasicTexture = addresable.WaitForCompletion();

        var addresableGrass = Addressables.LoadAssetAsync<Sprite>("Assets/Sprites/Roads/grass001.png");
        Grass001 = addresableGrass.WaitForCompletion();

        var addresableRoad = Addressables.LoadAssetAsync<Sprite>("Assets/Sprites/Roads/road001.png");
        Road001 = addresableRoad.WaitForCompletion();
    }


}
