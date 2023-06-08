import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { MojConfig } from 'src/app/mojConfig';
import { HttpClient } from '@angular/common/http';

declare function porukaSuccess(m: string): any;

@Component({
  selector: 'app-spremljeni-poslovi',
  templateUrl: './spremljeni-poslovi.component.html',
  styleUrls: ['./spremljeni-poslovi.component.css']
})
export class SpremljeniPosloviComponent {
  public spremljeniPoslovi: any;
  public apliciraniPoslovi: any;
  public korisnik: any;
  public novoApliciranje: any;
  constructor(private authService: AuthService, private mojConfig: MojConfig, private httpClient: HttpClient) {

  }
  ngOnInit() {
    this.httpClient.get(this.mojConfig.adresaServera + "Autentifikacija/GetUserByUsername?username=" + this.authService.getUserNameFromToken()).subscribe(
      x => {
        this.korisnik = x;
        this.fetchPoslovi();
      });
  }
  apliciraj(obj: any) {
    this.novoApliciranje = {
      status: obj.status.toLocaleString(),
      datum_apliciranja: '2023-05-15T19:38:28.098Z',
      posao_id: obj.posao_id,
      aplikant_id: obj.aplikant_id
    };
    this.httpClient.post(this.mojConfig.adresaServera + "ApliciraniPosao/Add", this.novoApliciranje).subscribe(param => {
      porukaSuccess("Uspješno ste aplicirali " + this.korisnik.korisnickoIme);
      this.fetchPoslovi();
    });
  }
  obrisi(id: number) {
    this.httpClient.delete(this.mojConfig.adresaServera + "SpremljeniPosao/RemoveByID?id=" + id).subscribe(param => {
      this.fetchPoslovi();
    });
  }
  obrisiAplicirani(id: number) {
    this.httpClient.delete(this.mojConfig.adresaServera + "ApliciraniPosao/RemoveByID?id=" + id).subscribe(param => {
      porukaSuccess("Apliciranje uspješno izbrisano " + this.korisnik.korisnickoIme);
      this.fetchPoslovi();
    });
  }
  fetchPoslovi() {
    this.httpClient.get(this.mojConfig.adresaServera + "SpremljeniPosao/GetByID?id=" + this.korisnik.id).subscribe(x => {
      this.spremljeniPoslovi = x;
    });
    this.httpClient.get(this.mojConfig.adresaServera + "ApliciraniPosao/GetByID?id=" + this.korisnik.id).subscribe(x => {
      this.apliciraniPoslovi = x;
    });
  }

}


