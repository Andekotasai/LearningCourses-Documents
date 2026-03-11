import { InjectionToken } from "@angular/core";

export interface config{
    api_Url:string;
    port:number;
    prod:boolean;
}


export const app_Config=new InjectionToken<config>("AppConfig",{
    providedIn:"root",
    factory:()=>{
        return{
            api_Url:"https://localhost:8000",
            port:8000,
            prod:true
        }
    }
});
