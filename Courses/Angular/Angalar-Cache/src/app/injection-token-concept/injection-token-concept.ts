import { Component, EnvironmentInjector, Inject, inject, Injector, runInInjectionContext } from '@angular/core';
import { appConfig, BaseURL, value } from '../app.config';
import { app_Config,config } from '../token';
import { InjectionToken } from '@angular/core/primitives/di';


@Component({
  selector: 'app-injection-token-concept',
  imports: [],
  templateUrl: './injection-token-concept.html',
  styleUrl: './injection-token-concept.scss',
  standalone:true
})
export class InjectionTokenConcept {

 
  value=inject(app_Config);
  dt=inject(BaseURL);
  newvalue=inject(value);
  // constructor(@Inject(app_Config) private configValue:any){ 
  //   console.log('Config Value from Injection');
  //   if(configValue.prod){
  //     console.log('Production Environment');
  //     console.log(configValue)
  //   }
  // }

  /**
   *
   */
  constructor(private env:EnvironmentInjector,private inject:Injector) {
    console.log("In constructor");
    console.log(inject.get(app_Config));
    console.log("----console-end-----");
  }

  

  ngOnInit(): void {
    console.log("In ngOnInit");
    console.log(this.dt);
    console.log("--end---")
    console.log(this.value);
    console.log("----value-end-----");
    console.log(this.newvalue);
    this.injectMethodCallng();
  }

 injectMethodCallng(){
    runInInjectionContext(this.env,()=>{
      inject(app_Config).prod? console.log("production env"):console.log("development env");
    });
 }
  

}


