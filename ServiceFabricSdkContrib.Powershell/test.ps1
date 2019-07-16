dotnet publish
ipmo .\bin\Debug\netstandard2.0\publish\ServiceFabricSdkContrib.Powershell.dll

#dotnet publish ..\TestSolution2\TestSolution.sln

ConvertTo-ServiceFabricApplicationDiffPackage -ClusterEndPoint http://localhost:19080/ @("..\TestSolution2\TestApplication1\pkg\Debug", "..\TestSolution2\TestApplication2\pkg\Debug", "..\TestSolution2\TestApplication3\pkg\Debug") 

Publish-ServiceFabricSolution -ClusterEndPoint http://localhost:19080/ @{ 
	"TestApplication1" = @{ PackagePath = "..\TestSolution2\TestApplication1\pkg\Debug"; ParameterFilePath ="..\TestSolution2\TestApplication1\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication2" = @{ PackagePath = "..\TestSolution2\TestApplication2\pkg\Debug"; ParameterFilePath ="..\TestSolution2\TestApplication2\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication3" = @{ PackagePath = "..\TestSolution2\TestApplication3\pkg\Debug"; ParameterFilePath ="..\TestSolution2\TestApplication3\ApplicationParameters\Local.1Node.xml" } 
} 