# Server-sent events alternative

## What does this do?
- This solution provides a `server-sent events` alternative using the POST method.

## When to use this
- When you need `server-sent events`-like behavior but you want to:
    - Send a larger or complex input as a JSON body to the endpoint
    - Do not want to fight against request length limits
    - Want to semantically be correct with the HTTP verbs and have the need to use POST.


## Implementation
- Backend implementation can be found in `Program.cs`
- Very simple frontend implementation can be found in `index.html` in the `wwwroot` folder.
