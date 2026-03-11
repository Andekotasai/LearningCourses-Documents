import { Component, effect, OnChanges, signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-signal-concept',
  imports: [],
  templateUrl: './signal-concept.html',
  styleUrl: './signal-concept.scss',
})
export class SignalConcept implements OnChanges {

  /*
  1.Writable Signals
  2.Computed Signals
  3.Effects

  */
  
  a:WritableSignal<number> =  signal(0);
  b:WritableSignal<number> =  signal(20);
  constructor() {
    effect(()=>{
      console.log("The Value of A is : ",this.a());
    })

    effect(()=>{
      
      console.log("The Value of B is : ",this.b());
    })

  }

  ngOnInit(): void {
  }

  ngOnChanges(): void {
    console.log('Signal Concept Changed',this.a());
  }

  ngDoCheck(): void {
    console.log('Signal Concept Checked',this.a());
  }
updateValue(){
this.a.update((val)=>val+1);
}

reduceValue(){
  this.a.update((val)=>val-1);
}


}
