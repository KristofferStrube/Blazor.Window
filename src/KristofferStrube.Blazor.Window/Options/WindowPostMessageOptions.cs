using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Window.Options;

public class WindowPostMessageOptions : StructuredSerializeOptions
{
    /// <summary>
    /// A target origin can be specified using this property. If not provided, it defaults to <c>"/"</c>. This default restricts the message to same-origin targets only.
    /// </summary>
    [JsonPropertyName("targetOrigin")]
    public string TargetOrigin { get; set; } = "/";
}
