
## input & Output decorator fields

## *old way (Angular 14 & below versions)*

## <mark> Parent Component.ts </mark>

``` TypeScript

//Parent Component.ts

import { Component, signal, WritableSignal } from '@angular/core';
import { ChildComponent } from './child-component/child-component';

@Component({
  selector: 'app-parent-component',
  imports: [ChildComponent],
  templateUrl: './parent-component.html',
  styleUrl: './parent-component.scss'
  
})
export class ParentComponent {

name:WritableSignal<string>=signal<string>("Angular Framework");


valueFromChild(event:any){
this.name.update(()=>event);
}

}


```


## <mark> Parent Component.html </mark>

``` TypeScript

<p>parent-component works!</p>

<app-child-component [val]="name()" (emit)="valueFromChild($event)"></app-child-component>

```

## <mark> Child Component.ts </mark>

``` TypeScript

import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-child-component',
  imports: [],
  templateUrl: './child-component.html',
  styleUrl: './child-component.scss',
  inputs:['val'],
  outputs: ['emit']
})
export class ChildComponent {

  @Input() val:string=''
  @Output() emit=new EventEmitter<string>();

    changeVal():void{
        this.emit.emit("Changed from Child Component");
    }

}


```

## <mark> Child Component.html </mark>

``` TypeScript
<p>child-component works!</p>
 <div>{{val}}</div>

<button (click)="changeVal()">Change Value</button>

```


## Angular 17 & 18 versions

## *Inputs & Outputs properties in Component Decorator*

**Note: Parent component code is same child component code only will vary**

## <mark> Parent Component.ts </mark>

``` TypeScript

//Parent Component.ts

import { Component, signal, WritableSignal } from '@angular/core';
import { ChildComponent } from './child-component/child-component';

@Component({
  selector: 'app-parent-component',
  imports: [ChildComponent],
  templateUrl: './parent-component.html',
  styleUrl: './parent-component.scss'
  
})
export class ParentComponent {

name:WritableSignal<string>=signal<string>("Angular Framework");


valueFromChild(event:any){
this.name.update(()=>event);
}

}


//Parent Component.html

<p>parent-component works!</p>

<app-child-component 
[val]="name()" 
(emit)="valueFromChild($event)">
</app-child-component>



```

## <mark> Child Component.ts & html </mark>

``` TypeScript
import { Component, EventEmitter, input, Input, Output, output } from '@angular/core';

@Component({
  selector: 'app-child-component',
  imports: [],
  templateUrl: './child-component.html',
  styleUrl: './child-component.scss',
  inputs:['val'],                           //input & Output field properties inside decorator
  outputs: ['emit']
})
export class ChildComponent {


  val:string='';
  emit=new EventEmitter<string>();

changeVal():void{
    this.emit.emit("Changed from Child Component");
  }

}

//ChildComponent.html

<p>child-component works!</p>
 <div>{{val}}</div>

<button (click)="changeVal()">Change Value</button>

```


## Angular 20+ versions

## *inputs & outputs signals*

**Note: Parent component code is same child component code only will vary**

## <mark> Parent Component.ts & html </mark>

``` TypeScript

//Parent Component.ts

import { Component, signal, WritableSignal } from '@angular/core';
import { ChildComponent } from './child-component/child-component';

@Component({
  selector: 'app-parent-component',
  imports: [ChildComponent],
  templateUrl: './parent-component.html',
  styleUrl: './parent-component.scss'
  
})
export class ParentComponent {

name:WritableSignal<string>=signal<string>("Angular Framework");


valueFromChild(event:any){
this.name.update(()=>event);
}

}


//Parent Component.html

<p>parent-component works!</p>

<app-child-component 
[val]="name()" 
(emit)="valueFromChild($event)">
</app-child-component>



```


## <mark> Child Component.ts & html </mark>

``` TypeScript

import { Component, EventEmitter, input, Input, Output, output } from '@angular/core';

@Component({
  selector: 'app-child-component',
  imports: [],
  templateUrl: './child-component.html',
  styleUrl: './child-component.scss'
})
export class ChildComponent {

  
  val=input<string>('');

  //val=input.required<string>({alias:'val'});   //with required
  //val=input<string>('', {alias:'value'});     //without required

  emit=output<string>();

  changeVal():void{
    this.emit.emit("Changed from Child Component");
  }


}


//Child Html Template

<p>child-component works!</p>

<div>{{val()}}</div>   // this is signal so we need use parenthesis

<button (click)="changeVal()">Change Value</button>

```

## Without alias: The name you use in the parent template must match the property name in the child class.
## With alias: You can make the external name different from the internal name.

``` TypeScript

//Child Class
@Input('user') userId:string; 

//user ==> External Name
//userId ==> Internal Name

//Parent Class

<app-child-component>
[user]="name()" 
</app-child-component>

//alias(user) name must be match with propert name inside Child selector

----------------------------------------------------------------------
Example : 2 

// External: [showActions]   → semantic
  // Internal: displayButtons  → clear intent inside component
  @Input('showActions') displayButtons: boolean = true;

  // OUTPUTS with ALIAS
  // External event: (onEdit)    → natural English ("on edit")
  // Internal emitter: editRequested
  @Output('onEdit') editRequested = new EventEmitter<User>();

  // Methods to emit events
  editClicked() {
    this.editRequested.emit(this.userInternal);
  }

//Parent Component
<app-user-card>
      [showActions]="true"
      (onEdit)="handleEdit($event)"
    </app-user-card>

```

## Key Advantages of Using Aliases Here

## Clean Parent Template
- Parent uses natural names: [user], (onEdit), (onDelete) → **very readable**.
## Safe Refactoring
- Later, you can rename userInternal → currentUserProfile without touching any parent code.
## Better Component API
- Consumers of your UserCardComponent see a simple, intuitive interface.

---
---



## Selector of component class called as **Host Element**