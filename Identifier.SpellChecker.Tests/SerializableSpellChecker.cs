using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public class SerializableSpellChecker : IXunitSerializable
    {
        public void Deserialize(IXunitSerializationInfo info) { }
        public void Serialize(IXunitSerializationInfo info) { }
    }
}
