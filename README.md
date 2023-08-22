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

## resources used

* https://www.elastic.co/blog/getting-started-with-the-elastic-stack-and-docker-compose - looks like filebeat goes directly to elasticsearch. It should be logstash instead.
* https://www.elastic.co/guide/en/beats/filebeat/current/running-on-docker.html - Run Filebeat on Docker
* https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-installation-configuration.html