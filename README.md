# PoliVagas REST API

## Installation

```bash
docker pull ghcr.io/poli-vagas/api:main
```

```bash
docker create --name poli-vagas-api -p 8080:80 --env-file .env ghcr.io/poli-vagas/api:main
```

```bash
docker start poli-vagas-api
```

## Running migrations

```bash
export DB_CONNECTION_STRING='User Id=postgres;Password=my_password;Server=db.isqqvvhrdyhlqgwvmdoj.supabase.co;Port=5432;Database=postgres'
```

```bash
dotnet ef database update
```

## Contributing

### Adding migrations

```bash
dotnet ef migrations add MigrationName -o src/Infrastructure/Persistence/Sql/Migrations
```