# An Apache-Maven based Auto-Cake example 

This little project demonstrates how to use AutoCake to
control a Maven based Java project. 

You can start a release with 

    # PowerShell on Windows
    build.cmd -Script release.cake

or

    # Linux
    build.sh -script release.cake
 

## This Maven sample project

This project is a typical multi-module build providing an API 
bundle, a single implementation and an assembly. It does nothing
except printing out a silly message and exists only to give 
AutoCake something to build.

## Requirements

AutoCake requires that Maven is installed and is available 
on the command search path (ie your ```%Path%``` or ``$PATH`` 
environment variable).

The maven project itself needs to have the versions-maven-plugin
configured so that AutoCake can update the POMs with the version 
numbers it calculates via GitVersion. 

## GitVersion and Maven artefact versioning

Maven uses different version number schemas for development and
release versions. Therefore the AutoCake script adjusts the version 
numbers received from GitVersion to match the common versioning
rules found in Maven based projects.

On the development branch, the builds version will be set to 
``X.Y.Z-SNAPSHOT`` where X is the major, Y the minor and Z is
the patch version as computed by GitVersion. For release candidate
builds the build uses the full version number as there may be many
of these builds during a stablisation phase. 

Once a release is finalized, a plain ``X.Y.Z`` version without 
any suffix is used for the actual release.

## Deployment repositories

Modern Maven versions require that each build specifies the target
repository on the command-line using the ``altDeploymentRepository``
property.

AutoCake-Maven checks for the existence of a MAVEN_ALT_DEPLOY_REPOSITORY
environment variable to set this property. If the variable is there
the value of that variable is used as deployment target.

The variable must contain the repository in the format

    id::layout::url
  
where id is the identifier in your $M2_HOME/settings.xml file that
contains the credentials for the repository.

Layout should be set to ``default`` to tell Maven that the repository
is a Maven 2 repository. Use ``legacy`` in the unlikely case that you
still use Maven 1 repositories.

An typical repository specifier looks like this:

    publish::default::http://localhost:8080/content/repositories/releases

with an matching entry for a ``publish`` server in the settings.xml

    <settings ..>
      ..
      <servers>
        <server>
          <id>publish</id>
          <username>myuser</username>
          <password>mypassword</password>
        </server>
        ..
      </servers>  
      ..
    </settings>
 
## Building

The release.cake script will call the build.cake script to trigger
the actual building. If your script needs to install built artefacts
locally to run correctly, you can use redirect Maven to a local
repository inside the build-artefacts directory. This way your 
release candidate builds do not pollute your main Maven cache with
potentially broken builds.

The build scripts here assume that development builds are not published
to a Maven repository server. The build will install the created
artefacts into the local Maven cache instead.

In Multi-project builds Maven publishes artefacts as soon as they
are built. This can create situations where a later module fails to
build and your Maven repository now contains a partially build 
product. To avoid publishing incomplete build artefacts I recommend
to delay install and deploy to the end of the build process.
  
Use the ``installAtEnd`` and the ``deployAtEnd`` properties to control 
that behavior.

Unlike the C# AutoCake builds, Maven handles both the actual build 
process and the publishing of the artefacts. Therefore the 
build-artefacts directory remains empty.