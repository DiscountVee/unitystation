﻿using Matrix;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tools : Editor {

    [MenuItem("Tools/Reconnect TileConnect")]
    static void RevertTileConnect() {
        var triggers = FindObjectsOfType<ConnectTrigger>();

        foreach(var t in triggers) {
            PrefabUtility.RevertPrefabInstance(t.gameObject);
        }
    }

    [MenuItem("Tools/Revert To Prefab %r")]
    static void RevertPrefabs() {
        var selection = Selection.gameObjects;

        if(selection.Length > 0) {
            for(var i = 0; i < selection.Length; i++) {
                PrefabUtility.RevertPrefabInstance(selection[i]);
                PrefabUtility.ReconnectToLastPrefab(selection[i]);
            }
        } else {
            Debug.Log("Cannot revert to prefab - nothing selected");
        }
    }

    [MenuItem("Tools/Resection Tiles")]
    static void ConnectTiles2Sections() {
        var registerTiles = FindObjectsOfType<RegisterTile>();

        foreach(var r in registerTiles) {
            var p = r.transform.position;

            int x = Mathf.RoundToInt(p.x);
            int y = Mathf.RoundToInt(p.y);

            r.transform.MoveToSection(Matrix.Matrix.At(x, y).Section);
            //PrefabUtility.RevertPrefabInstance(r.gameObject);
        }
    }
}
