import { Component, computed, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { Sample } from './sample/sample';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected title = 'Angalar-Cache';
  
  ngOnInit():void{  
     
  }


  changeDetect():void{
    console.log("Detecting the changes");
  }

}





//   count=signal(0);
// double=computed(()=>{
//   this.count()*2
// });

// signal is a function we are assigning to a variable 
//set(),update()
