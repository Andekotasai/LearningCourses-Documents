import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-map-concept',
  imports: [],
  templateUrl: './map-concept.html',
  styleUrl: './map-concept.scss',
})
export class MapConcept {
iterate:boolean=false;
  mapdata!:Map<string,string>;
  /**
   *
   */
  constructor() {
    //syntax for Map
    this.mapdata=new Map<string,string>([
      ['name','Angular'],
      ['version','1.0.0'],
      ['type','Frontend Framework'] 
    ]);
  }

  insertData(){
    this.mapdata.set('releasedYear','2016');
  }

  readData(){
    console.log( this.mapdata.get('name'));
  }

  iterateDatainMap(){
    this.iterate=true;
    console.log("Iterating Map using forEach");
    this.mapdata.forEach((value,key)=>{
      console.log(`${key} : ${value}`);
    })
    console.log("Iterating Map using for..of");

    for(let [key,value] of this.mapdata){
      console.log(`${key} => ${value}`);
    }
  }

  clearMap(){
    this.mapdata.clear();
  }

  deleteData(){
    this.mapdata.delete('type');
  }

  get MapDataEntries(){
    return Array.from(this.mapdata.entries());
  }
}
