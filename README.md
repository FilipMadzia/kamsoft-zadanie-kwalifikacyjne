# Zadanie kwalifikacyjne: Generyczny parser danych przesyłanych przez API (.NET / C#)

Program parsjący dane typu `CSV` oraz `INTERNAL_JSON` przesłanych w formacie `Base64`

### Wymagania

.NET 10 SDK

### Uruchomienie

Przejdź do katalogu projektu i uruchom aplikację za pomocą polecenia:

```bash
dotnet run
```

Wpisz w przeglądarce adres `http://localhost:5014/swagger` aby uzyskać dostęp do interfejsu Swagger UI lub użyj innego narędzia np. `Postman`

### Endpointy

API posiada jeden endpoint:

```
POST http://localhost:5014/swagger/api/v1/parse-content
```

wymagający nagłówka `Content-Type: application/json` oraz przyjmujący w ciele żądania obiekt JSON o następującej strukturze:

```json
{
  "type": "CSV" | "INTERNAL_JSON",
  "content": "..."
}
```

`type` przyjmuje tylko i wyłącznie wartości `CSV` lub `INTERNAL_JSON` oraz nie może być `null`

`content` przyjmuje tylko i wyłącznie niepusty ciąg znaków w formacie `Base64` zawierający zakodowane dane w formacie zgodnym z polem `type`

Endpoint może zwrócić następujące kody HTTP:

`200` - w przypadku poprawnego przetworzenia danych, wraz z obiektem JSON zawierającym liczbę przetworzonych obiektów oraz tablicę obiektów

`400` - w przypadku błędnych danych wejściowych (np. niepoprawny format Base64, niepoprawny format danych, brak wymaganych pól w obiekcie JSON)

### Przykładowe żądania

#### Przykład 1: Parsowanie danych typu CSV

```json
{
  "type": "CSV",
  "content": "Rmlyc3ROYW1lLExhc3ROYW1lLEFnZQpKb2huLFNtaXRoLDI4CkVtbWEsSm9obnNvbiwzNApMaWFtLEJyb3duLDE5Ck9saXZpYSxEYXZpcyw0MgpOb2FoLFdpbHNvbiwyNQ=="
}
```

Oczekiwana odpowiedź:

```json
{
    "httpCode": 200,
    "objectCount": 5,
    "objects": [
        {
            "firstName": "John",
            "lastName": "Smith",
            "age": "28"
        },
        {
            "firstName": "Emma",
            "lastName": "Johnson",
            "age": "34"
        },
        {
            "firstName": "Liam",
            "lastName": "Brown",
            "age": "19"
        },
        {
            "firstName": "Olivia",
            "lastName": "Davis",
            "age": "42"
        },
        {
            "firstName": "Noah",
            "lastName": "Wilson",
            "age": "25"
        }
    ]
}
```

#### Przykład 2: Parsowanie danych typu INTERNAL_JSON

```json
{
  "type": "INTERNAL_JSON",
  "content": "WwogIHsKICAgICJmaXJzdE5hbWUiOiAiU29waGlhIiwKICAgICJsYXN0TmFtZSI6ICJNaWxsZXIiLAogICAgImFnZSI6IDMxCiAgfSwKICB7CiAgICAiZmlyc3ROYW1lIjogIkphbWVzIiwKICAgICJsYXN0TmFtZSI6ICJUYXlsb3IiLAogICAgImFnZSI6IDQ1CiAgfSwKICB7CiAgICAiZmlyc3ROYW1lIjogIkF2YSIsCiAgICAibGFzdE5hbWUiOiAiQW5kZXJzb24iLAogICAgImFnZSI6IDIyCiAgfSwKICB7CiAgICAiZmlyc3ROYW1lIjogIkJlbmphbWluIiwKICAgICJsYXN0TmFtZSI6ICJUaG9tYXMiLAogICAgImFnZSI6IDM3CiAgfSwKICB7CiAgICAiZmlyc3ROYW1lIjogIkNoYXJsb3R0ZSIsCiAgICAibGFzdE5hbWUiOiAiSmFja3NvbiIsCiAgICAiYWdlIjogMjkKICB9Cl0="
}
```

Oczekiwana odpowiedź:

```json
{
    "httpCode": 200,
    "objectCount": 5,
    "objects": [
        {
            "firstName": "Sophia",
            "lastName": "Miller",
            "age": 31
        },
        {
            "firstName": "James",
            "lastName": "Taylor",
            "age": 45
        },
        {
            "firstName": "Ava",
            "lastName": "Anderson",
            "age": 22
        },
        {
            "firstName": "Benjamin",
            "lastName": "Thomas",
            "age": 37
        },
        {
            "firstName": "Charlotte",
            "lastName": "Jackson",
            "age": 29
        }
    ]
}
```

#### Przykład 3: Błędne dane wejściowe

```json
{
  "type": "CSV",
  "content": "treść w formacie innym niż base64"
}
```

Oczekiwana odpowiedź:

```
Invalid Base64 string.
```

wraz z kodem HTTP `400 Bad Request`