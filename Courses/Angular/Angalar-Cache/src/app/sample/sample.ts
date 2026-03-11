import { AfterViewChecked, AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Datafetching } from '../datafetching';

@Component({
  selector: 'app-sample',
  imports: [],
  templateUrl: './sample.html',
  styleUrl: './sample.scss',
  changeDetection:ChangeDetectionStrategy.OnPush  
})
export class Sample {
  msg:string="Angular Learning";
  constructor(private ser:Datafetching){}

  temlatedata(){
    console.log('Template Data');
    return this.msg;
  }

  changeDetect():void{
   this.msg="React Learning";
  // this.detref.detectChanges();
  }

  reactiveData(){
    console.log('Reactive Data from Service');
    // return 'Reactive Data from Service';
  }
}
