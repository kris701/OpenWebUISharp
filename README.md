<p align="center">
    <img src="https://github.com/user-attachments/assets/d94133c2-bdf1-477b-a3b7-88a62e16e538" width="200" height="200" />
</p>

[![Build and Publish](https://github.com/kris701/OpenWebUISharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/kris701/OpenWebUISharp/actions/workflows/dotnet.yml)
![Nuget](https://img.shields.io/nuget/v/OpenWebUISharp)
![Nuget](https://img.shields.io/nuget/dt/OpenWebUISharp)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/kris701/OpenWebUISharp/main)
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/m/kris701/OpenWebUISharp)
![Static Badge](https://img.shields.io/badge/Platform-Windows-blue)
![Static Badge](https://img.shields.io/badge/Platform-Linux-blue)
![Static Badge](https://img.shields.io/badge/Framework-dotnet--10.0-green)


# OpenWebUISharp

This is a simple wrapper project to make a C# wrapper for some of the API calls to [OpenWebUI](https://openwebui.com/).

This is not a full wrapper of all possible API calls, just more or less the ones i use.

## Usage
There are currently four areas of OpenWebUI that this wrapper covers:
* Querying (ChatCompletions format) [`IQueryWrapper`]
* Knowledgebase [`IKnowledgebaseWrapper`]
* Models [`IModelsWrapper`]
* Tools [`IToolWrapper`]

You can either use each area through its own wrapper or through the `IOpenWebUIWrapper` wrapper.

## Example

To query a given model (in this case `gemma3:1b`):
```csharp
var wrapper = new QueryWrapper("<token>", "<url>");
var response = await wrapper.Query(
    "How are you today?"
    "gemma3:1b");
```
This will return a `ConversationMessage` model that contains the response and
information on potential knowledgebase files used.

## Tests
To run the tests, you need to run `docker compose up` at the root of this project

And make sure you create a user in there and replace the API keys in the test files with a new valid one.
