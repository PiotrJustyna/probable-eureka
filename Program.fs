module SampleFSharpWorker.App

open System
open System.Reflection
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Serilog
open Serilog.Context
open Serilog.Configuration
open Serilog.Exceptions
open Serilog.Sinks.File
open Elastic.CommonSchema.Serilog

let configureLogging () =
  let environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

  let configurationBuilder = new ConfigurationBuilder()

  let configuration =
    configurationBuilder
      .AddJsonFile("appsettings.json", false, true)
      .AddJsonFile($"appsettings.{environment}.json", true, true)
      .Build()

  let loggerEnrichmentConfiguration = new LoggerConfiguration()

  Log.Logger <-
    loggerEnrichmentConfiguration
      .Enrich.FromLogContext()
      .Enrich.WithExceptionDetails()
      .Enrich.WithProperty("Environment", environment)
      .WriteTo.Debug(new EcsTextFormatter())
      .WriteTo.Console(new EcsTextFormatter())
      .WriteTo.File(
        new EcsTextFormatter(),
        "/tmp/worker/log.json",
        ?rollingInterval = Some RollingInterval.Minute,
        ?retainedFileCountLimit = Some 3)
      .ReadFrom.Configuration(configuration)
      .CreateLogger()

  ()

[<EntryPoint>]
let main argv =
  let hostBuilder = Host.CreateDefaultBuilder(argv)

  configureLogging ()

  let host =
    hostBuilder
      .ConfigureServices(fun hostContext services -> 
        services.AddHostedService<SampleFSharpWorker.Workers.Worker>() |> ignore
      )
      .UseSerilog()
      .Build()
      .Run()
  
  0