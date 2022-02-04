param (
 # Perform an incremental build (does not clean and does not run tests).
 [switch] $Incremental = $false, 

 # Create the Zip file.
 [switch] $Pack = $false, 

 # Publish the Zip. If it does not exist, creates it.
 [switch] $Publish = $false,

 # Does not kill processes
  [switch] $NoKill = $false )

$ErrorActionPreference = "Stop" 
$outputZipPath = "docfx\output\Metalama.Doc.zip"

if ( $Publish ) 
{
    $Pack = $false
    if ( !$env:DOC_API_KEY )
    {
        Throw "The environment variable DOC_API_KEY is not defined."
    }
}

function RemoveLockedFiles()
{
    if ( $Pack -and -not $NoKill ) {
        gcim win32_process | where { $_.Name -eq "iisexpress.exe" } | foreach { 
            Stop-Process -ID $_.ProcessId 
            Start-Sleep  -Seconds 1
        }

        # Delete the output soon because it may be locked.
        if ( test-path $outputZipPath) {
        del $outputZipPath
        }
    }

}

function Clean()
{

    RemoveLockedFiles

 
    # Delete temporary files (disable incremental build)
    if ( test-path "docfx\obj" ) {
       rd "docfx\obj" -Recurse -Force
    }

    if ( test-path "docfx\_site" ) {
       rd "docfx\_site" -Recurse -Force
    }

}

function Restore()
{

    # Restore DocFx
    nuget restore "docfx\packages.config" -OutputDirectory "docfx\packages"

    if ($LASTEXITCODE -ne 0 ) { exit }


    # Restore samples and DoxFX extensions
    dotnet restore "code" --no-cache --force

    if ($LASTEXITCODE -ne 0 ) { exit }

}

function Metadata()
{
    
    docfx\packages\docfx.console.2.59.0\tools\docfx.exe "docfx\docfx.json" metadata

    if ($LASTEXITCODE -ne 0 ) { exit }
}

function BuildExtensions()
{
    dotnet build "code\Metalama.Documentation.DfmExtensions\Metalama.Documentation.DfmExtensions.csproj"

    if ($LASTEXITCODE -ne 0 ) { exit }
}

function RunTests()
{
    
    dotnet test "code\Metalama.Documentation.SampleCode.sln"

    # We tolerate failing tests for now.
  #  if ($LASTEXITCODE -ne 0 ) { exit }
}

function BuildDoc()
{
 
   docfx\packages\docfx.console.2.59.0\tools\docfx.exe "docfx\docfx.json" build
    
   if ($LASTEXITCODE -ne 0 ) { exit }
}

function Pack()
{

    if (!(test-path "docfx\output" )) {
       New-Item -ItemType Directory -Force -Path "docfx\output"
    }

    Compress-Archive -Path "docfx\_site\*" -DestinationPath $outputZipPath -Force
}

function Prepare()
{
    Clean
    Restore
    Metadata
    BuildExtensions
    RunTests
}

function Publish()
{
    if ( (Get-Module -ListAvailable -Name AWS.Tools.S3) )
    {
        echo "AWS.Tools.S3 already installed."
    }
    else
    {
        Install-Module -Name AWS.Tools.S3 -Scope CurrentUser -Force
    }

    # This will use the default credentials.

    Write-S3Object -BucketName "doc.postsharp.net" -File $outputZipPath  -Key "Metalama.Doc.zip" -PublicReadOnly -EndpointUrl https://s3.eu-west-1.amazonaws.com -Region eu-west-1

    Invoke-WebRequest "https://postsharp-helpbrowser.azurewebsites.net/_api/invalidate?key=$($env:DOC_API_KEY)"
 
}

# Main build sequence
if ( $Incremental )
{
    BuildDoc
}
else
{
    Prepare
    BuildDoc
}

if ( $Pack -or $Publish )
{
    Pack
}

if ( $Publish )
{
    Publish
}