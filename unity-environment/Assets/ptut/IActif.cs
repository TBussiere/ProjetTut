using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    interface IActif
    {
        int ID { get; }
        string Name { get; }
        string Description { get; }
        int range { get; }
        int classe { get; }
        int cost { get; }
        int cooldown { get; }

        int Damage { get; }
        bool canShoot(ClicCell c1, ClicCell c2);

    }
}
