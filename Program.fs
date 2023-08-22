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
open Serilog.Sinks.Elasticsearch

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
      .WriteTo.Debug()
      .WriteTo.Console()
      .Enrich.WithProperty("Environment", environment)
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
