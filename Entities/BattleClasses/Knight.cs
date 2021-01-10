using System;
using System.Text;
using System.Collections.Generic;
using SharpBattle.Entities;

namespace SharpBattle
{
    class Knight : BaseClass
    { 
        static Knight KnightClass{get => GetKnightBuffer();}
        public Knight() : base()
        { 
            HP      = 30.00;
            DMG     *= 0.2;
            DEF     = 10.00;
            MAG     = 2.0;
            HLY     = 4.0;
            LCK     = 6.0;
        }
//        <-------CLASS SKILLS--------->
        public static double CrossSlash(Player player, Enemy enemy)
        {
            
            Random rnd = new Random();
            double skillDamage = GetKnightBuffer().DMG;
            skillDamage += rnd.Next(10,50);
            enemy.HP -= skillDamage;
            System.Console.WriteLine($"{player.Name} casts Cross Slash, {enemy.Name} receives {skillDamage} damage!");
            if (enemy.HP <= 0)
            {
                Console.WriteLine($"{player.Name} beat {enemy.Name}!");
                return 0;
            }
            return skillDamage;
        }

//        <-------AUXILIARY METHODS--------->
        public void ListSkills()
        {
            Console.Clear();
            StringBuilder listOfSkills = new StringBuilder();
            listOfSkills.AppendLine($"-----> {this.GetType().Name} Skills <-----");
            listOfSkills.Append("1 - Cross Slash (Phys DMG)");

            System.Console.WriteLine(listOfSkills);
        }
        public void ChooseSkill(Player player, Enemy enemy)
        {
            System.Console.WriteLine("Choose a skill: ");
            short choice = short.Parse(Console.ReadLine());

            switch(choice)
            {
                case 1:
                    Knight.CrossSlash(player,enemy);
                    break;

                default :
                System.Console.WriteLine("Invalid option, going back.");
                player.NextAction(enemy);
                    break; 
            }
        }
        public static void OverridePlayerStats(Player player)
        {
            var knight = new Knight();

            player.HP   = knight.HP;
            player.DMG  = knight.DMG;
            player.DEF  = knight.DEF;
            player.MAG  = knight.MAG;
            player.HLY  = knight.HLY;
            player.LCK  = knight.LCK;
        }
        public static Knight GetKnightBuffer()
        {
            Knight knight = new Knight();
            return knight;
        }    
    }
}