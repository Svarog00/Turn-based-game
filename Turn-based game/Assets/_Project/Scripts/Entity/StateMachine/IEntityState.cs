using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Scripts.Entity.StateMachine
{
    public interface IEntityState
    {
        void Enter();
        void Handle();
        void Exit();
    }
}
