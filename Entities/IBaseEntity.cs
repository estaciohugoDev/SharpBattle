using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBattle.Entities
{
    public interface IBaseEntity
    {
        int Attack(BaseEntity target);
        int Guard();
        int Cast(BaseEntity target);
        void Use(); //TODO: use an item from Inventory
        int TakeDamage (int damage);
    }
}