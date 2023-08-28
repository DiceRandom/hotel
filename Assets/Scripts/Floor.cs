using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FloorInfo {
public enum FloorType {Lobby,Bedroom};

[CreateAssetMenu(fileName = "Floor", menuName = "Hotel/Floor", order = 0)]
public class Floor : ScriptableObject {
    public FloorType floorType;
    public int level;
    public Sprite floorTexture;
    public Sprite shaderTexture;
}
}

