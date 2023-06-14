using Stedders.Components;

namespace Stedders.Entities
{
    public class Entity
    {
        public List<Component> Components = new();
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; init; }

        public string ShortId() => Id.ToString().Substring(0, 4);
        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        public IEnumerable<T> GetComponents<T>()
        {
            return Components.OfType<T>();
        }
        public bool HasTypes(params Type[] types)
        {
            return types.All(t => Components.Any(c => c.GetType() == t));
        }
    }
}