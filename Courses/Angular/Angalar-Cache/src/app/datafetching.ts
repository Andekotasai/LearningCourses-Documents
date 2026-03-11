import { HttpClient } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Datafetching {
  /**
   *
   */
  constructor(private http:HttpClient) {
    
    
  }
  
  getData():Observable<any>{
    return this.http.get('https://localhost:44384/WeatherForecast/getall');
  }
  postData(data:any){
    return this.http.post('https://localhost:44384/WeatherForecast/portdata',data);
  }
}
