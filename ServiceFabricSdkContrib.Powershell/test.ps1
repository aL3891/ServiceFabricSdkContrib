Connect-ServiceFabricCluster
ipmo .\ServiceFabricSdkContrib.Powershell.dll

cd ..\..\..\..\TestSolution

msbuild.exe

ConvertTo-ServiceFabricApplicationDiffPackage TestApplication1\pkg\Debug
ConvertTo-ServiceFabricApplicationDiffPackage TestApplication2\pkg\Debug
ConvertTo-ServiceFabricApplicationDiffPackage TestApplication3\pkg\Debug

Publish-ServiceFabricSolution @{ 
	"TestApplication1" = @{ PackagePath = "TestApplication1\pkg\Debug"; ParameterFilePath ="TestApplication1\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication2" = @{ PackagePath = "TestApplication2\pkg\Debug"; ParameterFilePath ="TestApplication2\ApplicationParameters\Local.1Node.xml" } 
	"TestApplication3" = @{ PackagePath = "TestApplication3\pkg\Debug"; ParameterFilePath ="TestApplication3\ApplicationParameters\Local.1Node.xml" } 
	
}

#.\scripts\Deploy-FabricApplication.ps1 -PublishProfileFile PublishProfiles\Local.1Node.xml -ApplicationPackagePath pkg\Debug -SkipPackageValidation
#}
#else{
#	Write-Host "Already up to date"
#}

