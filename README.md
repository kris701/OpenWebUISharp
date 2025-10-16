<p align="center">
    <img src="https://github.com/user-attachments/assets/d94133c2-bdf1-477b-a3b7-88a62e16e538" width="200" height="200" />
</p>

[![Build and Publish](https://github.com/kris701/OpenWebUISharp/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/kris701/OpenWebUISharp/actions/workflows/dotnet-desktop.yml)
![Nuget](https://img.shields.io/nuget/v/OpenWebUISharp)
![Nuget](https://img.shields.io/nuget/dt/OpenWebUISharp)
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/kris701/OpenWebUISharp/main)
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/m/kris701/OpenWebUISharp)
![Static Badge](https://img.shields.io/badge/Platform-Windows-blue)
![Static Badge](https://img.shields.io/badge/Platform-Linux-blue)
![Static Badge](https://img.shields.io/badge/Framework-dotnet--9.0-green)


# OpenWebUISharp

This is a simple wrapper project to make a C# wrapper for some of the API calls to [OpenWebUI](https://openwebui.com/).

This is not a full wrapper of all possible API calls, just more or less the ones i use.

It is designed for OpenWebUI v0.6.33.

## Tests
To run the tests, you need to have the following docker container running:

`docker run -d -p 3030:8080 --gpus=all -e ENV=dev -v ollama:/root/.ollama -v open-webui:/app/backend/data --name openwebuiharp ghcr.io/open-webui/open-webui:ollama`

And make sure you create a user in there and replace the API keys in the test files with a new valid one.
