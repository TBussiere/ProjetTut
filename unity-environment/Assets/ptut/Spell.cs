using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    abstract class Spell : IActif
    {
        private int id;
        private string Nom;
        private string Desc;
        private int Po;
        private int cl;
        private int cPa;
        private int cd;
        private int Dmg;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">L'id</param>
        /// <param name="Nom">Le nom</param>
        /// <param name="desc">La description</param>
        /// <param name="Po">La porté</param>
        /// <param name="cl">La class de l'arme</param>
        /// <param name="cpa">Le cout en PA</param>
        /// <param name="cd">le Cooldown</param>
        public Spell(int id, string Nom, string desc, int Po, int cl, int cpa, int cd,int dmg)
        {
            this.id = id;
            this.Nom = Nom;
            this.Desc = desc;
            this.Po = Po;
            this.cl = cl;
            this.cPa = cpa;
            this.cd = cd;
            this.Dmg = dmg;
        }
        public int classe
        {
            get
            {
                return cl;
            }
        }

        public int cooldown
        {
            get
            {
                return cd;
            }
        }

        public int cost
        {
            get
            {
                return cPa;
            }
        }

        public string Description
        {
            get
            {
                return Desc;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return Nom;
            }
        }

        public int range
        {
            get
            {
                return Po;
            }
        }

        public int Damage
        {
            get
            {
                return Dmg;
            }
        }

        public bool canShoot(ClicCell c1, ClicCell c2)
        {
            return ClicCell.distanceCells(c1, c2) <= Po;
        }
    }
}
