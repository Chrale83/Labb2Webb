# Labb2Webb API Specifikation

_Detta API används för att intregrera med backend delen för min YnetShop_

## ProductController

**Bas-url:** /api/products

**Ansvar:** Tar hand om hämtning av produkter för kunder, och Resterande CRUD operation för användare med adminstratörs rättigheter

---

## Endpoints

> ### Skapa produkt
>
> - **HTTP-metod:** Post
> - **Route:** /api/products
> - **Beskrivning:** Används för att skapa en produkt

### Request Body

```json
{
  "Name": "ProduktNamn",
  "Description": "Beskrivning av produkten",
  "Price": "205,50",
  "Status": "true",
  "Category": "2"
}
```

| **Statuskod**   | Beskrivning      |
| --------------- | ---------------- |
| 201 Created     | Produkten skapas |
| 400 Bad Request | Valideringsfel   |

---

> ### Hämta alla produkter
>
> - **HTTP-metod:** Get
> - **Route:** /api/products
> - **Beskrivning:** Används för att hämta alla produkter

### Response Body

```json
[
  {
    "id": 1,
    "name": "Produktnamn",
    "description": "Beskrivning av produkten",
    "price": 199.99,
    "status": true,
    "categoryId": 2
  },
  {
    "id": 2,
    "name": "En annan produkt",
    "description": "Beskrivning av den andra produkten",
    "price": 299.99,
    "status": false,
    "categoryId": 3
  }
]
```

> ### Ta bort en produkt
>
> - **HTTP-metod:** Delete
> - **Route:** /api/products{id}
> - **Beskrivning:** Används för att hämta ta bort en specifik produkt med produktens id nummer

### Request Body

```json
{
  "Id": "Produktens Id nummer"
}
```

| **Statuskod** | Beskrivning             |
| ------------- | ----------------------- |
| 200 Ok        | Produkten togsbort      |
| 204 NoContent | Produkten hittades inte |

---

> ### Sök efter produkter
>
> - **HTTP-metod:** Get
> - **Route:** /api/products/search?searchWord={söksträng}
> - **Beskrivning:** Används för att hämta söka efter produkter genom deras namn om dem innehåller dess sträng och få en lista returnerad av dessa produkter

### Query Parameter

- searchWord (string): söksträngen

### Request Body

```json
{
  "string": "en sök sträng"
}
```

| **Statuskod** | Beskrivning               |
| ------------- | ------------------------- |
| 200 Ok        | Produkten har uppdaterats |

---

> ### Uppdatera en produkt
>
> - **HTTP-metod:** Put
> - **Route:** /api/products/{id}
> - **Beskrivning:** Används för att uppdatera en befintlig produkt

### Request Body

```json
{
  "Name": "Namnet på produkten",
  "Description": "Beskrivning av produkten",
  "Price": "3000,56",
  "Status": "Finns den i lager",
  "Category": "5"
}
```

| **Statuskod** | Beskrivning                |
| ------------- | -------------------------- |
| 200 Ok        | En lista med produkter fås |
| 204 NoContent | inga produkter hittades    |

---

---

---

## Customer

**Bas-url:** /api/customers

**Ansvar:** Tar hand om hämta information om kunder, uppdatera kunders information och ta bort kunder

---

## Endpoints

> ### Hämta en kund
>
> - **HTTP-metod:** Get
> - **Route:** /api/customers/{id}
> - **Beskrivning:** Används hämta en kund, autensering krävs. Admin eller respektive kund

### Response Body

```json
{
  "Id": "4",
  "FirstName": "Arne",
  "LastName": "Arnesson",
  "Email": "arne@arne.se",
  "PhoneNumber": "031553322",
  "Streetname": "Korsvägen",
  "StreetNumber": "55",
  "City": "Göteborg"
}
```

| **Statuskod** | Beskrivning          |
| ------------- | -------------------- |
| 200 OK        | En kund returneras   |
| 404 NotFound  | Kunden hittades inte |
| 403 Forbid    | Ingen behörighet     |

---

> ### Hämta alla kunder
>
> - **HTTP-metod:** Get
> - **Route:** /api/customers/
> - **Beskrivning:** Används hämta Alla kunder. Autensering krävs

### Response Body

```json
[
  {
    "Id": "4",
    "FirstName": "Arne",
    "LastName": "Arnesson",
    "Email": "arne@arne.se",
    "PhoneNumber": "031553322",
    "Streetname": "Korsvägen",
    "StreetNumber": "55",
    "City": "Göteborg"
  },
  {
    "Id": "7",
    "FirstName": "Bengt",
    "LastName": "Bengtsson",
    "Email": "bengt@bengt.se",
    "PhoneNumber": "031553322",
    "Streetname": "Kungsgatan",
    "StreetNumber": "44",
    "City": "Malmö"
  }
]
```

| **Statuskod** | Beskrivning          |
| ------------- | -------------------- |
| 200 OK        | En lista returneras  |
| 204 NoCOntent | Inga kunder hittades |

---

> ### Uppdatera kund
>
> - **HTTP-metod:** Put
> - **Route:** /api/customers/
> - **Beskrivning:** Uppdatera en befintlig kund

### Request Body

```json
{
  "Id": "4",
  "FirstName": "Arne",
  "LastName": "Arnesson",
  "PhoneNumber": "031553322",
  "Streetname": "Korsvägen",
  "StreetNumber": "55",
  "City": "Göteborg"
}
```

| **Statuskod** | Beskrivning            |
| ------------- | ---------------------- |
| 200 OK        | Kunden har uppdaterats |
| 404 NoCOntent | Ingen kund hittades    |
| 403 Forbid    | Fel rättigheter        |

---

> ### Ta bort kund
>
> - **HTTP-metod:** Delete
> - **Route:** /api/customers/{id}
> - **Beskrivning:** Ta bort vald kund

| **Statuskod** | Beskrivning           |
| ------------- | --------------------- |
| 200 OK        | Kunden har tagitsbort |
| 404 NotFound  | Ingen kund hittades   |

---

> ### Sök efter kunder
>
> - **HTTP-metod:** Get
> - **Route:** /api/customers/search?searchWord={söksträng}
> - **Beskrivning:** Används för att hämta söka efter kunder genom deras email om dem innehåller dess sträng och få en lista returnerad av dessa kunder

### Query Parameter

- searchWord (string): söksträngen

### Request Body

```json
{
  "string": "en sök sträng"
}
```

| **Statuskod** | Beskrivning          |
| ------------- | -------------------- |
| 200 Ok        | en lista returneras  |
| 204 NoContent | Inga kunder hittades |

## Authensering

**Bas-url:** /api/auth

**Ansvar:** för att registrera och logga in som kund eller logga in som admin

---

## Endpoints

> ### Registrera en kund
>
> - **HTTP-metod:** Post
> - **Route:** /api/auth/
> - **Beskrivning:** Registrera dig som en ny kund

### Request Body

```json
{
  "Id": "4",
  "FirstName": "Arne",
  "Password": "hemligtLösen500",
  "LastName": "Arnesson",
  "Email": "arne@arne.se",
  "PhoneNumber": "031553322",
  "Streetname": "Korsvägen",
  "StreetNumber": "55",
  "City": "Göteborg"
}
```

| **Statuskod**  | Beskrivning             |
| -------------- | ----------------------- |
| 200 OK         | Registreringen lyckades |
| 400 Badrequest | gick inte att skapa     |

---

> ### Logga in som kund/adminstratör
>
> - **HTTP-metod:** Post
> - **Route:** /api/auth/
> - **Beskrivning:** Logga in som kund eller adminstratör

### Request Body

```json
{
  "Email": "arne@arne.se",
  "Password": "hemligtLösen500"
}
```

| **Statuskod**  | Beskrivning                               |
| -------------- | ----------------------------------------- |
| 200 OK         | Authensering lyckades en token returneras |
| 400 Badrequest | fel vid mail eller lösenord               |

---

## Orders

**Bas-url:** /api/orders

**Ansvar:** För att skapa och hämta order som kund

---

## Endpoints

> ### Skapa en order
>
> - **HTTP-metod:** Post
> - **Route:** /api/orders/
> - **Beskrivning:** Skapa en order. Kräver att vara authensierard

### Request Body

```json
{
  "Ordertime": "2004-03-23",
  "TotalCost": "50000,54",
  "ProductsOrder": [
    {
      "ProductId": "5",
      "Quantity": "3"
    },
    {
      "ProductId": "55",
      "Quantity": "5"
    }
  ]
}
```

| **Statuskod** | Beskrivning        |
| ------------- | ------------------ |
| 201 Created   | Ordern har sparats |

---

> ### Hämta ordar
>
> - **HTTP-metod:** get
> - **Route:** /api/orders/{id}
> - **Beskrivning:** Hämta alla odrar för vald kund

### Response Body

```json
{
  "OrderId": "5",
  "CustomerFirstName": "Arne",
  "CustomerLastName": "Arnesson",
  "PurchaseDate": "2004-03-21",
  "TotalPrice": "40000.43"
  "Products": [
    {
        "ProductName": "RTX 3090",
        "ProductProice": "12000,00",
        "CategoryName": "Grafikkort",
        "Quantity": "2"
    },
    {
        "ProductName": "dell 24",
        "ProductProice": "18000,00",
        "CategoryName": "Skärm",
        "Quantity": "1"
    }
  ]
}
```

| **Statuskod**  | Beskrivning                               |
| -------------- | ----------------------------------------- |
| 200 OK         | Authensering lyckades en token returneras |
| 400 Badrequest | fel vid mail eller lösenord               |

---

## Referens>Daqta

**Bas-url:** /api/orders

**Ansvar:** För att kunna hämta kategorierna som tilhör produkter produkterna

---

## Endpoints

> ### Skapa en order
>
> - **HTTP-metod:** Get
> - **Route:** /api/categories/
> - **Beskrivning:** Hämta kategorier till produkterna

### Response Body

```json
[
  {
    "Id": "5",
    "Name": "Grafikkort"
  },
  {
    "Id": "2",
    "Name": "Skärm"
  },
  {
    "Id": "1",
    "Name": "Ram minne"
  }
]
```
