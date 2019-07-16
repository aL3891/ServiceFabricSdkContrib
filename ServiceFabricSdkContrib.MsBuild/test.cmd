dotnet build-server shutdown
dotnet publish -r win-x64 
#SET MSBUILDDEBUGONSTART=2
dotnet msbuild /t:Publish /nodereuse:false ..\TestSolution2\TestApplication1\TestApplication1.sfproj /p:ServiceFabricEndpoint="http://localhost:19080" /p:ServiceFabricThumbPrint="" /p:ServiceFabricParametersFile="ApplicationParameters\Local.1Node.xml"


#dotnet msbuild /t:Publish /nodereuse:false ..\TestSolution2\TestSolution.sln /p:ServiceFabricEndpoint="http://localhost:19080" /p:ServiceFabricThumbPrint="" /p:ServiceFabricParametersFile="ApplicationParameters\Local.1Node.xml"
