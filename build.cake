#reference "tools/AutoCake.Maven/tools/AutoCake.Maven.dll"
#load      "tools/AutoCake.Maven/tools/tasks.cake"

CreateDirectory("build-artefacts/maven");
CreateDirectory("build-artefacts/local-repo");

//// Uncomment the line below to redirect maven to use a local directory to store artefacts. This
//// Makes sure your build starts with a clean cache.
//
// MavenActions.Settings.Properties.Add("maven.repo.local", "build-artefacts/local-repo");

var buildType = Argument("buildType", "development");
var targetDir = Argument("targetdir", "build-artefacts/current-build");
CreateDirectory(targetDir);

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
   .Does(() => {

     MavenActions.RunMaven("clean");

     if (buildType == "development") {
       MavenActions.RunMaven("install");
     }
     else {
       MavenActions.RunMaven("deploy");
     }

     // This copies all created jar-files from the various module directories
     // into a central location. It makes it easier to harvest the results.
     CopyFiles("**/target/*.jar", targetDir);
   });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
RunTarget(target);
