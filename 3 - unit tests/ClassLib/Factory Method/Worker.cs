﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib.Decorator;

namespace ClassLib.Factory_Method
{
    public class Worker : Robot
    {
        public Worker(string image)
            : base(image, "Worker", "Highest payload and battery, 10% decode cargo", 100, 200)
        {

        }

        public override string AddStone(params Stone[] stone)
        {
            Stone st = stone[0];
            if (st.Weight + CurrentWeight <= MaxWeight)
            {
                if (st.Decryption)
                {
                    Random random = new Random();
                    if (random.Next(11) == 0)
                    {
                        Baggage.Add(st);
                        CurrentWeight += st.Weight;
                        return st.GetInfo() + "\r\n Succesfully decrypted and added";
                    }
                    else
                    {
                        return "Decryption failed";
                    }
                }
                else
                {
                    Baggage.Add(st);
                    CurrentWeight += st.Weight;
                    return st.GetInfo() + "\r\n Succesfully added";
                }
            }
            else
            {
                return "You can`t lift this stone. Robot overload";
            }
        }

        public override string DropStone()
        {
            if (Baggage.Count > 0)
            {
                string info = Baggage[Baggage.Count - 1].GetInfo();
                CurrentWeight -= Baggage[Baggage.Count - 1].Weight;
                Baggage.RemoveAt(Baggage.Count - 1);
                return "Succesfully droped: " + info;
            }
            return "Baggage already empty";
        }

        public override string GetBaggageInfo()
        {
            double cost = 0;
            double weight = 0;
            foreach (var item in Baggage)
            {
                cost += item.GetCost();
                weight += item.Weight;
            }
            return "Total weight: " + weight + ", total cost: " + cost + ", free space: " + (MaxWeight - weight);
        }

        public override string GetInfo()
        {
            return "Id: " + Id + ", image: " + RobotImageBase64 + ", name: " + Name + ", description: " + Description
                + ", battery charge: " + BatteryCharge + ", max weight: " + MaxWeight + ", current weight: " + CurrentWeight
                + ", baggage count: " + Baggage.Count;
        }

        public override string Turn()
        {
            string res = "";
            double battery = BatteryCharge;

            foreach (var item in Baggage)
            {
                if (item.Collapses)
                {
                    if (item.StoneHealth > 1)
                        item.DecreaseHealth();
                    else
                        res += DropCollapsedStone(item);
                }
                BatteryCharge -= item.Weight * 0.1; 
            }

            foreach (var item in StonesToDrop)
            {
                CurrentWeight -= item.Weight;
                Baggage.Remove(item);
            }

            BatteryCharge--;
            return "Battery charge: " + BatteryCharge + ", battery lost: " + (battery - BatteryCharge) + ", " + res;
        }

        public override bool IsAlive()
        {
            if (BatteryCharge > 0)
                return true;
            return false;
        }

        public override string FinalInfo()
        {
            double cost = 0;
            foreach (var item in Baggage)
            {
                cost += item.GetCost();
            }
            return "Total baggage cost: " + cost + ", battery level: " + (BatteryCharge > 0 ? BatteryCharge : 0) + ".";
        }
    }
}