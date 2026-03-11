import { Component, HostBinding, HostListener } from '@angular/core';

@Component({
  selector: 'app-host-element',
  imports: [],
  templateUrl: './host-element.html',
  styleUrl: './host-element.scss',
  host:{
    'role':'button',
    '[class.details]':'details',
    '(click)':'methodClicked()',
    '(document:keydown.backspace)':'methodClicked()'
  }
})
export class HostElement {

  details:boolean=false;
  // @HostListener('click')hover=()=>{
  //   console.log("Host Element Clicked");
  //   this.details=!this.details;
  // };

  methodClicked():void{
    console.log("Host Element Clicked from method");
    this.details=!this.details;
  }
  // @HostBinding('class.details')details=true;

}
