using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Entity.Interfaces
{
    public interface IAction
    {
        public float ActionRange { get; }

        void ExecuteAction();
    }
}
