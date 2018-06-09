dotnet restore
dotnet publish /p:Configuration=Release
dotnet pack /p:NuspecFile=ServiceFabricSdkContrib.Applications.nuspec /p:Configuration=Release
dotnet pack /p:NuspecFile=ServiceFabricSdkContrib.Services.nuspec /p:Configuration=Release