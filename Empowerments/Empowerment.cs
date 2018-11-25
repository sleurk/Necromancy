using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Necromancy.Empowerments
{
	public abstract class Empowerment
    {
        public EmpType empType;
        public int time = 0;
        public int power = 0;
        public bool flag = false;
        public int owner;
        public int maxTime;

        public Empowerment(int time, int owner, int maxTime, bool flag, int power)
        {
            this.time = time;
            this.owner = owner;
            this.maxTime = maxTime;
            this.flag = flag;
            this.power = power;
        }

        public Player Player
        {
            get { return Main.player[owner]; }
        }

        public virtual Color Color
        {
            get { return default(Color); }
        }

        public virtual Color TextColor
        {
            get { return Color; }
        }

        public virtual Point Frame
        {
            get { return new Point(); }
        }

        public virtual string EmpDisplayName
        {
            get { return ""; }
        }
        
        public virtual string Text
        {
            get { return power / 2 + "%"; }
        }

        public virtual Texture2D Texture
        {
            get { return ModLoader.GetTexture("Necromancy/Empowerments/Empowerments"); }
        }

        public abstract void Behavior();

        public static Empowerment NewEmp(EmpType empType, int time, int owner, int maxTime = -1, bool flag = true, int power = 1)
        {
            if (maxTime == -1) maxTime = time;
            Empowerment emp = null;
            switch (empType)
            {
                case EmpType.MeleeDamage:
                    {
                        emp = new EmpowermentMeleeDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Damage:
                    {
                        emp = new EmpowermentDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.ThrowingDamage:
                    {
                        emp = new EmpowermentThrowingDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.CritChance:
                    {
                        emp = new EmpowermentCritChance(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.RadiantDamage:
                    {
                        emp = new EmpowermentRadiantDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Dodge:
                       {
                           emp = new EmpowermentDodge(time, owner, maxTime, flag, power);
                           break;
                       }
                case EmpType.MaxLife:
                    {
                        emp = new EmpowermentMaxLife(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.ArmorPierce:
                    {
                        emp = new EmpowermentArmorPierce(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.RangedDamage:
                    {
                        emp = new EmpowermentRangedDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.AmmoConsumption:
                    {
                        emp = new EmpowermentAmmoConsumption(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.SymphonicDamage:
                    {
                        emp = new EmpowermentSymphonicDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.MoveSpeed:
                    {
                        emp = new EmpowermentMoveSpeed(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Flight:
                    {
                        emp = new EmpowermentFlight(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.AttackSpeed:
                    {
                        emp = new EmpowermentAttackSpeed(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.MagicDamage:
                    {
                        emp = new EmpowermentMagicDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Endurance:
                    {
                        emp = new EmpowermentEndurance(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Defense:
                    {
                        emp = new EmpowermentDefense(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.MaxMana:
                    {
                        emp = new EmpowermentMaxMana(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.ManaRegen:
                    {
                        emp = new EmpowermentManaRegen(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.ManaEfficiency:
                    {
                        emp = new EmpowermentManaEfficiency(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.SummonDamage:
                    {
                        emp = new EmpowermentSummonDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Restoration:
                    {
                        emp = new EmpowermentRestoration(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.LifeRegen:
                    {
                        emp = new EmpowermentLifeRegen(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.LifeSteal:
                    {
                        emp = new EmpowermentLifeSteal(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.NecroticDamage:
                    {
                        emp = new EmpowermentNecroticDamage(time, owner, maxTime, flag, power);
                        break;
                    }
                case EmpType.Immortality:
                    {
                        emp = new EmpowermentImmortality(time, owner, maxTime, flag, power);
                        break;
                    }
            }
            return emp;
        }
    }

    public class EmpowermentComparer : IComparer<EmpType>
    {
        public int Compare(EmpType x, EmpType y)
        {
            Empowerment empX = Empowerment.NewEmp(x, 0, 0);
            Empowerment empY = Empowerment.NewEmp(y, 0, 0);
            float xValue = empX.Frame.X + empX.Frame.Y * 0.5f;
            float yValue = empY.Frame.X + empY.Frame.Y * 0.5f;
            return Math.Sign(xValue - yValue);
        }
    }

    public enum EmpType : byte
    {
        MeleeDamage,
        Damage,
        ThrowingDamage,
        CritChance,
        RadiantDamage,
        Dodge,
        MaxLife,
        ArmorPierce,
        RangedDamage,
        AmmoConsumption,
        SymphonicDamage,
        MoveSpeed,
        Flight,
        AttackSpeed,
        MagicDamage,
        Endurance,
        MaxMana,
        Defense,
        ManaRegen,
        ManaEfficiency,
        SummonDamage,
        Restoration,
        LifeRegen,
        LifeSteal,
        NecroticDamage,
        Immortality
    }
}