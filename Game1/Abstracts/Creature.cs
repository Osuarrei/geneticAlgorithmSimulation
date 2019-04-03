using Game1.Entities;
using Game1.InfoTransfer;
using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.Abstracts
{
    public abstract class Creature : ICreature
    {
        public Creature(int xloc, int yloc)
        {
            this.xloc = xloc;
            this.yloc = yloc;

            NutrientReserves = new float[3];
            foreach (var nutrient in this.creatureGenes.TargetNutrients)
            {
                NutrientReserves[(int)nutrient] = (float)SimulationGlobals.random.NextDouble() * this.creatureGenes.MaxNutrients;
            }
            this.CurrentBreedingCooldown = 0;
            CurrentHealth = this.creatureGenes.MaxHealth;
        }
        public Creature(Vector2 location, CreatureGenePack genes)
        {
            this.xloc = (int)location.X;
            this.yloc = (int)location.Y;

            this.Genes = genes;

            NutrientReserves = new float[3];
            foreach (var nutrient in this.creatureGenes.TargetNutrients)
            {
                NutrientReserves[(int)nutrient] = (float)SimulationGlobals.random.NextDouble() * this.creatureGenes.MaxNutrients;
            }
            this.CurrentBreedingCooldown = 0;
            CurrentHealth = this.creatureGenes.MaxHealth;
        }

        protected int xloc { get; set; }
        protected int yloc { get; set; }

        protected int CurrentBreedingCooldown { get; set; }
        protected float[] NutrientReserves { get; set; }
        protected int CurrentHealth { get; set; }
        public GenePack Genes { get; protected set; }
        public CreatureGenePack creatureGenes { get => Genes as CreatureGenePack; }

        public bool CanBreed => creatureGenes.BreedingCooldown >= CurrentBreedingCooldown;

        public bool IsDead { get; protected set; }

        public float gatheringDifficulty => creatureGenes.gatheringDifficulty;

        public float BreedingChance => creatureGenes.BreedingChance;

        public void BreedWith(Creature target, List<Creature> breedables)
        {
            if (this.CanBreed && target.CanBreed)
            {
                if (this.GetType() == target.GetType())
                {
                    var chanceHolder = this.BreedingChance * target.BreedingChance;
                    if (chanceHolder > SimulationGlobals.random.NextDouble())
                    {
                        this.creatureGenes.Combine(target.Genes);
                        breedables.Add((Creature)Activator.CreateInstance(this.GetType(), new object[] { this.xloc, this.yloc }));
                        CurrentBreedingCooldown = 0;
                        target.ResetBreedingCooldown();
                    }
                }
            }
        }

        public void ResetBreedingCooldown()
        {
            this.CurrentBreedingCooldown = 0;
        }

        public abstract void Draw(GraphicsDevice graphicsDevice);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void Eat(IEdible edible)
        {
            var nutrientHolder = edible.Eaten(new InfoTransfer.NutrientRequest(this.creatureGenes.TargetNutrients[SimulationGlobals.random.Next(0, this.creatureGenes.TargetNutrients.Length)], this.creatureGenes.GatheringEffectiveness));
            NutrientReserves[(int)nutrientHolder.type] += nutrientHolder.amount;
        }

        public NutrientPack Eaten(NutrientRequest request)
        {
            throw new NotImplementedException();
        }

        public void MoveMe()
        {
            switch (SimulationGlobals.random.Next(0, 4))
            {
                case 0:
                    if (yloc + 1 < (int)SimulationStateEnums.MapValues.MapHeightTiles)
                    {
                        yloc++;
                    }
                    break;
                case 1:
                    if (xloc + 1 < (int)SimulationStateEnums.MapValues.MapWidthTiles)
                    {
                        xloc++;
                    }
                    break;
                case 2:
                    if (yloc - 1 > -1)
                    {
                        yloc--;
                    }
                    break;
                case 3:
                    if (xloc - 1 > -1)
                    {
                        xloc--;
                    }
                    break;
            }
        }

        public void Update(Map map)
        {
            var currentTile = map.GetTile(this.Location());
            UpdateHealth();

            if (this.IsDead)
            {
                foreach (var item in this.creatureGenes.TargetNutrients)
                {
                    currentTile.AddNutrients(new NutrientPack(item, NutrientReserves[(int)item]));
                }
            }
            else
            {
                currentTile.AddNutrients(Poop());
            }

            if (this.CurrentBreedingCooldown <= this.creatureGenes.BreedingCooldown)
            {
                this.CurrentBreedingCooldown++;
            }

            switch (SimulationGlobals.random.Next(0, 11))
            {
                case 1:
                    MoveMe();
                    break;
                default:
                    Eat(currentTile);
                    break;
            }
        }

        public void UpdateHealth()
        {
            foreach (var target in this.creatureGenes.TargetNutrients)
            {
                if (NutrientReserves[(int)target] < this.creatureGenes.StarvingNutrientLevel)
                {
                    //reduce health
                    this.CurrentHealth--;
                    if (this.CurrentHealth <= 0)
                    {
                        IsDead = true;
                    }
                }
                else if (NutrientReserves[(int)target] > (2 * this.creatureGenes.StarvingNutrientLevel))
                {
                    //add to health
                    var amountToAdd = this.creatureGenes.MaxHealth * this.creatureGenes.RestoreHealthRate;
                    if (this.CurrentHealth + amountToAdd < this.creatureGenes.MaxHealth)
                    {
                        this.CurrentHealth += (int)amountToAdd;
                    }
                    else
                    {
                        this.CurrentHealth = this.creatureGenes.MaxHealth;
                    }
                }
            }
        }

        public Vector2 Location()
        {
            return new Vector2(xloc, yloc);
        }

        public NutrientPack Poop()
        {
            var nutrient = this.creatureGenes.TargetNutrients[SimulationGlobals.random.Next(0, this.creatureGenes.TargetNutrients.Length)];
            float pooPercent = (float)SimulationGlobals.random.NextDouble() * 0.05f;

            float amount = pooPercent * this.NutrientReserves[(int)nutrient];
            this.NutrientReserves[(int)nutrient] -= amount;

            return new NutrientPack(nutrient, amount);
        }
    }
}
