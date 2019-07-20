dotnet publish
$path = [Environment]::GetEnvironmentVariable("PSModulePath").Split(';')[0]
$path += "\ServiceFabricSdkContrib"

if (Test-Path $path)
{
	Remove-Item $path -Recurse -Force
}

New-Item $path -ItemType Directory  -Force

Copy-Item .\bin\Debug\netstandard2.0\publish\*.* $path -Force