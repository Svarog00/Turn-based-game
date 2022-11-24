using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Scripts.Entity
{
    public interface IHealth
    {
        public int HealthPoints { get; set; }

        public void Heal(int health);
        public void Hurt(int damage);
    }
}
