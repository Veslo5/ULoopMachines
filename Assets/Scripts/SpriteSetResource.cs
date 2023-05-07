using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SpriteSetResource
{
    private Dictionary<string, Sprite> sprites;

    public SpriteSetResource()
    {
        sprites = new Dictionary<string, Sprite>();
    }

    public void LoadSpriteResource(string path){
        var addresableHandle = Addressables.LoadAssetAsync<Sprite>(path);
        sprites.Add(path,addresableHandle.WaitForCompletion());
    }

    public Sprite GetSpriteResource(string path){
        return sprites[path];
    }

}
