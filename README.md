![example workflow](https://github.com/kwtc/persistence/actions/workflows/dotnet.yml/badge.svg)

# Persistence
A collection of tools and utilities for working with persistence in .NET using Dapper.

## Features
- Basic implementations of MsSql-, MySql- and Sqlite connection factories

## Instructions

Generate new nuget package (from PowerShell) with Nuke build automation
.\build.ps1 Pack

Publish nuget package
dotnet nuget push ".\output\Kwtc.Persistence.0.0.1.nupkg" --api-key {personal access token} --source "github"