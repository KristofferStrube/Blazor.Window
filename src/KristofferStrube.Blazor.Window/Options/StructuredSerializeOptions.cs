using KristofferStrube.Blazor.WebIDL;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Window.Options;

public class StructuredSerializeOptions
{
    /// <summary>
    /// Objects listed in this property are transferred, not just cloned, meaning that they are no longer usable on the sending side.
    /// </summary>
    [JsonPropertyName("transfer")]
    public ITransferable[] Transfer { get; set; } = Array.Empty<ITransferable>();
}
