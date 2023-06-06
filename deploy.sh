# Pull new version
docker pull ghcr.io/poli-vagas/api:main

# Remove backup if exists
if [ "$(docker ps -a -q -f name=poli-vagas-api-old)" ]; then
    docker rm poli-vagas-api-old
fi


# Create backup of current version
docker rename poli-vagas-api poli-vagas-api-old

# Create new container
docker create --name poli-vagas-api -p 8080:80 --env-file .env ghcr.io/poli-vagas/api:main

# Stop current container
docker stop poli-vagas-api-old

# Start new container
docker start poli-vagas-api