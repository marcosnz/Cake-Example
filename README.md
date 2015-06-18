#TL;DR
To see how easy it is to get a build running with [Cake](https://github.com/cake-build/cake)

* Clone this repo (which has no Cake files)

* Run this powershell  to get the 2 files required to bootstrap the build:
	```PowerShell

	"build.ps1","build.cake"|%{Invoke-RestMethod -Uri "https://raw.githubusercontent.com/cake-build/bootstrapper/master/res/scripts/$($_)" -OutFile $_}
	```
* Run the PS script ./Build.ps1


Note, you don't *have* to use powershell to use Cake. I just love that I can add 2 small files to my repo and that's all I need for the build process to work.

#The slightly longer version

Here's some info about [Cake](https://github.com/cake-build/cake):

Cake (C# Make) is a build automation system with a C# DSL to do things like compiling code, copy files/folders, running unit tests, compress files and build NuGet packages.

[![Build status](https://ci.appveyor.com/api/projects/status/c6lw0vvj1mf4395a/branch/develop?svg=true)](https://ci.appveyor.com/project/patriksvensson/cake/branch/develop)
[![Coverity Scan](https://scan.coverity.com/projects/4147/badge.svg)](https://scan.coverity.com/projects/4147) 

[![Follow @cakebuildnet](https://img.shields.io/badge/Twitter-Follow%20%40cakebuildnet-blue.svg)](https://twitter.com/intent/follow?screen_name=cakebuildnet)

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)

[![Join the chat at https://gitter.im/cake-build/cake](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/cake-build/cake?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

##So what's this Repo?

I have just started using Cake and it took me a little (not long) time to figure out what I needed to start my build process. It turns out that it's hardly anything and the steps are simple. This is my attempt to document that.

This repo has a very simple Hello World C# Console solution with a Nuget dependency (log4net). It has *no* build scripts or *any* Cake-related files.

By following these steps you can have it building with Cake in a few minutes

###1. Clone this repo 
if you haven't already.
###2. Get the required files
You need 2 files for a build (these will all you'll have to commit to you repo to use Cake):
* build.ps1
  * This is a bootstrapper powershell script that ensures you have Cake and required dependencies installed.
* build.cake
  * This is the actual build script. It doesn't have to be named this but this will be found by default.

An easy way to get these files is to download them using powershell. 
Open powershell, CD to the root of your repo and execute this command (this will not execute the powershell script, just download it):
```PowerShell
"build.ps1","build.cake"|%{Invoke-RestMethod -Uri "https://raw.githubusercontent.com/cake-build/bootstrapper/master/res/scripts/$($_)" -OutFile $_}
```
This downloads the files from [Cake Bootstrapper](https://github.com/cake-build/bootstrapper)
Other ways to get the files can be found on the [Getting Started](http://cakebuild.net/getting-started/) page on [cakebuild.net](http://cakebuild.net)
###3. Run the build script
Still in powershell execute the script. 
```PowerShell
.\build.ps1
```

The script will detect that you don't have cake and download it. It will then run the very simple build.cake script that finds any .sln in files in or below the current folder, restores any NuGet dependencies, and builds them.


Congratulations, you've run you first Cake script!

###4. Bonus points! - run the tests
The script is a fairly bare-bones implementation. But extending it is easy. For instance to run the extensive unit tests for the awesome application you need to add a test target:
```CSharp
Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    MSTest("./src/**/bin/" + configuration + "/*Test.dll");
});
```
This is using MSTest but out of the box you have XUnit and NUnit test helpers as well.

Adding the target doesn't necessarily run it unless another target is dependent on it or you call it explicitly.

In our case we can just change the default task to be dependent on our test task (which in turn is dependent on the build task):
```CSharp
Task("Default")
    .IsDependentOn("Run-Unit-Tests");
```
Running the build now will run the tests after building...
```PowerShell
.\build.ps1
```
