using KristofferStrube.Blazor.DOM;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Window;

/// <summary>
/// The options specific to initializing an <see cref="ErrorEvent"/>. 
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#erroreventinit">See the API definition here</see>.</remarks>
public class ErrorEventInit : EventInit
{
    /// <summary>
    /// It represents the error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    /// <summary>
    /// It represents the URL of the script in which the error originally occurred.
    /// </summary>
    [JsonPropertyName("filename")]
    public string FileName { get; set; } = "";

    /// <summary>
    /// It represents the line number where the error occurred in the script.
    /// </summary>
    [JsonPropertyName("lineno")]
    public ulong LineNumber { get; set; } = 0;

    /// <summary>
    /// It represents the column number where the error occurred in the script.
    /// </summary>
    [JsonPropertyName("colno")]
    public ulong ColumnNumber { get; set; } = 0;

    /// <summary>
    /// Where appropriate, it is set to the object representing the error (e.g., the exception object in the case of an uncaught exception).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("error")]
    public IJSObjectReference? Error { get; set; }
}
