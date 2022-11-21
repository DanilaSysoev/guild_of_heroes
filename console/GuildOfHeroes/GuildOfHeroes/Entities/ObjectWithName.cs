namespace GuildOfHeroes.Entities
{
    public class ObjectWithName
    {
        public string Name { get; private set; }

        protected ObjectWithName(string name)
        {
            Name = name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
