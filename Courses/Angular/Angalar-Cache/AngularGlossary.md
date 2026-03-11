
## AOT(AHead Of Time) Compilation
 - The Angular ahead-of-time (AOT) compiler converts Angular HTML and TypeScript code into efficient JavaScript code during the build phase. The build phase occurs before the browser downloads and runs the rendered code.


## tree shake
 - `` remove unused code and optimizes for performance. ``

## Angular Element
- `` Angular Component packaged into a custom element``

## Annotation
- ``A structure that provides metadata for a class. EX: Decorator``

## app-shell
- ``App shell is a way to render a portion of your application using a route at build time.Means that appears quickly because the browser can render static HTML and CSS without the need to initialize JavaScript.``

``` TypeScript
ng generate app-shell
```
- ``Generates an application shell for running a server-side version of an app.``

## attribute directive
- ``A category of directive that can listen to and modify the behavior of other HTML elements, attributes, properties, and components. ``


## Architect
- The tool that the Angular CLI uses to perform complex tasks such as compilation and test running, according to a provided configuration. Architect is a shell that runs a builder with a given target configuration. The **builder** is defined in an **npm package**.

link [https://v17.angular.io/guide/glossary]

## binding
- setting a variable or property to a data value. 
- Within Angular, typically refers to data binding, which coordinates DOM object properties with data object properties.

## bootstrap
- <mark>A way to initialize and launch an application or system.</mark>

- In Angular, the AppModule root NgModule (**Below V15**) of an application has a bootstrap property that identifies the top-level components of the application. During the bootstrap process, Angular creates and inserts these components into the **index.html**  host web page. 

## builder
- A function that uses the Architect API to perform a complex process such as build or test. The builder code is defined in an npm package.

## change detection
- The mechanism by which the Angular framework synchronizes the state of the UI of an application with the state of the data. 
- The change detector checks the current state of the data model whenever it runs, and maintains it as the previous state to compare on the next iteration.

## class decorator
- A decorator that appears immediately before a class definition, which declares the class to be of the given type, and provides metadata suitable to the type.

``` TypeScript
The following decorators can declare Angular class types.

@Component()
@Directive()
@Pipe()
@Injectable()
@NgModule()
```


## class field decorator
- A decorator statement immediately before a field in a class definition that declares the type of that field. Some examples are 


These are imported from `@angular/core`. Here's a list of the most common and useful ones:

| Decorator          | Purpose                                                                 | Typical Use Case                                                                 | Example |
|--------------------|-------------------------------------------------------------------------|----------------------------------------------------------------------------------|---------|
| `@Input()`        | Marks a property as an **input** binding. Allows a parent component to pass data to a child component via property binding. | Passing data/values from parent to child.                                        | ```@Input() name: string;``` |
| `@Output()`       | Marks a property as an **output** binding. Usually paired with `EventEmitter` to emit custom events from child to parent. | Sending events/data from child to parent (e.g., button clicks).                  | ```@Output() clicked = new EventEmitter<void>();``` |
| `@ViewChild()`    | Queries for a **single** child element, component, or directive in the component's template/view. Provides a reference after view initialization. | Accessing a child component's methods/properties or a DOM element (e.g., `<canvas>`). | ```@ViewChild('myInput') inputEl: ElementRef;``` |
| `@ViewChildren()` | Queries for **multiple** child elements/components matching a selector. Returns a `QueryList`. | Accessing a list of similar child elements (e.g., all items in an `*ngFor`).     | ```@ViewChildren(ItemComponent) items: QueryList<ItemComponent>;``` |
| `@ContentChild()` | Queries for a **single** projected child (via `<ng-content>`) from content projection. | Accessing content projected into the component from the parent.                  | ```@ContentChild('header') header: ElementRef;``` |
| `@ContentChildren()` | Queries for **multiple** projected children. Returns a `QueryList`.    | Accessing multiple projected elements.                                           | ```@ContentChildren(PanelComponent) panels: QueryList<PanelComponent>;``` |
| `@HostBinding()`  | Binds a host element property (e.g., class, style, attr) to a component property. | Dynamically updating the host element's attributes/styles based on component state. | ```@HostBinding('class.active') isActive: boolean;``` |
| `@HostListener()` | Not a property decorator (it's a **method** decorator), but often used alongside property ones to listen to host events. | Listening to DOM events on the host element.                                     | ```@HostListener('click') onClick() { ... }``` |
| `@Inject()`       | Parameter decorator for constructors, but can be used on properties for injection (less common; usually in constructors). | Injecting dependencies into properties (rarely used directly on fields).         | N/A (typically on constructor params). |


## content projection
- A way to insert DOM content from outside a component into the view of the component in a designated spot.


## dependency injection (DI)
- A **design pattern and mechanism** for creating and delivering some parts of an application (dependencies) to other parts of an application that require them.

In Angular, dependencies are typically services, but they also can be values, such as strings or functions. An injector for an application (created automatically during bootstrap) instantiates dependencies when needed, using a configured provider of the service or value. 

## DI token
- A lookup token associated with a dependency provider, for use with the dependency injection system.

## observer
- An object passed to the subscribe() method for an observable. The object defines the callbacks for the subscriber.

## platform
- In Angular terminology, a platform is the context in which an Angular application runs. The most common platform for Angular applications is a web browser

## server-side rendering
- A technique that generates static application pages on the server, and can generate and serve those pages in response to requests from browsers. It can also pre-generate pages as HTML files that you serve later.

## standalone
- A configuration of components, directives, and pipes to indicate that this class can be imported directly without declaring it in any NgModule.

## Host element

- The selector of the component is called as host element

``` TypeScript

@Component({
  selector: 'app-parent-component',   // This selector we are calling as Host Element
  imports: [ChildComponent],
  templateUrl: './parent-component.html',
  styleUrl: './parent-component.scss',
  
})
export class ParentComponent {

name:WritableSignal<string>=signal<string>("Angular Framework");

}

```