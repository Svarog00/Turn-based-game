using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Assets._Project.Scripts.VersionControl
{
    public class VersionController
    {
        public Commit LastCommit => _lastCommit;
        public Commit CurrentCommit => _currentCommit;
        public List<Branch> Branches => _branches;

        private Commit _lastCommit;
        private Commit _currentCommit;

        private Branch _currentBranch;
        private List<Branch> _branches;

        public VersionController()
        {
            _currentBranch = new Branch();
            _currentCommit = null;

            _branches = new List<Branch>();
            _branches.Add(new Branch());
        }

        public void AddNewCommit(Commit newCommit)
        {
            if (_currentCommit == null)
            {
                _currentBranch.AddNewCommit(newCommit);
                _lastCommit = _currentBranch.LastCommit;
                _currentCommit = _lastCommit;
                return;
            }

            if (newCommit.Equals(_currentCommit))
            {
                return;
            }

            if(_currentCommit.Next.Count == 0)
            {
                _currentBranch.AddNewCommit(newCommit);
                _lastCommit = _currentBranch.LastCommit;
                _currentCommit = _lastCommit;
                return;
            }

            foreach(Commit commit in _currentCommit.Next)
            {
                if(newCommit.Equals(commit))
                {
                    return;
                }
            }

            CreateNewBranch(newCommit);
        }

        public void CreateNewBranch(Commit commit)
        {
            _currentCommit.Next.Add(commit);
            commit.Previous = _currentCommit;
            _lastCommit = commit;

            _currentBranch = new Branch(_lastCommit);
            _branches.Add(_currentBranch);

            _currentCommit = _lastCommit;
        }

        public void SwitchBranch(Guid guid)
        {
            _currentBranch = _branches.Find((Branch branch) => branch.Guid == guid);
            _lastCommit = _currentBranch.LastCommit;
            _currentCommit = _lastCommit;
        }

        public Commit GetPreviousCommit()
        {
            _currentCommit = _currentCommit?.Previous;
            return _currentCommit;
        }

        public Commit GetNextCommit(Guid commitGuid)
        {
            _currentCommit = _currentCommit.Next.Find((Commit commit) => commit.Guid == commitGuid); 
            return _currentCommit;
        }
    }
}
