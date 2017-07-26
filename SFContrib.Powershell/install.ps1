$path = [Environment]::GetEnvironmentVariable("PSModulePath").Split(';')[0]
$path += "\SfSdkContrib"

Copy-Item .\SfSdkContrib.psm1 $path