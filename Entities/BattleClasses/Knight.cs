using System;
using System.Text;
using System.Collections.Generic;
using SharpBattle.Entities;

namespace SharpBattle
{
    class Knight : BaseClass
    {
        public Knight() : base()
        {
            HP *= 0.2;
            STR *= 0.5;
            DEF *= 1.1;
            MAG *= 2.0;
            HLY *= 4.0;
        }
        #region CLASS SKILLS
        public static double CrossSlash(Player player, BaseEntity target)
        {
            Random rnd = new();
            var skillDamage = player.DMG + rnd.Next(10, 50);
            target.HP -= skillDamage;
            System.Console.WriteLine($"{Player.Name} casts Cross Slash, {target.Name} receives {skillDamage} damage!");
            if (target.HP <= 0)
            {
                Console.WriteLine($"{Player.Name} beat {target.Name}!");
                return 0;
            }
            return skillDamage;
        }
        #endregion CLASS SKILLS

        #region AUXILIARY METHODS
        public void ListSkills()
        {
            Console.Clear();
            StringBuilder listOfSkills = new();
            listOfSkills.AppendLine($"-----> {this.GetType().Name} Skills <-----");
            listOfSkills.Append("1 - Cross Slash (Phys DMG)");

            System.Console.WriteLine(listOfSkills);
        }
        public static void ChooseSkill(Player player, Enemy enemy)
        {
            System.Console.WriteLine("Choose a skill: ");
            short choice = short.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Knight.CrossSlash(player, enemy);
                    break;

                default:
                    System.Console.WriteLine("Invalid option, going back.");
                    player.BattleActions(enemy);
                    break;
            }
        }
        #endregion AUXILIARY METHODS
    }
}