using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Scripts.VersionControl
{
    public class VersionController
    {
        public Commit CurrentCommit => _currentCommit;

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
            if(newCommit.Equals(_currentCommit))
            {
                return;
            }

            if(_currentCommit.Next.Count == 0)
            {
                _currentBranch.AddNewCommit(newCommit);
                _currentCommit = _currentBranch.LastCommit;
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
            _currentCommit = commit;

            _currentBranch = new Branch(_currentCommit);
            _branches.Add(_currentBranch);
        }

        public void SwitchBranch(Guid guid)
        {
            _currentBranch = _branches.Find((Branch branch) => branch.Guid == guid);
            _currentCommit = _currentBranch.LastCommit;
        }

        public Commit GetPreviousCommit()
        {
            _currentCommit = _currentCommit.Previous;
            return _currentCommit;
        }

        public Commit GetNextCommit(Guid branchGuid)
        {
            //TODO: return next commit depending on current branch or choice of the user

            _currentCommit = _currentCommit.Next.First(); 
            return _currentCommit;
        }
    }
}
