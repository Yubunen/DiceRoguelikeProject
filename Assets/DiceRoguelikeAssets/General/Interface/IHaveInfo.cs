
namespace LSemiRoguelike
{
    public interface IHaveInfo
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public UnityEngine.Sprite Icon { get; }
    }
}