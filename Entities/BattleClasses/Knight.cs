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
            HP *= (int)1.5;
            STR *= (int)1.5;
            DEF *= (int)1.2;
            MAG *= 1;
            HLY *= 1;
            Name = "Knight";
        }
        #region CLASS SKILLS
        public static double CrossSlash(Player player, BaseEntity target)
        {
            var skillDamage = player.DMG + player.Roll.Next(10, 50);
            target.TakeDamage(skillDamage);
            System.Console.WriteLine($"{player.Name} casts Cross Slash, {target.Name} receives {skillDamage} damage!");
            if (target.HP <= 0)
            {
                Console.WriteLine($"{player.Name} defeated {target.Name}!");
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
            listOfSkills.AppendLine($"-----> {Name} Skills <-----");
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
                    player.Actions(enemy);
                    break;
            }
        }
        #endregion AUXILIARY METHODS
    }
}