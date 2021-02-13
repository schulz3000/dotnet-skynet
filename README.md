# dotnet-skynet
SDK for integrating Skynet into dotnet applications

## Build
![dotnet-skynet](https://github.com/schulz3000/dotnet-skynet/workflows/.NET/badge.svg)

## NuGet
[![dotnet-skynet](https://img.shields.io/nuget/v/dotnet-skynet.svg?style=flat)](https://www.nuget.org/packages/dotnet-skynet)

## Work in Progress
- [ ] NuGet Package
- [ ] CI Pipeline
- [ ] Directory upload
- [ ] Better Documentation (incl. inline docs)

## How to use

```
using var client = new SkynetClient();

var blocklists = await client.GetBlocklists();

var stats = await client.GetStatistic();

var metadata = await client.GetFileMetadata("IADR9tvmKSzmY-i0Bfyd8mXgaGUZmuQDsbimvgjnFQXIhQ");

var downloadMetadata = await client.Download("IADR9tvmKSzmY-i0Bfyd8mXgaGUZmuQDsbimvgjnFQXIhQ", @"c:\temp\sia");

var response = await client.Upload(@"c:\temp\sia\sia-lm.png");

Console.WriteLine(response.Skylink);
```
