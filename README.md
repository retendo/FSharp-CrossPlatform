# FSharp-CrossPlatform
This is a sample F# project that uses the SAFE stack for web frontend/backend and Fabulous/Xamarin for the iOS and Android mobile apps

It demonstrates how you can share a lot of code between the different platforms.

## Get started

1. Clone the repo
2. Follow the instructions in /web/README.md to setup the SAFE stack, except the "fake build -t Run" step
3. Have Visual Studio installed including Xamarin and .NET Core support
4. Open the solution (NewApp.sln) in Visual Studio. The sub-projects need to be restored, otherwise the next step doesn't work. (I don't know why, to be honest...)
5. In your terminal of choice, navigate into the /web folder and do "fake build -t Run" to start the web server/client
6. In Visual Studio start the iOS or Android app
