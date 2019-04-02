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

            GatheringEffectiveness = SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowEffectivenessCap)] * (float)SimulationGlobals.random.NextDouble();
            var numOfNutrients = SimulationGlobals.random.Next(1, 4);
            TargetNutrients = new NutrientTypes[numOfNutrients];
            NutrientReserves = new float[3];
            MaxNutrients = ((float)SimulationGlobals.random.NextDouble() * 30.0f) + 20.0f;
            StarvingNutrientLevel = ((float)SimulationGlobals.random.NextDouble() * 15.0f) + 10.0f;

            List<NutrientTypes> typeHolder = Enum.GetValues(typeof(NutrientTypes)).Cast<NutrientTypes>().ToList();

            for (int i = 0; i < numOfNutrients; i++)
            {
                var chosenIndex = SimulationGlobals.random.Next(0, typeHolder.Count);
                TargetNutrients[i] = typeHolder[chosenIndex];
                typeHolder.RemoveAt(chosenIndex);
            }

            foreach (var nutrient in TargetNutrients)
            {
                NutrientReserves[(int)nutrient] = (float)SimulationGlobals.random.NextDouble() * this.MaxNutrients;
            }

            BreedingCooldown = SimulationGlobals.random.Next((int)SimulationGlobals.AttributeValues[nameof(SimulationStateEnums.AttributeKeys.CowBreedCooldownMin)], 1500);
            BreedingChance = (float)SimulationGlobals.random.NextDouble() * SimulationGlobals.AttributeValues[nameof(AttributeKeys.CowBreedabilityCap)];
            this.CurrentBreedingCooldown = 0;

            MaxHealth = SimulationGlobals.random.Next(50, 101);
            CurrentHealth = this.MaxHealth;
            RestoreHealthRate = (float)SimulationGlobals.random.NextDouble() * 0.1f;
        }

        protected int xloc { get; set; }
        protected int yloc { get; set; }

        protected int BreedingCooldown { get; set; }
        protected int CurrentBreedingCooldown { get; set; }
        public float BreedingChance { get; protected set; }

        protected float[] NutrientReserves { get; set; }
        protected NutrientTypes[] TargetNutrients { get; set; }

        public float gatheringDifficulty { get; private set; }

        protected float GatheringEffectiveness { get; set; }
        protected float MaxNutrients { get; set; }
        protected float StarvingNutrientLevel { get; set;}

        protected int MaxHealth { get; set; }
        protected int CurrentHealth { get; set; }
        protected float RestoreHealthRate { get; set; }

        public bool CanBreed => BreedingCooldown >= CurrentBreedingCooldown;

        public bool IsDead { get; protected set; }

        public void BreedWith(IBreedable target, List<Creature> breedables)
        {
            if (this.CanBreed && target.CanBreed)
            {
                if (this.GetType() == target.GetType())
                {
                    var chanceHolder = this.BreedingChance * target.BreedingChance;
                    if (chanceHolder > SimulationGlobals.random.NextDouble())
                    {
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
            var nutrientHolder = edible.Eaten(new InfoTransfer.NutrientRequest(TargetNutrients[SimulationGlobals.random.Next(0, TargetNutrients.Length)], GatheringEffectiveness));
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
                foreach (var item in TargetNutrients)
                {
                    currentTile.AddNutrients(new NutrientPack(item, NutrientReserves[(int)item]));
                }
            }
            else
            {
                currentTile.AddNutrients(Poop());
            }

            if (this.CurrentBreedingCooldown <= BreedingCooldown)
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
            foreach (var target in TargetNutrients)
            {
                if (NutrientReserves[(int)target] < StarvingNutrientLevel)
                {
                    //reduce health
                    this.CurrentHealth--;
                    if (this.CurrentHealth <= 0)
                    {
                        IsDead = true;
                    }
                }
                else if (NutrientReserves[(int)target] > (2 * StarvingNutrientLevel))
                {
                    //add to health
                    var amountToAdd = this.MaxHealth * this.RestoreHealthRate;
                    if (this.CurrentHealth + amountToAdd < this.MaxHealth)
                    {
                        this.CurrentHealth += (int)amountToAdd;
                    }
                    else
                    {
                        this.CurrentHealth = this.MaxHealth;
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
            var nutrient = this.TargetNutrients[SimulationGlobals.random.Next(0, this.TargetNutrients.Length)];
            float pooPercent = (float)SimulationGlobals.random.NextDouble() * 0.05f;

            float amount = pooPercent * this.NutrientReserves[(int)nutrient];
            this.NutrientReserves[(int)nutrient] -= amount;

            return new NutrientPack(nutrient, amount);
        }
    }
}
