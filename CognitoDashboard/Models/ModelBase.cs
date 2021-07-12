using System.Text.Json;

namespace CognitoDashboard.Models
{
    public abstract class ModelBase
    {
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
