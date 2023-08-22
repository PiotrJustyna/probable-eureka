# probable-eureka

f# + filebeat + elasticsearch + kibana

## status

WIP, but all 4 containers are happily running and I can see filebeat working (not processing any logs yet, though). Here is what I get when I use the filebeat terminal:

```bash
$ filebeat modules list
Enabled:

Disabled:
activemq
apache
auditd
aws
awsfargate
azure
barracuda
bluecoat
cef
checkpoint
cisco
coredns
crowdstrike
cyberarkpas
cylance
elasticsearch
envoyproxy
f5
fortinet
gcp
google_workspace
haproxy
ibmmq
icinga
iis
imperva
infoblox
iptables
juniper
kafka
kibana
logstash
microsoft
misp
mongodb
mssql
mysql
mysqlenterprise
nats
netflow
netscout
nginx
o365
okta
oracle
osquery
panw
pensando
postgresql
proofpoint
rabbitmq
radware
redis
salesforce
santa
snort
snyk
sonicwall
sophos
squid
suricata
system
threatintel
tomcat
traefik
zeek
zookeeper
zoom
zscaler
```

The worked is outputting logs in the ECS (Elastic Common Schema) format:

```json
{
  "@timestamp":"2023-08-22T09:58:41.408734+00:00",
  "log.level":"Information",
  "message":"Worker running at: 08/22/2023 09:58:41 +00:00",
  "ecs.version":"8.6.0",
  "log":{
    "logger":"SampleFSharpWorker.Workers.Worker"
  },
  "labels":{
    "MessageTemplate":"Worker running at: {time}",
    "Environment":"Development"
  },
  "agent":{
    "type":"Elastic.CommonSchema.Serilog",
    "version":"8.6.1+88f2bc81a0b7440e4059e323e610bb03df61862c"
  },
  "event":{
    "created":"2023-08-22T09:58:41.408734+00:00",
    "severity":2,
    "timezone":"Coordinated Universal Time"
  },
  "host":{
    "os":{
      "full":"Linux 5.15.49-linuxkit-pr #1 SMP PREEMPT Thu May 25 07:27:39 UTC 2023",
      "platform":"Unix",
      "version":"5.15.49.0"
    },
    "architecture":"Arm64",
    "hostname":"6e2e4dbd1a82"
  },
  "process":{
    "name":"SampleFSharpWorker",
    "pid":131,
    "thread.id":5,
    "thread.name":".NET ThreadPool Worker",
    "title":""
  },
  "service":{
    "name":"SampleFSharpWorker",
    "type":"dotnet",
    "version":"1.0.0"
  },
  "user":{
    "domain":"6e2e4dbd1a82",
    "name":"local_developer"
  },
  "metadata":{
    "time":"2023-08-22T09:58:41.4085193+00:00"
  }
}
```

Rolling log files:

![rolling log files](./img/rolling-file.png)

## resources used

* https://www.elastic.co/blog/getting-started-with-the-elastic-stack-and-docker-compose - looks like filebeat goes directly to elasticsearch. It should be logstash instead.
* https://www.elastic.co/guide/en/beats/filebeat/current/running-on-docker.html - Run Filebeat on Docker
* https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-installation-configuration.html
* https://www.elastic.co/guide/en/beats/filebeat/current/configuring-howto-filebeat.html - Configure Filebeat
* https://www.elastic.co/guide/en/ecs-logging/dotnet/master/setup.html - elastic + filebeat + dotnet, specifically this: https://www.elastic.co/guide/en/ecs-logging/dotnet/master/setup.html#setup-step-3
* https://www.elastic.co/guide/en/ecs-logging/dotnet/master/serilog-formatter.html - serilog formatter