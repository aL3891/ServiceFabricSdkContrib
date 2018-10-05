dotnet build-server shutdown
dotnet publish
msbuild ..\TestSolution2\TestSolution.sln /bl /t:publish /p:ServiceFabricEndpoint="http://localhost:19080" /p:ServiceFabricThumbPrint="" /p:ServiceFabricParametersFile="ApplicationParameters\Local.1Node.xml"
