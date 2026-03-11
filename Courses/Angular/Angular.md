> ## HTTP InterCeptor

### USER
i want to make the notes on HttpInterCeptor. can u provide me notes  cover below points 1.What is interceptor 2.when we will use & What is the fnctionality we use 3. how many way we can create interceptor (class -> uses with class & function --> user with function) 4.Inside Http Inetrceptor we have req & next , inside this req & next what are functionalities we have and how can we use? in Interceptor we will get both request & respnse if we want to subscribe request how can we do? while getting the request we will use few pipe operators explain those pipe operators and when we will use , what is function we have for each pipe operator? explain each & every thing and provide me if any concept if i missed please add that also & provide me nodes

### ASSISTANT
Certainly! Below are detailed, structured notes on **HttpInterceptor** in Angular that cover all your points clearly and concisely—perfect for documentation or personal learning.

---

# 📘 Angular HttpInterceptor – Comprehensive Notes

## 1. **What is an HttpInterceptor?**

- In Angular, `HttpInterceptor` is part of the `@angular/common/http` module.
- It allows **intercepting** HTTP requests and responses **globally** before they are sent to the server or returned to the caller.
- Think of it as middleware for HTTP communication.
- Interceptors are useful for **cross-cutting concerns** like:
  - Adding authentication tokens
  - Logging requests/responses
  - Caching
  - Error handling
  - Modifying headers, URLs, or payloads

> 💡 **Key Point**: Interceptors are **applied globally** to all `HttpClient` requests unless explicitly bypassed.

---

## 2. **When to Use & Common Functionalities**

### ✅ **When to Use:**
- You need consistent behavior across **all or most** HTTP calls.
- You want to **avoid repeating logic** in every service (e.g., manually adding auth tokens).

### 🔧 **Common Functionalities Implemented:**
| Use Case                | Description |
|------------------------|-------------|
| **Authentication**     | Add `Authorization` header with JWT token |
| **Logging**            | Log request URL, method, time, response status |
| **Error Handling**     | Catch 401/403 errors globally (e.g., redirect to login) |
| **Request/Response Transformation** | Modify data format before sending or after receiving |
| **Loading Indicators** | Show/hide spinner based on ongoing requests |
| **Caching**            | Return cached response for certain GET requests |

---

## 3. **Ways to Create an Interceptor**

There are **two conceptual patterns**, but **technically only one standard way** in Angular:

### ✅ **Standard Way: Class-based Interceptor (Only Supported Method)**

Angular **requires** interceptors to be classes that implement the `HttpInterceptor` interface.

```ts
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Modify request
    const clonedReq = req.clone({
      setHeaders: { Authorization: 'Bearer xyz123' }
    });

    // Pass to next handler
    return next.handle(clonedReq);
  }
}
```

> ⚠️ **Important**: Angular **does not support function-based interceptors** like some other frameworks (e.g., Axios in React). The interceptor **must be a class** decorated with `@Injectable()` and registered in the `HTTP_INTERCEPTORS` provider.

### ❌ Function-Based Interceptor?
- **Not supported** in Angular.
- All interceptors must be **classes implementing `HttpInterceptor`**.
- You **cannot** pass a plain function to `HTTP_INTERCEPTORS`.

> ✅ **Conclusion**: Only **one way**: **Class-based** with `intercept()` method.

---

## 4. **Inside the Interceptor: `req` and `next`**

### 🔹 `req: HttpRequest<any>`
- Immutable object representing the outgoing request.
- **You cannot modify it directly** → use `.clone()` to create a modified copy.

#### Common Methods/Properties:
| Property/Method      | Purpose |
|----------------------|--------|
| `req.url`            | Request URL |
| `req.method`         | HTTP method (GET, POST, etc.) |
| `req.headers`        | Read-only headers |
| `req.body`           | Request payload |
| `req.clone({ ... })` | Create a modified copy (e.g., add headers) |

✅ **Example**:
```ts
const newReq = req.clone({
  setHeaders: { 'X-Custom': 'value' },
  url: req.url.replace('http://', 'https://')
});
```

---

### 🔹 `next: HttpHandler`
- Represents the **next step** in the interceptor chain.
- Call `next.handle(modifiedRequest)` to continue.

> 🔄 **Interceptor Chain**: Multiple interceptors form a chain. The last one calls the actual backend.

---

### 🔄 **How to Handle Response?**
The `next.handle()` returns an `Observable<HttpEvent<any>>`, which includes:
- `HttpSentEvent`
- `HttpHeaderResponse`
- `HttpResponse`
- `HttpProgressEvent`
- `HttpUserEvent`

To **access the response**, use RxJS operators like `map`, `tap`, `catchError`.

> 🔁 **You cannot "subscribe" directly inside the interceptor** in a way that breaks the chain. Instead, **transform or react** using RxJS pipes.

---

## 5. **Using RxJS Pipe Operators in Interceptors**

Since `next.handle()` returns an `Observable`, you **pipe** operators to process events.

### 🔸 Common Operators & Their Use:

| Operator      | Purpose | When to Use | Example |
|--------------|--------|------------|--------|
| **`tap`** | **Side effects** (logging, analytics) | When you want to **observe** without modifying the stream | Log response time |
| **`map`** | **Transform** response data | Modify response structure before returning to caller | Rename fields |
| **`catchError`** | **Handle errors globally** | Intercept 401, 500 errors and react (e.g., redirect, refresh token) | Refresh JWT on 401 |
| **`finalize`** | Cleanup (e.g., hide spinner) | After request completes (success or error) | Stop loading indicator |
| **`switchMap` / `mergeMap`** | **Chain with another observable** | When you need to make another HTTP call (e.g., refresh token) | Fetch new token and retry |

---

### ✅ **Example: Full Interceptor with Operators**

```ts
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, finalize } from 'rxjs/operators';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const startTime = Date.now();

    return next.handle(req).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          const elapsed = Date.now() - startTime;
          console.log(`Request to ${req.url} took ${elapsed} ms`);
        }
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('HTTP Error:', error);
        if (error.status === 401) {
          // Handle unauthorized
          // e.g., this.router.navigate(['/login']);
        }
        return throwError(() => error);
      }),
      finalize(() => {
        // e.g., this.loadingService.hide();
      })
    );
  }
}
```

---

## 6. **Registering the Interceptor**

In your `AppModule` (or feature module if scoped):

```ts
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true // ⚠️ Must be true for multiple interceptors
    }
  ]
})
export class AppModule {}
```

> 🔁 **Order Matters**: Interceptors run in the order they are provided.

---

## 7. **Key Concepts You Might Have Missed**

### ✅ **1. Immutability of HttpRequest**
- Always use `.clone()` to modify requests.

### ✅ **2. Multiple Interceptors**
- Angular supports chaining multiple interceptors via `multi: true`.

### ✅ **3. Skipping Interceptors (Advanced)**
- There’s **no built-in way** to skip an interceptor for specific requests.
- Workaround: Add a custom header (e.g., `skip-interceptor: true`) and check for it inside the interceptor.

```ts
if (req.headers.has('skip-interceptor')) {
  return next.handle(req);
}
```

### ✅ **4. Interceptors Only Work with `HttpClient`**
- They **do not intercept** `fetch()`, `XMLHttpRequest`, or third-party HTTP libraries.

### ✅ **5. Testing Interceptors**
- Use `HttpClientTestingModule` in unit tests.

---

## 📌 Summary Checklist

| Concept | Covered? |
|--------|--------|
| What is HttpInterceptor? | ✅ |
| When & Why to use | ✅ |
| Creation method (class only) | ✅ |
| `req` and `next` usage | ✅ |
| Accessing/modifying request & response | ✅ |
| RxJS operators (`tap`, `map`, `catchError`, etc.) | ✅ |
| Subscription inside interceptor? → Use operators, not direct subscribe | ✅ |
| Registration & ordering | ✅ |
| Immutability, skipping, limitations | ✅ |

---

> ## Functional Interceptor (V16+)

> ## DI enable for services


### ASSISTANT
Absolutely! You're absolutely right: **starting Angular v15+ (especially v16+ with standalone APIs)**, **functional interceptors** are the **modern, recommended way** — replacing class-based interceptors.

Below is a **clear, detailed note-style guide** covering:

✅ Functional `HttpInterceptor`  
✅ Setting headers  
✅ Error handling & redirecting  
✅ Using RxJS `pipe()` operators  
✅ Creating & injecting services (with & without `providedIn: 'root'`)  
✅ Registering everything in `app.config.ts` (standalone apps)

---

## 📌 1. ✨ Functional Interceptor vs Class Interceptor

| Feature | Class Interceptor | **Functional Interceptor (Modern)** |
|--------|-------------------|------------------------------------|
| Syntax | `class MyInterceptor implements HttpInterceptor` | `const myInterceptor: HttpInterceptorFn = ...` |
| DI | Constructor injection | `inject()` inside function |
| Registration | `HTTP_INTERCEPTORS` (NgModule) or `withInterceptors([...])` | **Only** via `withInterceptors([...])` |
| Tree-shakable | ❌ (if not careful) | ✅ (by default) |
| Recommended | ❌ (legacy) | ✅ **Yes!** |

> ✅ **Use functional interceptors** in Angular 16+ standalone apps.

---

## 🛠️ 2. Functional Interceptor Code (with Headers & Error Handling)

```ts
// auth.interceptor.ts
import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  // 🔐 Get token (from service or localStorage)
  const token = localStorage.getItem('authToken'); // or use a service (see below)

  // 📦 Clone request with headers
  const authReq = req.clone({
    setHeaders: {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` })
    }
  });

  // 🔄 Handle request & response
  return next(authReq).pipe(
    tap(event => {
      // Optional: log success
      console.log('HTTP OK:', event);
    }),
    catchError(error => {
      console.error('HTTP Error:', error);

      if (error.status === 401) {
        localStorage.removeItem('authToken');
        router.navigate(['/login']);
      } else if (error.status === 403) {
        router.navigate(['/forbidden']);
      } else if (error.status >= 500) {
        console.error('Server error!');
      }

      return throwError(() => error);
    })
  );
};
```

> ✅ No class → just a **function** that uses `inject()` for DI.

---

## 🧩 3. Creating a Reusable Service (for Token Management)

### Option A: **`providedIn: 'root'`** (Recommended)
Auto-provided, tree-shakable, no manual registration.

```ts
// auth.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root' // 🌟 Auto-registered in root injector
})
export class AuthService {
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  setToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  clearToken(): void {
    localStorage.removeItem('authToken');
  }
}
```

✅ **Pros**: No need to add to `providers`. Automatically available everywhere.

---

### Option B: **Manual Registration (without `providedIn`)**
You **must** add it to `providers` in `app.config.ts`.

```ts
// auth.service.ts
import { Injectable } from '@angular/core';

@Injectable() // ❌ No providedIn
export class AuthService {
  // ... same methods
}
```

Then in `app.config.ts`:

```ts
// app.config.ts
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './core/auth.interceptor';
import { AuthService } from './core/auth.service'; // ← must import

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    
    // ✅ Manually provide service (only if NOT using providedIn: 'root')
    AuthService
  ]
};
```

> 🔴 **Avoid this unless you need scoped providers (e.g., `@Component`-level)**.

---

## 🔁 4. Using `AuthService` Inside Functional Interceptor

Update the interceptor to use the service:

```ts
// auth.interceptor.ts
import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AuthService } from './auth.service'; // 👈 import service

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const authService = inject(AuthService); // ✅ Inject service

  const token = authService.getToken(); // ✅ Use service method

  const authReq = req.clone({
    setHeaders: {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` })
    }
  });

  return next(authReq).pipe(
    catchError(error => {
      if (error.status === 401) {
        authService.clearToken();
        router.navigate(['/login']);
      }
      return throwError(() => error);
    })
  );
};
```

> ✅ Clean, testable, and follows Angular best practices.

---

## 📦 5. Final `app.config.ts` (Standalone App)

```ts
// app.config.ts
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app.routes';
import { authInterceptor } from './core/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    // AuthService is auto-provided (if providedIn: 'root') → no need to list here
  ]
};
```

> ✅ That’s it! No `NgModule`, no `multi: true`, no boilerplate.

---

## 📝 Summary Notes

| Concept | How to Do It |
|-------|-------------|
| **Functional Interceptor** | Export a function of type `HttpInterceptorFn` |
| **Inject dependencies** | Use `inject(Service)` inside the function |
| **Add headers** | `req.clone({ setHeaders: { ... } })` |
| **Error handling** | Use `catchError` inside `pipe()` |
| **Redirect on 401** | Inject `Router` and call `router.navigate(...)` |
| **Create service** | Use `@Injectable({ providedIn: 'root' })` → auto-DI |
| **Manual service registration** | Only if no `providedIn` → add to `providers` in `appConfig` |
| **Register interceptor** | `provideHttpClient(withInterceptors([myInterceptor]))` |

---

✅ **Best Practice**:  
- Always use `providedIn: 'root'` for app-wide services  
- Prefer functional interceptors in Angular 16+  
- Keep interceptors **lightweight** — avoid heavy logic

----------------
----------------
-----------------

> ## Providers 
### USER
Explain about providers in angular? explain cleary in injections & singleton & scoped injection provide me notes for these components. And if we want to inject the service outside the constructor that also provide me for making notes and provide me proper header & proper explanation and proper coding implementation for making note in simple understandable way

### ASSISTANT
# Angular Providers: Injection, Singleton, Scoped Services & Manual Injection

## 1. What is a Provider in Angular?

A **provider** is a recipe that tells Angular how to create or obtain a dependency (usually a service). Providers are used by Angular's **Dependency Injection (DI)** system to inject services into components, directives, pipes, or other services.

---

## 2. Dependency Injection (DI) in Angular

Angular’s **Dependency Injection** is a design pattern where a class receives its dependencies from an external source rather than creating them itself.

### How it works:
- You **declare a provider** (where & how to create a service).
- You **inject** the service where needed (usually in a constructor).
- Angular’s injector **creates/resolves** the instance and gives it to your class.

---

## 3. Singleton Services (Application-wide)

A **singleton service** has **only one instance** across the whole app.

### How to create:
#### ✅ Preferred Way (Angular v6+):
Use `providedIn: 'root'` in the `@Injectable()` decorator.

```ts
// auth.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root' // ← Makes it a singleton automatically
})
export class AuthService {
  currentUser = 'John Doe';
}
```

#### Usage in a component:
```ts
// home.component.ts
import { Component } from '@angular/core';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-home',
  template: `<p>User: {{ authService.currentUser }}</p>`
})
export class HomeComponent {
  constructor(public authService: AuthService) { }
}
```

> ✅ **Result**: Same `AuthService` instance is shared across all components.

---

## 4. Scoped (Non-Singleton) Services

Sometimes you want a **new instance** of a service **per component** (not shared).

### How to do it:
**Do NOT** use `providedIn: 'root'`. Instead, **provide the service in the component’s `providers` array**.

```ts
// counter.service.ts
import { Injectable } from '@angular/core';

@Injectable() // ← No providedIn!
export class CounterService {
  count = 0;
  increment() { this.count++; }
}
```

```ts
// parent.component.ts
import { Component } from '@angular/core';
import { CounterService } from './counter.service';

@Component({
  selector: 'app-parent',
  template: `
    <p>Parent Count: {{ counter.count }}</p>
    <button (click)="counter.increment()">+ Parent</button>
    <app-child></app-child>
  `,
  providers: [CounterService] // ← New instance for this component & its children
})
export class ParentComponent {
  constructor(public counter: CounterService) { }
}
```

```ts
// child.component.ts
import { Component } from '@angular/core';
import { CounterService } from './counter.service';

@Component({
  selector: 'app-child',
  template: `
    <p>Child Count: {{ counter.count }}</p>
    <button (click)="counter.increment()">+ Child</button>
  `
})
export class ChildComponent {
  constructor(public counter: CounterService) { }
}
```

> ✅ **Result**: `ParentComponent` and `ChildComponent` **share the same instance** (because child is in parent’s view).  
> ❌ But a **second** `ParentComponent` elsewhere would get its **own new instance**.

> 🔍 **Note**: This creates a **hierarchical injector** — child components inherit the provider from the parent component if not overridden.

---

## 5. Injecting a Service Outside the Constructor

Sometimes you need to inject a service **dynamically** (e.g., inside a method, or in a non-injectable class).

Use the `Injector` class.

### Step-by-step:

1. Inject `Injector` in the constructor.
2. Use `injector.get(Service)` to get the instance.

```ts
// logger.service.ts
@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  log(msg: string) {
    console.log('[LOG]', msg);
  }
}
```

```ts
// dynamic.component.ts
import { Component, Injector } from '@angular/core';
import { LoggerService } from './logger.service';

@Component({
  selector: 'app-dynamic',
  template: `<button (click)="doSomething()">Log Now</button>`
})
export class DynamicComponent {
  constructor(private injector: Injector) { }

  doSomething() {
    // Inject service manually
    const logger = this.injector.get(LoggerService);
    logger.log('Injected outside constructor!');
  }
}
```

> ✅ **Use case**: When you **can’t use constructor injection** (e.g., in a utility class, or legacy code).

> ⚠️ **Caution**: Avoid overusing manual injection — it makes code harder to test and less clear.

---

## 6. Summary Table

| Type                | How to Define                        | Scope                      | Use Case |
|---------------------|--------------------------------------|----------------------------|--------|
| **Singleton**       | `@Injectable({ providedIn: 'root' })` | Entire application         | Shared state, API services |
| **Scoped**          | `providers: [MyService]` in `@Component` | Component + its children   | Isolated logic (e.g., form state) |
| **Manual Injection**| `injector.get(MyService)`            | On-demand                  | Dynamic or conditional usage |

---

## 7. Best Practices

- ✅ Prefer **singleton services** for most cases (API, auth, logging).
- ✅ Use **scoped services** only when you need **isolated state**.
- ✅ Avoid manual injection unless **absolutely necessary**.
- ✅ Always use **constructor injection** for clarity and testability.

---

> 💡 **Remember**: Angular’s DI is **hierarchical** — child injectors can **shadow** parent providers, enabling scoped services.

-------

### ✅ New Option: Standalone `inject()` (Angular v14+ and improved in v16–v17)

In **modern Angular (v16+)**, you **can** use the **`inject()` function** outside the constructor — **but only under certain conditions**.

#### ✅ Works: Inside a component class body (for initialization)
```ts
@Component({ ... })
export class MyComponent {
  private loginService = inject(LoginService); // ✅ Works in Angular v14+

  ngOnInit() {
    this.loginService.login();
  }
}
```

---

### Final Verdict (Angular v17)

| Approach                      | Works in v17? | When? |
|-----------------------------|---------------|------|
| `@Inject(LoginService)` outside constructor | ❌ Never | Invalid syntax |
| `inject(LoginService)` in class body | ✅ Yes | During creation (not in event handlers) |
| `inject(LoginService)` in method like `onClick()` | ❌ No | No DI context |
| `injector.get(LoginService)` in any method | ✅ Yes | Always safe |

---

### ✅ Recommendation for Angular v17

- **Prefer**: `private loginService = inject(LoginService);` in class body (cleaner than constructor).
- **Fallback**: Use `Injector` + `injector.get()` for dynamic/runtime injection in methods.


## Remember Points
- Providers we are using for injecting services/classes/ values
- in Old Angular version, for interceptors we used below format to inject the HttpInterceptor
``` ts
providers: [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: <YourInterceptorClass>,
    multi: true
  }
]

```
- So here **HTTP_INTERCEPTORS** is one type of token and we are registering this token. while running the code it check this token, based on this token availability it will inject that **InterCeptorClass** into the **Constructor**

- Here we have given **multi** is **true** that means we can register multiple **Http Interceptors** here  with array format
 **useClass: [<you can mention HttpInterceptors>]**

- In Some cases we will use **useValue** that means for correspnding to that registered token we will inject that value

``` ts
const APP_CONFIG = new InjectionToken<AppConfig>('App Config');

providers: [
  { 
    provide: APP_CONFIG, 
    useValue: { 
      apiUrl: 'https://api.example.com',
      timeout: 5000 
    } 
  }
]

```

- If we want use the value then we need to inject that Provide in that component or Service

```ts
constructor(@Inject(APP_CONFIG) private config: AppConfig) {
  // TypeScript knows `config` has `apiUrl` and `timeout`
  console.log(config.apiUrl); // ✅ Type-safe!
}
```

---

## Signals

**There are 3 types of signals**
 - 1. Writable Signals
 - 2. Computed Signals
 - 3. Effects

## 1.Writable Signals
Writable Signals are *Mutable* signals, Using Writable signals we can peform  3 Operations
 - [X] Read   ==> we can read the value from signal
 - [X] Set    ==> we can set the new value
 - [X] update ==> we can update the value based on Previous value
Writable signals provide an API for updating their values directly. You create writable signals by calling the signal function with the signal's initial value:

``` TypeScript
//Writable Signal

title:WritableSignal<string>=signal("Signal Concept");
 OR
title=signal("Signal Concept");  //we can use like this also but this not the formal way

//Read the signal ==> while reading the signal we need to call it as function
this.title()

// this.title == this will return signal function function (() => signalGetFn(node))



```
To change the value of a writable signal, either .set() it directly:

``` TypeScript
const count = signal(0);
count.set(3);

```
or use the .update() operation to compute a new value from the previous one:

``` TypeScript
const count = signal(0);
// Increment the count by 1.
count.update(value => value + 1);

```

## 2.Computed Signals ==> Computed means calculate
Computed signal are read-only signals that derive their value from other signals. You define computed signals using the computed function and specifying a derivation:

``` TypeScript

const count: WritableSignal<number> = signal(0);
const doubleCount: Signal<number> = computed(() => count() * 2);  

// here whenever this count() updates then it will update doublecount as well

//In Computed signal we are modifying the writable Signal

```

## <mark> *The **doubleCount** signal depends on the **count** signal. Whenever **count** updates, Angular knows that **doubleCount** needs to update as well.* </mark>

<span style="background-color:red;">Note</span> : caching we have in Computed signals. if **count signal** Value changed then **compute signal** will execute and will update that value to **double count** and that computed value will cached. this cached value valid till count signal value changes. so whenever you are using the doubleCount value directly it will return cached value


## ChangeDetectRef 

## Basic Concepts

### ✅ **1. What are "data-bound properties"? (With Code)**

## 1. Local Variables (True "Variables")

- Declared inside methods or blocks using `let`, `const`, or `var`.
- **Not accessible** from the template.
- Only live inside the TypeScript code.

```ts
export class MyComponent {
  ngOnInit() {
    let tempMessage = 'Only visible here';  // local variable
    const API_URL = 'https://api.example.com'; // constant
    // template cannot read these
  }
}
```
## 2. Class Properties (What You Usually Use)
- **Declared directly in the class body (no let/const needed).**
- **Automatically public and accessible in the template.
Watched by Angular change detection.
``` ts
@Component({
  template: `<h1>{{title}}</h1><p>Count: {{count}}</p>`
})
export class MyComponent {
  title = 'My Awesome App';      // property
  count = 0;                     // property
  isAdmin = true;                // property
  user = { name: 'John', role: 'dev' };

  private apiKey = 'secret123';  // private property (still a property)
}

```

**Data binding** in Angular means connecting **component class properties** to the **template (HTML)** so that when the property changes, the view (DOM) updates automatically.

#### 🔸 Example:
```ts
// app.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <h1>Hello, {{ name }}!</h1>        <!-- 👈 This is DATA BINDING -->
    <p>Count: {{ count }}</p>          <!-- 👈 Another data-bound property -->
    <button (click)="increment()">+</button>
  `
})
export class AppComponent {
  name = 'Alice';      // ←── data-bound property
  count = 0;           // ←── data-bound property

  increment() {
    this.count++;      // When this changes, view updates (if change detection runs)
  }
}
```

- ✅ **`name`** and **`count`** are **data-bound properties** because they’re used in the template with `{{ }}` (interpolation).
- 🔁 When `count` changes (e.g., via button click), Angular’s **change detection** checks if `count` has a new value → if yes, it updates the **DOM** (the `<p>` element).
- 🧱 **DOM (Document Object Model)** = the actual HTML structure in the browser that the user sees. When Angular says “update the view”, it means: **change the text/content/attributes in the real HTML page**.

> So:  
> **Data-bound property** → `count` (in TypeScript class)  
> **View (DOM)** → `<p>Count: 5</p>` in the browser

---

### ✅ **2. Tree of Change Detectors – Correct Mental Model**

Your “head and hands” analogy is **close**, but let’s refine it.

#### 🔸 Real Structure: **Component Tree = Change Detector Tree**
Each component has its **own change detector**. They form a **tree** that matches your component hierarchy.

#### Example:
```html
<app-root>                 <!-- Root component (head) -->
  <app-header></app-header>   <!-- Left hand -->
  <app-main>                  <!-- Right hand -->
    <app-sidebar></app-sidebar>  <!-- Finger on right hand -->
    <app-content></app-content>  <!-- Another finger -->
  </app-main>
</app-root>
```

Now, how change detection flows:

| Scenario | What Happens |
|--------|--------------|
| **Default strategy everywhere** | When **any async event** happens (e.g., button click), Angular starts at **`app-root`** and checks **every component** down the tree (root → header → main → sidebar → content). Zone.js will detect the changes and activate the change detection from root component |
| **You change `app-sidebar` internally** | Still, **entire tree is checked** (because of `Default` strategy). |
| **If `app-main` uses `OnPush`** | Angular will **skip** checking `app-main`, `sidebar`, and `content` **unless**: their `@Input()` changes, or an event originates inside them. |

> ❌ Your assumption: *"If changes in left hand, only left hand is checked"* → **Not true in `Default` strategy!**  
> ✅ Reality: In `Default`, **every change detection cycle checks the whole tree from root down**, regardless of where the change happened.

> ✅ Only with `OnPush` can you **limit** checking to a subtree—but even then, it’s not automatic on internal changes.

---

### ✅  **“Async Event”?**

In Angular, **change detection is triggered by “Zone.js”**—a library that **patches** async APIs.

#### 🔸 Async events that trigger change detection:
| Event Type | Example | Is it async? |
|----------|--------|-------------|
| User interaction | `(click)`, `(input)` | ✅ Yes |
| Timer | `setTimeout`, `setInterval` | ✅ Yes |
| HTTP request | `HttpClient.get()` | ✅ Yes |
| Promise | `Promise.resolve()`, `async/await` | ✅ Yes |
| Microtask | `queueMicrotask()` | ✅ Yes |

#### 🔸 Example:
```ts
ngOnInit() {
  // This is an async event → triggers change detection when callback runs
  setTimeout(() => {
    this.name = 'Bob'; // Angular will detect this change and update DOM
  }, 1000);
}
```

> 🔄 So yes—`setTimeout` and `setInterval` **are async events**, and they **do trigger change detection** (thanks to Zone.js).

> ⚠️ If you run code **outside Angular’s zone** (e.g., using `NgZone.runOutsideAngular`), then change detection **won’t run automatically**—you’d need `ChangeDetectorRef.detectChanges()`.

---

### ✅ **What is Change Detection?**
In Angular, **change detection** is the mechanism that checks for changes in data-bound properties and updates the view (DOM) accordingly.

- Angular builds a **tree of change detectors** (one per component) and checks them in a specific order when an event occurs (e.g., user input, HTTP response, timer).

---
---

### ✅ **Two Change Detection Strategies**

#### 1. `ChangeDetectionStrategy.Default` (the default)
- **How it works**: Whenever **any** async event occurs (e.g., click, Promise resolve, etc.), Angular runs change detection **from the root component down** through the entire component tree.
- it means Angular **checks** every component’s bindings to see if the values have changed (using **reference checks** for objects).
- If a bound value has **actually changed**, then the DOM is updated.

> 📌 **Important**: It’s **not** that the view "refreshes"—it’s that Angular **checks** whether it needs to update the DOM.

#### 2. `ChangeDetectionStrategy.OnPush`

- **How it works**: Angular will **skip** change detection for this component **unless one of these happens**:
  1. **Input reference changes** (i.e., you pass a **new object/array**, not just mutate the existing one).
  2. The component or its subtree **fires an event** (e.g., `(click)` inside the component).
  3. An **async pipe** (e.g., `observable | async`) emits a new value.
  4. You **manually trigger** change detection using `ChangeDetectorRef`.

> ✅ So yes—**only that component (and its children)** may be checked, **but only under specific conditions**.

---

- changeDetection means render the changes in view Component, that means view will refresh automatically
- This ChangeDetection concept mainly referring to view component only.

### we have two types of change detection strategies
- 1. OnPush
- 2. Default

## 1. Default
 - bydefault we have changedetectref as default, Due to this **default**  if any changes happened in  any  component then automatially all the view components will refresh

## 2. Push
 - In this case if any changes happened inside that particular concept via signals or any event changes, then only that particular component view will refresh


---

## InjectionContext

**What is InjectionContext?**
#### Level 1: Simple & Clear (Most interviewers love this)
> "In Angular, the `inject()` function lets us do dependency injection outside of constructors — like in field initializers, factory functions, or even standalone functions.
> 
> But `inject()` only works if Angular knows 'where' we are in the component tree — that 'where' is called the **InjectionContext**.
> 
> Angular automatically creates and tracks this context during component creation. If you try to call `inject()` outside of it (e.g., at the top level of a file), you get an error: 'No provider for InjectionContext'."

#### Level 2: Real-World Use Cases (Shows Depth)
> "InjectionContext becomes essential in these modern Angular patterns:
> 1. **Field initializers** in classes:
>    ```ts
>    class MyService {
>      private api = inject(ApiService);  // Works because Angular creates context
>    }
>    ```
> 
> 2. **Factory functions** (especially with `environmentInjector` or `runInInjectionContext`):
>    ```ts
>    const createService = () => {
>      // inject() would fail here without context
>      return inject(MyToken);
>    };
> 
>    // Correct way:
>    injector.runInInjectionContext(() => createService());
>    ```

### Let’s Understand **Injection Context** Like You’re 10 Years Old! (Super Simple & Clear)

Imagine Angular is a **magic toy factory**.

Every time Angular creates a component (like `CounterComponent`), it opens a **special magic box** for that component only.

Inside this magic box, Angular puts:
- All the services you asked for (`HttpClient`, `Router`, etc.)
- A button called **Destroy** (that will be pressed when component dies)
- A notebook that remembers everything about this component

This **magic box = INJECTION CONTEXT**

Only when this box is **open**, you are allowed to say:  
“Hey Angular, give me HttpClient!” → using `inject(HttpClient)`

If the box is **not open**, Angular says:  
“Sorry, I don’t know which component you are! I can’t give you anything!”

---

### Where is the Magic Box (Injection Context) OPEN?

| Place in Code                          | Is Magic Box OPEN? | Can you use `inject()`? | Real Life Example |
|----------------------------------------|--------------------|--------------------------|-------------------|
| Inside `constructor()`                 | Yes                | Yes                      | Toy factory is running |
| Inside `ngOnInit()`, `ngAfterViewInit()`| Yes                | Yes                      | Still inside factory |
| At the top of class (field) → Angular 18+ | Yes             | Yes                      | Factory now allows it! |
| At the top of class → Angular 16/17    | No                 | No → Error!              | Box not opened yet |
| In a normal method like `onClick()`    | No                 | No → Error!              | You left the factory |

---

### Let’s See Real Code – Step by Step

#### Case 1: Angular 18+ (2025) → Magic Box is OPEN everywhere!

```ts
@Component({...})
export class CounterComponent {

  // Magic box is OPEN here now! (Angular 18+)
  private http = inject(HttpClient);          // Works!
  private destroyRef = inject(DestroyRef);    // Works!

  count = signal(0);

  // Effect also knows when to die automatically
  private logger = effect(() => {
    console.log("Count is:", this.count());
  });

  constructor() {
    // Magic box is also open here
    const router = inject(Router);  // Works!
  }

  ngOnInit() {
    // Magic box still open
    const auth = inject(AuthService); // Works!
  }
}
```

→ Everything works. No error. No extra code.

---

#### Case 2: Old Angular 16/17 → Magic Box NOT open at top!

```ts
export class OldComponent {

  // Magic box NOT open yet → ERROR!
  private http = inject(HttpClient);  // Throws error!

  constructor() {
    // Now magic box is open → OK
    private http = inject(HttpClient);  // Works here
  }
}
```

Error you used to see:
```
NG0203: inject() must be called from an injection context
```

Meaning: “You’re asking for a toy, but the magic box is closed!”

---

### So What is Happening Inside Angular? (Behind the Scenes)

Think of this like a hidden variable:

```ts
// Angular secretly does this when creating component
let currentInjectionContext = {
  component: this,
  services: { HttpClient: instance1, Router: instance2, ... },
  onDestroy: () => { /* cleanup when component dies */ }
};

// When you write:
inject(HttpClient)

// Angular looks up:
// → Is currentInjectionContext available?
// → Yes → give you the service from the box
// → No → throw error!
```

---

### How `effect()` Uses This Magic Box?

`effect()` needs to know:
1. Which component it belongs to
2. When to clean itself (when component is destroyed)

So it checks:

```ts
if (there is a magic box open) {
  // Great! I will clean myself when this component dies
  autoCleanup = true;
} else {
  // No box? Then I will live forever → memory leak!
}
```

That’s why in old Angular, you had to write:

```ts
effect(() => {...}, { injector: inject(Injector) });
```

→ You were **manually giving** the magic box to the effect!

But now in **Angular 18+**, the magic box is always open → no need to pass anything!

---

### Summary in 5 Simple Points (Beginner Friendly)

| Question                            | Answer |
|-------------------------------------|--------|
| What is Injection Context?          | A hidden "magic box" that knows which component is being created |
| When is it available?               | In constructor, ngOnInit, and (now) even at the top of class |
| Why do we need it?                  | So `inject()` knows which component wants the service |
| Can we use `inject()` outside constructor? | Yes → in Angular 18+, directly in class fields! |
| Do effects auto-clean now?          | Yes → because magic box is open, effect knows when to die |

---

### Final Example – What You Should Write in 2025

```ts
@Component({
  template: `<button (click)="add()">Count: {{count()}}</button>`
})
export class MyComponent {

  // All these work perfectly today!
  private http = inject(HttpClient);
  private auth = inject(AuthService);
  count = signal(0);

  private logEffect = effect(() => {
    console.log("New count:", this.count());
  });

  add() {
    this.count.set(this.count() + 1);
  }

  // No ngOnDestroy needed anymore!
}
```

# ``Creating component-specific instances``

- Services with **providedIn: 'root'** can be overridden at the component level. - This Means if we give **providedIn: 'root'** then same instance will inject to the all the components  but if you give provide in **Component** level then new instance will create, that means root level injection will override

- This ties the instance of the service to the life of a component. As a result, when the component gets destroyed, the provided service is also destroyed as well.

``` TypeScript
import { Injectable, Component, inject } from '@angular/core';
@Injectable({ providedIn: 'root' })
export class DataStore {
  private data: ListItem[] = [];
}
// This component gets its own instance
@Component({
  selector: 'app-isolated',
  // Creates new instance of `DataStore` rather than using the root-provided instance.
  providers: [DataStore],
  template: `...`
})
export class IsolatedComponent {
  dataStore = inject(DataStore); // Component-specific instance
}

```

