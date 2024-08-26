using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using KristofferStrube.Blazor.Window.Extensions;
using Microsoft.JSInterop;
using static System.Text.Json.JsonSerializer;

namespace KristofferStrube.Blazor.Window;

/// <summary>
/// <see cref="Event"/> that holds information about some error.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#errorevent">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class ErrorEvent : Event, IJSCreatable<ErrorEvent>
{
    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.Window library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> windowHelperTask;

    /// <summary>
    /// Creates an <see cref="ErrorEvent"/> using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="type">The type of the <see cref="Event"/>.</param>
    /// <param name="eventInitDict">Extra options for setting options specific to the the <see cref="ErrorEvent"/>.</param>
    /// <returns>A new instance of an <see cref="ErrorEvent"/>.</returns>
    public static async Task<ErrorEvent> CreateAsync(IJSRuntime jSRuntime, string type, ErrorEventInit? eventInitDict = null)
    {
        await using IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructErrorEvent", type, eventInitDict);
        return new ErrorEvent(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc/>
    public static new async Task<ErrorEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<ErrorEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ErrorEvent(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected ErrorEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        windowHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// The error message.
    /// </summary>
    public async Task<string> GetMessageAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "message");
    }

    /// <summary>
    /// The URL of the script in which the error originally occurred.
    /// </summary>
    public async Task<string> GetFileNameAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "filename");
    }

    /// <summary>
    /// The line number where the error occurred in the script.
    /// </summary>
    /// <returns></returns>
    public async Task<ulong> GetLineNumberAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "lineno");
    }

    /// <summary>
    /// The column number where the error occurred in the script.
    /// </summary>
    /// <returns></returns>
    public async Task<ulong> GetColumnNumberAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<ulong>("getAttribute", JSReference, "colno");
    }

    /// <summary>
    /// The object representing the error (e.g., the exception object in the case of an uncaught exception).
    /// </summary>
    /// <returns></returns>
    public async Task<IJSObjectReference> GetErrorAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "error");
    }

    /// <summary>
    /// The object representing the error (e.g., the exception object in the case of an uncaught exception) as a <see cref="WebIDLException"/>.
    /// </summary>
    /// <param name="errorMapper">
    ///     A dictionary that maps from error names to a creator method that takes the name, message, stack trace, and inner exception and creates a new <see cref="WebIDLException"/>.
    ///     Can be used to add handlers for additional JS error types.
    ///     If the default of <see langword="null"/> is used then it is initialized to a value equivalent to <see cref="ErrorMappers.Default"/>.
    /// </param>
    /// <param name="extraErrorProperties">Extra properties that should be mapped for errors.</param>
    /// <returns>A <see cref="WebIDLException"/> if the error of the event is an Error type which has a mapping in <paramref name="errorMapper"/>; Else it returns <see langword="null"/>.</returns>
    public async Task<WebIDLException?> GetErrorAsExceptionAsync(Dictionary<string, Func<JSError, WebIDLException>>? errorMapper = null, string[]? extraErrorProperties = null)
    {
        errorMapper ??= new(ErrorMappers.Default);

        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "error");
        string serializedError = await helper.InvokeAsync<string>("formatError", jSInstance, extraErrorProperties);

        JSError? error = null;

        try
        {
            error = Deserialize<JSError>(serializedError);
        }
        catch
        {
        }

        if (error is null)
        {
            return null;
        }

        if (errorMapper.TryGetValue(error.Name, out Func<JSError, WebIDLException>? creator))
        {
            return creator(error);
        }
        else
        {
            return new WebIDLException($"{error.Name}: \"{error.Message}\"", error.Stack, default!);
        }
    }
}
