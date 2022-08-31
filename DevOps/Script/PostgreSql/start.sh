docker run -d \
    --name postgresql \
    -p 5432:5432 \
    -e POSTGRES_PASSWORD=postgres \
    -e PGDATA=/var/lib/postgresql/data/pgdata \
    -v /var/docker_volumes/databases/postgresql/data:/var/lib/postgresql/data \
    postgres
