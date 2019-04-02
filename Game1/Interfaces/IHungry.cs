using Game1.InfoTransfer;

namespace Game1.Interfaces
{
    public interface IHungry
    {
        void Eat(IEdible edible);
        NutrientPack Poop();
    }
}
