if(!(test-path 'variable:testranonce')){
Connect-ServiceFabricCluster
ipmo .\ServiceFabricSdkContrib.Powershell.dll
cd ..\..\..\..\TestSolution\TestSolution
}

msbuild.exe
if(ConvertTo-ServiceFabricApplicationDiffPackage pkg\Debug){
.\scripts\Deploy-FabricApplication.ps1 -PublishProfileFile PublishProfiles\Local.1Node.xml -ApplicationPackagePath pkg\Debug -SkipPackageValidation
}
else{
	Write-Host "Already up to date"
}

