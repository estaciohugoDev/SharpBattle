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
            HP = 30.00;
            DMG *= 1.2;
            DEF += 10.00;
            MAG = 2.0;
            HLY = 4.0;
            LCK = 6.0;
        }
        #region CLASS SKILLS
        public static double CrossSlash(Player player, Enemy enemy)
        {
            Random rnd = new Random();
            double skillDamage = player.DMG;
            skillDamage += rnd.Next(10, 50);
            enemy.HP -= skillDamage;
            System.Console.WriteLine($"{player.Name} casts Cross Slash, {enemy.Name} receives {skillDamage} damage!");
            if (enemy.HP <= 0)
            {
                Console.WriteLine($"{player.Name} beat {enemy.Name}!");
                return 0;
            }
            return skillDamage;
        }
        #endregion CLASS SKILLS

        #region AUXILIARY METHODS
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