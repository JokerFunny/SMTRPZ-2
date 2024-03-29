﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public class CollapseStone : StoneDecorator
    {
        public CollapseStone(int stoneHealth, Stone stone)
            : base(stone.Damage, stone.Description + ", will destroy on every turn", stone.Weight, stone.Decryption, stoneHealth, true, stone)
        {

        }

        private CollapseStone(Stone stone)
            : base(stone.Damage, stone.Description, stone.Weight, false, stone.StoneHealth, true, stone)
        {

        }

        public override void DecreaseHealth()
        {
            if (StoneHealth > 0)
                StoneHealth--;
        }

        public override double GetCost()
        {
            return stone.GetCost() * 1.5;
        }

        public override string GetInfo()
        {
            string res = "Cost: " + GetCost() + ", weight: " + Weight + ", description: " + Description;
            if (Collapses)
                res += ", stone health: " + StoneHealth;
            if (Damage > 0)
                res += ", damage: " + Damage;
            return res;
        }

        public override object Clone()
        {
            return new CollapseStone(new Stone(Damage, Description, Weight, Decryption, StoneHealth, Collapses));
        }
    }
}
