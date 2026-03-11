import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

export enum gender{
  male="Male",female="Female"
};

@Component({
  selector: 'app-template-driven',
  imports: [FormsModule,CommonModule],
  templateUrl: './template-driven.html',
  styleUrl: './template-driven.scss',
  standalone:true
})


export class TemplateDriven implements OnInit {

  // firstname:string='';
  Name:string='';
  age:number=0;
  gender:string='';

  genderOptions=Object.values(gender);

  genderValue=Object.entries(gender);

  constructor() {
    
  }
  ngOnInit(): void {
    
  }
  DataSubmit(user:NgForm){
    
  var genderOptions=Object.values(gender);
    var genderValue=Object.entries(gender);
    console.log('User Data:', user.value);

  }
}
