import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, input, Input, output,Output } from '@angular/core';

@Component({
  selector: 'app-two-way-binding-child-signal',
  imports: [CommonModule],
  templateUrl: './two-way-binding-child-signal.html',
  styleUrl: './two-way-binding-child-signal.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TwoWayBindingChildSignal {

  // @Input() value:any;
  // @Output() valueChanges=new EventEmitter();

  value=input<string>();
  valueChanges=output();


  dataEmit(input:any){
    this.valueChanges.emit(input.value);
  }
}
