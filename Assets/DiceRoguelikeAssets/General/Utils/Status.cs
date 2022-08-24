namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Status
    {
        public int hp;
        public int shield;
        public int power;

        public Status(int hp, int shield) : this(hp, shield, 0) { }
        public Status(int hp, int shield, int power)
        {
            this.hp = hp;
            this.shield = shield;
            this.power = power;
        }


        public override string ToString()
        {
            return $"HP: {hp}, Shield: {shield}, Power: {power}";
        }

        public static Status operator +(Status a, Status b)
        {
            return new Status(a.hp + b.hp, a.shield + b.shield, a.power + b.power);
        }

        public static Status operator -(Status a, Status b)
        {
            return new Status(a.hp - b.hp, a.shield - b.shield, a.power - b.power);
        }

        public static Status zero => new Status(0, 0, 0);
    }
}