# Run from a Visual Studio Developer PowerShell at the repository root.
$ErrorActionPreference = 'Stop'
msbuild CPC02.sln /t:Build /p:Configuration=Debug /nologo /v:minimal
if ($LASTEXITCODE -ne 0) { throw 'Project build failed.' }
node tests/esg-calculation.test.cjs
if ($LASTEXITCODE -ne 0) { throw 'Frontend regression checks failed.' }
$esgTestExecutable = Join-Path $PSScriptRoot '../CPC02/bin/EsgRegressionTests.exe'
try {
    csc /nologo /out:$esgTestExecutable /reference:CPC02/bin/CPC02.dll /reference:CPC02/bin/System.Web.Mvc.dll /reference:System.Web.dll /reference:System.Data.dll /reference:CPC02/bin/EntityFramework.dll tests/EsgRegressionTests.cs tests/WasteQueryTranslationTests.cs tests/TrafficQueryTests.cs tests/CommutingFactorTests.cs
    if ($LASTEXITCODE -ne 0) { throw 'Backend regression checks did not compile.' }
    & $esgTestExecutable
    if ($LASTEXITCODE -ne 0) { throw 'Backend regression checks failed.' }
}
finally {
    if (Test-Path -LiteralPath $esgTestExecutable) { Remove-Item -LiteralPath $esgTestExecutable }
}
