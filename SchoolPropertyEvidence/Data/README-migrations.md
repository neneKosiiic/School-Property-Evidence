# Databázové migrace

Projekt používá **Entity Framework Core migrations** pro správu změn v databázi.

Backend projekt je:

```bash
SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

Databázový context je:

```bash
SchoolPropertyEvidence/Data/ApplicationDbContext.cs
```

Migrace se ukládají do složky:

```bash
SchoolPropertyEvidence/Data/Migrations
```

---

## K čemu migrace slouží

Migrace slouží k tomu, aby se změny v databázových modelech daly verzovat přes Git.

Například když přidáme nový sloupec do modelu, nevytváříme ho ručně v databázi. Místo toho vytvoříme migraci, commitneme ji a ostatní členové týmu si ji aplikují pomocí příkazu:

```bash
dotnet ef database update
```

## Co udělat po `git pull`

Po stažení změn z Gitu vždy spusť:

```bash
dotnet ef database update --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

Tento příkaz aplikuje všechny nové migrace do databáze.

---

## Jak vytvořit novou migraci

Migraci vytváří ten, kdo změnil databázové modely.

Typicky se jedná o změny ve složce:

```bash
SchoolPropertyEvidence/Models
```

Například:

- přidání nové tabulky,
- přidání nového sloupce,
- přejmenování sloupce,
- změna datového typu,
- přidání vztahu mezi tabulkami.

Nová migrace se vytvoří příkazem:

```bash
dotnet ef migrations add NazevMigrace --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --output-dir Data/Migrations
```

Příklad:

```bash
dotnet ef migrations add AddSerialNumberToItems --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --output-dir Data/Migrations
```

---

## Jak aplikovat migraci do databáze

Po vytvoření migrace spusť:

```bash
dotnet ef database update --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

Tím se změny fyzicky provedou v databázi.

---

## Co commitovat

Do Gitu commitujeme složku s migracemi:

```bash
SchoolPropertyEvidence/Data/Migrations
```

a také soubory, ve kterých se měnily modely, například:

```bash
SchoolPropertyEvidence/Models/ItemModel.cs
```

Příklad:

```bash
git add SchoolPropertyEvidence/Data/Migrations
git add SchoolPropertyEvidence/Models
git commit -m "Add item serial number migration"
git push
```

---

## Doporučený workflow

### Když jen stahuju změny

```bash
git pull
dotnet ef database update --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

---

### Když měním databázový model

1. Uprav model v kódu.
2. Vytvoř migraci.
3. Aplikuj migraci do databáze.
4. Commitni modely i migraci.

Příklad:

```bash
dotnet ef migrations add NazevZmeny --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --output-dir Data/Migrations

dotnet ef database update --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj

git add SchoolPropertyEvidence/Data/Migrations
git add SchoolPropertyEvidence/Models
git commit -m "Add database migration NazevZmeny"
git push
```

---

## Jak zobrazit seznam migrací

```bash
dotnet ef migrations list --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

Pokud je migrace označená jako `Pending`, znamená to, že ještě není aplikovaná v databázi.

Aplikuje se pomocí:

```bash
dotnet ef database update --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

---

## Jak odstranit poslední migraci

Pokud byla migrace vytvořená omylem a ještě nebyla aplikovaná do databáze, dá se odstranit:

```bash
dotnet ef migrations remove --project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj --startup-project SchoolPropertyEvidence/SchoolPropertyEvidence.csproj
```

Pokud už migrace byla aplikovaná do databáze, nejdřív je potřeba vrátit databázi na předchozí migraci. To se musí dělat opatrně a ideálně po domluvě s týmem.

---

## Důležité pravidlo

Nikdo by neměl upravovat strukturu databáze ručně přes phpMyAdmin, Adminer nebo MySQL konzoli, pokud se jedná o změnu, která má být součástí projektu.

Struktura databáze se mění přes:

```bash
dotnet ef migrations add ...
dotnet ef database update
```

Ruční změny v databázi by se totiž nepropsaly ostatním členům týmu přes Git.

---

## Poznámka k InitialBaseline

Projekt používá počáteční baseline migraci.

To znamená, že první migrace pouze říká Entity Frameworku, že existující databáze je výchozí stav projektu.

Tato migrace nemusí vytvářet tabulky od nuly. Slouží hlavně k tomu, aby se od tohoto bodu daly všechny další změny databáze spravovat přes migrace.

Od této chvíle musí každá další změna databázových modelů vzniknout jako nová migrace.
