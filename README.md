# probable-eureka

f# + filebeat + elasticsearch + kibana + docker-compose

## communication diagram

Logs are propagated as follows:

```mermaid
stateDiagram-v2
direction lr

state "application runtime" as runtime
state "filebeat" as filebeat
state "elasticsearch" as elasticsearch
state "kibana" as kibana

state "./filebeat.yml" as filebeatyml
state "elasticsearch-data" as elasticsearchdata
state "." as currentdirectory
state "./logs" as logs

state host-os {
  state volumes {
    filebeatyml
    elasticsearchdata
    currentdirectory
    logs
  }

  state containers {
    runtime
    filebeat
    elasticsearch
    kibana
  }
}

filebeatyml --> filebeat
elasticsearch --> elasticsearchdata
currentdirectory --> runtime
runtime --> logs
logs --> filebeat
filebeat --> elasticsearch
elasticsearch --> kibana
```

Rolling log files:

![rolling log files](./img/rolling-file.png)

Kibana:

![kibana](./img/kibana.png)

## how to use

* `./start-development-environment.sh`
* `./stop-development-environment.sh`
* `./build.sh`
* `./run.sh`
* `./nuke-development-environment.sh`

### kibana

* http://localhost:5601/app/discover#/?_g=()
* main menu -> analytics -> discover
* create data view
  * index name
  * index pattern
  * timestamp field
  * save data view to kibana

## resources used

* https://www.elastic.co/blog/getting-started-with-the-elastic-stack-and-docker-compose - looks like filebeat goes directly to elasticsearch. It should be logstash instead.
* https://www.elastic.co/guide/en/beats/filebeat/current/running-on-docker.html - Run Filebeat on Docker
* https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-installation-configuration.html
* https://www.elastic.co/guide/en/beats/filebeat/current/configuring-howto-filebeat.html - Configure Filebeat
* https://www.elastic.co/guide/en/ecs-logging/dotnet/master/setup.html - elastic + filebeat + dotnet, specifically this: https://www.elastic.co/guide/en/ecs-logging/dotnet/master/setup.html#setup-step-3
* https://www.elastic.co/guide/en/ecs-logging/dotnet/master/serilog-formatter.html - serilog formatter
