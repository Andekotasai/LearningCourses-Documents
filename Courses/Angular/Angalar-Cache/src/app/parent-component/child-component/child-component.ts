import { Component, EventEmitter, input, Input, Output, output } from '@angular/core';

@Component({
  selector: 'app-child-component',
  imports: [],
  templateUrl: './child-component.html',
  styleUrl: './child-component.scss',
  // inputs:['val'],
  // outputs: ['emit']
})
export class ChildComponent {

  // @Input() val:string=''
  // @Output() emit=new EventEmitter<string>();

  val=input.required<string>({alias:'val'});
  emit=output<string>();

  changeVal():void{
    this.emit.emit("Changed from Child Component");
  }

//  val:string='';
//  emit=new EventEmitter<string>();
// changeVal():void{
//     this.emit.emit("Changed from Child Component");
//   }

}
