using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Window.Options;

/// <summary>
/// The options available when posting a message from a <see cref="Window"/>.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#windowpostmessageoptions">See the API definition here</see>.</remarks>
public class WindowPostMessageOptions : StructuredSerializeOptions
{
    /// <summary>
    /// A target origin can be specified using this property. If not provided, it defaults to <c>"/"</c>. This default restricts the message to same-origin targets only.
    /// </summary>
    [JsonPropertyName("targetOrigin")]
    public string TargetOrigin { get; set; } = "/";
}
