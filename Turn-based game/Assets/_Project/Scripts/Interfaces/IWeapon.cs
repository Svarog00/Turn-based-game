namespace Assets._Project.Scripts.Entity
{
    public interface IWeapon
    {
        public int Damage { get; }
        public float AttackRange { get; }

        public void Attack(IHealth target);
    }
}