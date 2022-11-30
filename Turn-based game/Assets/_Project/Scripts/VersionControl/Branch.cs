using System;
using System.Linq;

namespace Assets._Project.Scripts.VersionControl
{
    public class Branch
    {
        public readonly Guid Guid;
        public Commit LastCommit => _lastCommit;

        private Commit _lastCommit;

        public Branch()
        {
            Guid = Guid.NewGuid();
            _lastCommit = null;
        }

        public Branch(Commit lastCommit)
        {
            Guid = Guid.NewGuid();
            _lastCommit = lastCommit;
        }

        public void AddNewCommit(Commit newCommit)
        {
            if(_lastCommit == null)
            {
                _lastCommit = newCommit;
                return;
            }

            newCommit.Previous = _lastCommit;
            _lastCommit.Next.Add(newCommit);
            _lastCommit = newCommit;
        }
    }
}
