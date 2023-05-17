# PoliVagas REST API

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