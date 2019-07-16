dotnet restore
dotnet publish -r win-x64 /p:Configuration=Debug
dotnet publish -r win-x64 /p:Configuration=Release
dotnet pack /p:NuspecFile=ServiceFabricSdkContrib.Applications.nuspec /p:Configuration=Release
dotnet pack /p:NuspecFile=ServiceFabricSdkContrib.Services.nuspec /p:Configuration=Release