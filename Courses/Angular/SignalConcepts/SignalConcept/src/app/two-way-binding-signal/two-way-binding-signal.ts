import { Component, model, signal } from '@angular/core';
import { TwoWayBindingChildSignal } from '../two-way-binding-child-signal/two-way-binding-child-signal';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-two-way-binding-signal',
  imports: [TwoWayBindingChildSignal,FormsModule],
  templateUrl: './two-way-binding-signal.html',
  styleUrl: './two-way-binding-signal.scss',
  standalone: true
})
export class TwoWayBindingSignal {
data!:string;

parentInputValue=signal('data from ');

parentModelValue=model('');

  // setValue(input:any){ 
  // this.data=input.value;
  // }

  dataFromChild(event:any){
    console.log("Data from Child:",event);
    this.parentInputValue.set(event);
  }

  valueChanges(event:any){
    this.parentInputValue.set(event.target.value);
  }
}

