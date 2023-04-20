import { Component, OnInit } from '@angular/core';
import { decode, encode } from 'messagepack';
import { WeatherService } from './weather.service';
import { weatherForecast } from './weatherForecast';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit{
  title = 'MessagePackTest';

  constructor(private weatherSvc: WeatherService) {}
  public forecast?: weatherForecast;
  ngOnInit() {
    this.weatherSvc.getWeather().subscribe((data) => {
      let enc = new TextEncoder();
      //let encoded = enc.encode(data);
      this.forecast = decode<weatherForecast>(data);
    })
  }
}
