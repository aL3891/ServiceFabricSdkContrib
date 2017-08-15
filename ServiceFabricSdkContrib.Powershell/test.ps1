Connect-ServiceFabricCluster
ipmo .\bin\Debug\net462\ServiceFabricSdkContrib.Powershell.dll

msbuild.exe ..\TestSolution

ConvertTo-ServiceFabricApplicationDiffPackage ..\TestSolution\TestApplication1\pkg\Debug
ConvertTo-ServiceFabricApplicationDiffPackage ..\TestSolution\TestApplication2\pkg\Debug
ConvertTo-ServiceFabricApplicationDiffPackage ..\TestSolution\TestApplication3\pkg\Debug

Publish-ServiceFabricSolution @{ 
	"TestApplication1" = @{ PackagePath = "..\TestSolution\TestApplication1\pkg\Debug"; ParameterFilePath ="..\TestSolution\TestApplication1\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication2" = @{ PackagePath = "..\TestSolution\TestApplication2\pkg\Debug"; ParameterFilePath ="..\TestSolution\TestApplication2\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication3" = @{ PackagePath = "..\TestSolution\TestApplication3\pkg\Debug"; ParameterFilePath ="..\TestSolution\TestApplication3\ApplicationParameters\Local.1Node.xml" } 
	
}

#.\scripts\Deploy-FabricApplication.ps1 -PublishProfileFile PublishProfiles\Local.1Node.xml -ApplicationPackagePath pkg\Debug -SkipPackageValidation
#}
#else{
#	Write-Host "Already up to date"
#}

