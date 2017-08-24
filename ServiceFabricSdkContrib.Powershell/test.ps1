Connect-ServiceFabricCluster
ipmo .\bin\Debug\net462\ServiceFabricSdkContrib.Powershell.dll

msbuild.exe ..\TestSolution /bl /noconsolelogger /m /t:Restore
msbuild.exe ..\TestSolution /bl /noconsolelogger /m

ConvertTo-ServiceFabricApplicationDiffPackage @("..\TestSolution\TestApplication1\pkg\Debug", "..\TestSolution\TestApplication2\pkg\Debug", "..\TestSolution\TestApplication3\pkg\Debug")


Publish-ServiceFabricSolution @{ 
	"TestApplication1" = @{ PackagePath = "..\TestSolution\TestApplication1\pkg\Debug"; ParameterFilePath ="..\TestSolution\TestApplication1\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication2" = @{ PackagePath = "..\TestSolution\TestApplication2\pkg\Debug"; ParameterFilePath ="..\TestSolution\TestApplication2\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication3" = @{ PackagePath = "..\TestSolution\TestApplication3\pkg\Debug"; ParameterFilePath ="..\TestSolution\TestApplication3\ApplicationParameters\Local.1Node.xml" } 
}
