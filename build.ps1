(& dotnet nuget locals http-cache -c) | Out-Null
& dotnet run --project "$PSScriptRoot\eng\src\BuildMetalamaDocumentation.csproj" -- $args
exit $LASTEXITCODE

