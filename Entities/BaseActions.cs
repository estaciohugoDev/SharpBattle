using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBattle.Entities
{
    public interface IBaseActions
    {
        double Attack(BaseEntity target);
        double Guard();
        double Cast(BaseEntity target);
        void Use(); //TODO: use an item from Inventory
        void TakeDamage (int damage);
    }
}