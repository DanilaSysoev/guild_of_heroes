using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldTileMenuItem : MonoBehaviour
{
    [MenuItem("Assets/Create/WorldTile")]
    private static void CreateTileFromSprite()
    {
        string path = EditorUtility.SaveFolderPanel("World Tiles Folder selection", "Assets/Objects/TileAssets", "");
        var assetsIndex = path.IndexOf("Assets");
        if (path == "" || assetsIndex < 0)
            return;

        path = path.Substring(path.IndexOf("Assets"));
        var objects = Selection.GetFiltered<Texture2D>(SelectionMode.Unfiltered);        
        foreach (var texture in objects)
        {            
            WorldTile tile = ScriptableObject.CreateInstance<WorldTile>();
            tile.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(texture)); //Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, 0), 256, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            tile.colliderType = UnityEngine.Tilemaps.Tile.ColliderType.None;
            tile.color = Color.white;
            tile.flags = UnityEngine.Tilemaps.TileFlags.LockColor;
            tile.name = texture.name;            
            AssetDatabase.CreateAsset(tile, path + "/" + texture.name + ".asset");
            AssetDatabase.SaveAssets();
        }  
    }
}
