using System;

[Serializable]
public class ItemRecipe {
    public enum Type {
        Assemble,Burn
    }
    public ItemStack stack;
    public Type type;
}