# Food Delivery System

Цей проєкт є навчальною системою доставки їжі, реалізованою на C# з використанням принципів об'єктно-орієнтованого програмування.

Система дозволяє користувачам:
- переглядати та керувати меню
- додавати страви до кошика
- створювати замовлення
- відстежувати статус замовлення

Адміністратор може:
- створювати, оновлювати та видаляти страви
- керувати меню
- переглядати користувачів

Кур’єр:
- приймає замовлення
- оновлює статус доставки

---

## Архітектура проєкту

Проєкт розділений на шари:

### Domain
Містить наступні сутності:
- User
- Client
- Admin
- Courier
- Dish
- Cart
- CartItem
- Order
- OrderItem

Містить наступні enums:
- OrderStatus (New, Accepted, InProgress, Delivered, Canceled)
- UserRole(Client, Courier)

Містить наступні інтерфейси:
- IAuthService
- ICartService
- IOrderService

###  Infrastructure
Містить сервіси:
- AuthService
- DishService
- OrderService
- CartService
- UserService

### Tests
Містить unit-тести:
- OrderTests
- DishTests
- CartTests
- ClientTests
- CourierTests
- AdminTests
- AuthServiceTests
- DishServiceTests
- OrderServiceTests

---

## Основні сутності

### User
Базовий клас користувача системи.

### Dish
Страва з параметрами:
- Id (GUID)
- Name
- Description
- Price
- ImageUrl

### Order
Замовлення, яке містить:
- список страв
- адресу доставки
- статус
- дату створення

### Cart
Кошик користувача для формування замовлення.

---

## Життєвий цикл замовлення

```

New → Accepted → InProgress → Delivered → Canceled

```

---

## Ролі в системі

### Client
- додає товари в кошик
- створює чи скасовує замовлення
- відстежує статус

### Admin
- додає/редагує/видаляє страви
- переглядає статуси замовлень
- переглядає користувачів системи

### Courier
- приймає замовлення
- змінює статус доставки

---


## Технології

- C#
- .NET
- xUnit (unit testing)
- OOP principles
- Clean Architecture (basic layering)

---

## Запуск проєкту

1. Клонувати репозиторій
2. Відкрити solution у Visual Studio
3. Зібрати проєкт
4. Запустити тести через Test Explorer

```
