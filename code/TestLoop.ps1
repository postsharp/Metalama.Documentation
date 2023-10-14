for ($i = 1; $i -le 100; $i++) {
    
    $currentTime = Get-Date
    Write-Host "Running iteration $i at $currentTime"
    
    dotnet test C:\src\Metalama.Documentation\code\Metalama.Documentation.Snippets.TestBased.sln

    $process = Start-Process -FilePath "dotnet" -ArgumentList "test C:\src\Metalama.Documentation\code\Metalama.Documentation.Snippets.TestBased.sln" -Wait -PassThru

    if ($process.ExitCode -ne 0) {
        Write-Host "Program returned a non-zero value. Exiting loop."
        break
    }

}

Write-Host "Loop finished."
