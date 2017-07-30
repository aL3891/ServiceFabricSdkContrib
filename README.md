# Service fabric Sdk Contrib
A set of msbuild targets and powershell cmdlets to make working with service fabric easier


## Installation
Powershell:
    
    Install-Module ServiceFabricSdkContrib

Nuget	

    Install-Package ServiceFabricSdkContrib.Services -Pre 

for services and 

    Install-Package ServiceFabricSdkContrib.Applications -Pre 

for application projects

Note, The current Visual studio tooling for sf projects is not really set up for nuget, so you may need to manually add a reference to the target file inside your sfproj file, something like this:

    <Import Project="..\packages\ServiceFabricSdkContrib.Applications.0.0.4-beta\build\ServiceFabricSdkContrib.Applications.targets" Condition="Exists('..\packages\ServiceFabricSdkContrib.Applications.0.0.4-beta\build\ServiceFabricSdkContrib.Applications.targets')" />

Also, by default the sfproj projects are not included in the standard build configuration, you need to in the solution configuration to create application packages during build.
Finally, Creating symlinks in windows requires administrator rights, so you must run visual studio/msbuild with elevated privileges for the packaging to work

## Feature summary

### Service features
* Generate hashes for all packages during build and update the service manifest

### Application features
* Creates symlinks for service files during build in the package folder
* Updates the application manifest with the versions found in each service manifest

### Powershell features
* Adds the ConvertTo-ServiceFabricApplicationDiffPackage that checks the connected cluster for exsisting packages and removes them from the local package

## Feature details

### Package hashing

In service fabric, each service and package has a unique version to enable side by side deployment of diffrent versions as well as upgrades. However making sure to update
the versions of only the services that has changed and then making sure to update the versions of the corresponding service and application manifest can be a hassle. With the 
targets in this package, the version of each package is calculated based on a sha hash, then all the package versions are hashed again in the service version. During build of the application project, 
all the service versions are hashed into the application type version. The hashes are appended to the version already present in the manifest during 
build and stored in the obj folder in order to not require constant commits to source control. 

### Application package symlinks

During the regular package process, all the services output files are copied to the package folder (pkg). However this operation hand take a significant amount of time 
if there are alot of services with alot of references. The new build target creates symlinks instead of copies witch is practically instant

### Create differential package

Service fabric supports deployment of differential packages that only contain the services and packagaes that have not been previously deployed to the cluster, however the standard
sdk does not provide a way to create such a package. The service fabric contrib module adds the 

## Future features / improvements

* The PackageSymlink target now also does the version rollup, this should be a separate target
* Add Cmdlet for deploying multiple apps in paralell
* Better error handling in general, add waring for when not running as elevated
* Hashes are now computed on all files, add an option to use a filter or perhaps be smart about package references and hash them separatly 
* Hashing all the binaries can still miss small changes, another option would be to use git commits as version and then only hash the diff of what is not commited
* Add a cleanup cmdlet that checks old service versions (if the current one does not)
* Add a Test-Package cmdlet that can check a remote cluster for exsisting packages, the built in done can only check locally it seems
* Create a proper build task for Actor manifest updates that does not invoke a external exe (for perf)
* Create a build task for actors that allows actors in referenced projects
* Add opt out properties for targets where it makes sense
* Make sure all targets work with incremental builds

Got an idea for a target or cmdlet? Please be sure to make an issue!
