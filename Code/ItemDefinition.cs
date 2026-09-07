[GameResource("Item Definition", "item", "A weapon or inventory item")]
public class ItemDefinition : GameResource
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Texture icon { get; set; }
    public PrefabFile heldPrefab { get; set; }
    public Sandbox.Citizen.CitizenAnimationHelper.HoldTypes holdType { get; set; }
    public int maxStack { get; set; } = 1;
    public float damage { get; set; }
    public float attackSpeed { get; set; }
}