# Service fabric Sdk Contrib

This is a project for adding functionality to the service fabric sdk that is either missing or could be done in a diffrent way. It's not
ment to replace the standard Sdk and the standard Sdk is ment to take precedence, so features in this project that are made obsolete by
the standard sdk will be removed. This repo is also ment for experimentation and ideas that might get added to the sdk later. Hopefully 
the standard sdk will be open sourced at some point, making it easier to move stable features from here (or elsewhere) to it.

If you have an idea for something that would make your work with service fabric eaiser feel free to raise an issue or open a PR!

## Installation
Powershell:
    
    Install-Module ServiceFabricSdkContrib

Nuget for service projects

    Install-Package ServiceFabricSdkContrib.Services -Pre 
    
and application projects
	
	Install-Package ServiceFabricSdkContrib.Applications -Pre 

Note, The current Visual studio tooling for sf projects is not really set up for nuget, so you may need to manually add a reference to the target file inside your sfproj file, something like this:

    <Import Project="..\packages\ServiceFabricSdkContrib.Applications.0.1.5-beta\build\ServiceFabricSdkContrib.Applications.targets" Condition="Exists('..\packages\ServiceFabricSdkContrib.Applications.0.1.5-beta\build\ServiceFabricSdkContrib.Applications.targets')" />

Also, sfproj projects are not included in the standard build configuration, so you need to modify the solution configuration in order to create application packages during build.
Finally, creating symlinks in windows requires administrator rights, so you must run visual studio/msbuild with elevated privileges for the packaging to work

## Feature summary

### Service features
* Generate versions based on git/hashes for all packages during build and update the service manifest
* Package services individually so that they can later be combined into applications later

### Application features
* Package applications during build
* Updates the application manifest with the versions found in each service manifest

### Powershell features
* Adds the ConvertTo-ServiceFabricApplicationDiffPackage cmdlet that checks the connected cluster for exsisting packages and removes them from the local package
* Adds the Deploy-ServiceFabricApplication cmdlet that uploads, provisions and upgrades applications in paralell  

## Feature details

### Package hashing

In service fabric, each service and package has a unique version to enable side by side deployment of diffrent versions as well as upgrades. However making sure to update
the versions of only the services that has changed and then making sure to update the versions of the corresponding service and application manifest can be a hassle. With the 
targets in this package, the version of each package is calculated based on a sha hash, then all the package versions are hashed again in the service version. During build of the application project, 
all the service versions are hashed into the application type version. The hashes are appended to the version already present in the manifest during 
build and stored in the obj folder in order to not require constant commits to source control. 

### Application package 

During the regular package process, all the services output files are copied to the package folder (pkg). However this operation hand take a significant amount of time 
if there are alot of services with alot of references. The new build target creates hardlinks instead of copies witch is practically instant

### Create differential package

Service fabric supports deployment of differential packages that only contain the services and packagaes that have not been previously deployed to the cluster, however the standard
sdk does not provide a way to create such a package. The service fabric contrib module adds the 

## Future features / improvements

* Better error handling in general, add waring for when not running as elevated
* Hashes are now computed on all files, add an option to use a filter or perhaps be smart about package references and hash them separatly 
* Add a Test-Package cmdlet that can check a remote cluster for exsisting packages, the built in done can only check locally it seems
* Create a proper build task for Actor manifest updates that does not invoke a external exe (for perf)
* Create a build task for actors that allows actors in referenced projects
* Make sure all targets work with incremental builds
* Automatically bump "real" version (that would be checked into source control) based on hash

Got an idea for a target or cmdlet? Please be sure to make an issue!
