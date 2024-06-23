export function getAttribute(object, attribute) { return object[attribute]; }

export function setAttribute(object, attribute, value) { object[attribute] = value; }

// This is copied from Blazor.WebIDL as we should not depend on JS files from other packages as they could change.
export function formatError(error, extraErrorProperties) {
    var name = error.name;
    if (error instanceof DOMException && name == "SyntaxError") {
        name = "DOMExceptionSyntaxError";
    };
    let copy = {
        name: name,
        message: error.message,
        stack: error.stack,
    };
    extraErrorProperties?.forEach(property => copy[property] = error[property]);
    return JSON.stringify(copy);
}