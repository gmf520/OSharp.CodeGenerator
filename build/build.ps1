function GetVersion()
{
    $file = "version.props"
    $xml = New-Object -TypeName XML
    $xml.Load($file)
    $version = $xml.Project.PropertyGroup.Version
    if($version.contains("VersionSuffixVersion"))
    {
        $version = "{0}.{1}{2}{3}" -f $xml.Project.PropertyGroup.VersionMain,$xml.Project.PropertyGroup.VersionPrefix,$xml.Project.PropertyGroup.VersionSuffix,$xml.Project.PropertyGroup.VersionSuffixVersion
    }
    else
    {
        $version = "{0}.{1}" -f $xml.Project.PropertyGroup.VersionMain,$xml.Project.PropertyGroup.VersionPrefix
    }
    return $version
}

$rootPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Write-Host ("��ǰĿ¼��{0}" -f $rootPath)
$version = GetVersion
Write-Host ("��ǰ�汾��{0}" -f $version)

$output = ".\output"
if(Test-Path $output)
{
    Remove-Item ("{0}\*.*" -f $output)
    Write-Host ("��� {0} �ļ���" -f $output)
}
else
{
    New-Item -Path . -Name $nupkgs -ItemType "directory" -Force
    Write-Host ("���� {0} �ļ���" -f $output)
}

$props = @("OSharp.CodeGenerator")
foreach($prop in $props)
{
    $path = ("../src/{0}/{0}.csproj" -f $prop)
    dotnet publish $path -c Release --force --output $output
}
