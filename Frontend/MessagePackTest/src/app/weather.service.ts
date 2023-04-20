import { Injectable } from '@angular/core';
import { catchError, map, Observable, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { decode, encode } from 'messagepack'
import { weatherForecast } from './weatherForecast';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private client: HttpClient) {}

  public getWeather(): Observable<any> {
    let test = encode('test data string')
    console.log(decode(test))
    return this.client.get('https://localhost:7093/weatherforecast', {responseType: 'blob'}).pipe(
      // map((data) => {
      //   decode<weatherForecast>(data as Uint8Array);
      // })    
      )
  }
}
