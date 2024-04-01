using KristofferStrube.Blazor.WebIDL;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Window.Options;

/// <summary>
/// The options available when posting a message across all message post implementations.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#structuredserializeoptions">See the API definition here</see>.</remarks>
public class StructuredSerializeOptions
{
    /// <summary>
    /// Objects listed in this property are transferred, not just cloned, meaning that they are no longer usable on the sending side.
    /// </summary>
    [JsonPropertyName("transfer")]
    public ITransferable[] Transfer { get; set; } = Array.Empty<ITransferable>();
}
