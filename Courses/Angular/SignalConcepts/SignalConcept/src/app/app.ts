import { HttpRequest } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, effect, signal, WritableSignal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  changeDetection: ChangeDetectionStrategy.Default
})
export class App {
  // protected title = 'SignalConcept';

  /**
   *
   */
  constructor(public dt:ChangeDetectorRef,req:HttpRequest<any>) {
    
  }
  title:WritableSignal<string>=signal("Signal Concept");
  //title=signal("Signal Concept");
  name =signal("Angular",{
    equal:(a,b)=>a===b
  });
  
  ngOnInit(){
    console.log(this.title);
    
  }
changeValue(event:any){
  var dp=new RegExp('sai');
  var val:string=event.target.value;
   if( !dp.test(event.target.value) ){
    this.dt.detach();
    if((event.target.value as string).includes('p')){
      this.dt.detectChanges();
      this.title.set("Changes detected with p");
    }
    else if((event.target.value as string).includes('lock')){
      this.dt.markForCheck();
      this.title.set("Mark for check with lock");
      console.log("Marked for check");
      console.log(this.title());
    }
    return;
   }
    
  
  this.dt.reattach();
  console.log(event);
  console.log(event.target.value);
  this.title.set(event.target.value);
   console.log(this.title());
}

}
