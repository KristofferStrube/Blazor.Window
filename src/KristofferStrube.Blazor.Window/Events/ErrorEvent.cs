﻿using KristofferStrube.Blazor.DOM;
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
    /// Extra properties that should be mapped for errors.
    /// </summary>
    public string[]? ExtraErrorProperties { get; set; }

    /// <summary>
    /// A dictionary that maps from error names to a creator method that takes the name, message, stack trace, and inner exception and creates a new <see cref="WebIDLException"/>. Can be used to add handlers for additional JS error types.
    /// <br />
    /// The default value is <see cref="ErrorMappers.Default"/>.
    /// </summary>
    public Dictionary<string, Func<JSError, WebIDLException>> ErrorMapper { get; set; } = new(ErrorMappers.Default);

    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.Window library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> windowHelperTask;

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
    /// <returns>A <see cref="WebIDLException"/> if the error of the event is an Error type which has a mapping in <see cref="ErrorMapper"/>; Else it returns <see langword="null"/>.</returns>
    public async Task<WebIDLException?> GetErrorAsExceptionAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "error");
        string serializedError = await helper.InvokeAsync<string>("formatError", jSInstance, ExtraErrorProperties);

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

        if (ErrorMapper.TryGetValue(error.Name, out Func<JSError, WebIDLException>? creator))
        {
            return creator(error);
        }
        else
        {
            return new WebIDLException($"{error.Name}: \"{error.Message}\"", error.Stack, default!);
        }
    }
}
