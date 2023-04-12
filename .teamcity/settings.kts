// This file is automatically generated when you do `Build.ps1 prepare`.

import jetbrains.buildServer.configs.kotlin.v2019_2.*

// Both Swabra and swabra need to be imported
import jetbrains.buildServer.configs.kotlin.v2019_2.buildFeatures.sshAgent
import jetbrains.buildServer.configs.kotlin.v2019_2.buildFeatures.Swabra
import jetbrains.buildServer.configs.kotlin.v2019_2.buildFeatures.swabra
import jetbrains.buildServer.configs.kotlin.v2019_2.buildSteps.powerShell
import jetbrains.buildServer.configs.kotlin.v2019_2.triggers.*

version = "2021.2"

project {

   buildType(DebugBuild)
   buildType(PublicBuild)
   buildType(PublicDeployment)
   buildType(PublicDeploymentNoDependency)
   buildType(PublicUpdateSearch)
   buildType(PublicUpdateSearchNoDependency)
   buildTypesOrder = arrayListOf(DebugBuild,PublicBuild,PublicDeployment,PublicDeploymentNoDependency,PublicUpdateSearch,PublicUpdateSearchNoDependency)
}

object DebugBuild : BuildType({

    name = "Build [Debug]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/AssemblyLocator/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTime/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTimeTroubleshooting/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CrashReports/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Extract/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/ExtractExceptions/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Logs/**/*=>logs"

    vcs {
        root(DslContext.settingsRoot)

  root(AbsoluteId("Metalama_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")

        }

    steps {
        // Step to kill all dotnet or VBCSCompiler processes that might be locking files we delete in during cleanup.
        powerShell {
            name = "Kill background processes before cleanup"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools kill")
        }
        powerShell {
            name = "Build [Debug]"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "test --configuration Debug --buildNumber %build.number% --buildType %system.teamcity.buildType.id%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {

        dependency(AbsoluteId("Metalama_Metalama_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaBackstage_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Backstage"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaCompiler_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaExtensions_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Extensions"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaLinqPad_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.LinqPad"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

        dependency(AbsoluteId("Metalama_Migration_MetalamaMigration_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Migration"
            }

        }

     }

})

object PublicBuild : BuildType({

    name = "Build [Public]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/AssemblyLocator/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTime/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTimeTroubleshooting/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CrashReports/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Extract/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/ExtractExceptions/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Logs/**/*=>logs"

    vcs {
        root(DslContext.settingsRoot)

  root(AbsoluteId("Metalama_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")

        }

    steps {
        // Step to kill all dotnet or VBCSCompiler processes that might be locking files we delete in during cleanup.
        powerShell {
            name = "Kill background processes before cleanup"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools kill")
        }
        powerShell {
            name = "Build [Public]"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "test --configuration Public --buildNumber %build.number% --buildType %system.teamcity.buildType.id%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {

        dependency(AbsoluteId("Metalama_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaBackstage_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Backstage"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaExtensions_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Extensions"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaLinqPad_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.LinqPad"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

        dependency(AbsoluteId("Metalama_Migration_MetalamaMigration_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Migration"
            }

        }

     }

})

object PublicDeployment : BuildType({

    name = "Deploy [Public]"

    type = Type.DEPLOYMENT

    vcs {
        root(DslContext.settingsRoot)

  root(AbsoluteId("Metalama_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")

        }

    steps {
        powerShell {
            name = "Deploy [Public]"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "publish --configuration Public")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    dependencies {

        dependency(AbsoluteId("Metalama_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaBackstage_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Backstage"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaExtensions_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Extensions"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaLinqPad_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.LinqPad"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaLinqPad_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

        dependency(AbsoluteId("Metalama_Migration_MetalamaMigration_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Migration"
            }

        }

        dependency(AbsoluteId("Metalama_Migration_MetalamaMigration_PublicDeployment")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

        dependency(PublicBuild) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults"
            }

        }

     }

})

object PublicDeploymentNoDependency : BuildType({

    name = "Standalone Deploy [Public]"

    type = Type.DEPLOYMENT

    vcs {
        root(DslContext.settingsRoot)

  root(AbsoluteId("Metalama_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")

        }

    steps {
        powerShell {
            name = "Standalone Deploy [Public]"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "publish --configuration Public")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    dependencies {

        dependency(AbsoluteId("Metalama_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaBackstage_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Backstage"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaExtensions_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Extensions"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaLinqPad_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.LinqPad"
            }

        }

        dependency(AbsoluteId("Metalama_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }

        }

        dependency(AbsoluteId("Metalama_Migration_MetalamaMigration_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Migration"
            }

        }

        dependency(PublicBuild) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults"
            }

        }

     }

})

object PublicUpdateSearch : BuildType({

    name = "Update Search [Public]"

    type = Type.DEPLOYMENT

    vcs {
        root(DslContext.settingsRoot)

  root(AbsoluteId("Metalama_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")

        }

    steps {
        powerShell {
            name = "Update Search [Public]"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools search update https://0fpg9nu41dat6boep.a1.typesense.net metalamadoc https://doc-production.metalama.net/sitemap.xml --ignore-tls")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    dependencies {

        dependency(PublicDeployment) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

     }

})

object PublicUpdateSearchNoDependency : BuildType({

    name = "Standalone Update Search [Public]"

    type = Type.DEPLOYMENT

    vcs {
        root(DslContext.settingsRoot)

  root(AbsoluteId("Metalama_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")

        }

    steps {
        powerShell {
            name = "Standalone Update Search [Public]"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools search update https://0fpg9nu41dat6boep.a1.typesense.net metalamadoc https://doc-production.metalama.net/sitemap.xml --ignore-tls")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    dependencies {

        dependency(PublicDeploymentNoDependency) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }


        }

     }

})

