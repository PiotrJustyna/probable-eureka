docker compose -f docker-compose.yml stop worker
docker compose -f docker-compose.yml stop filebeat
docker compose -f docker-compose.yml stop kibana
docker compose -f docker-compose.yml stop elasticsearch

rm -rf ./logs