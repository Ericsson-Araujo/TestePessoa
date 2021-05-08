import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Pessoa } from '../_models/Pessoa';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class PessoaService {
    baseUrl = 'http://localhost:5000/api/pessoas';
    constructor(private http: HttpClient) { }

    getAllPessoas(): Observable<Pessoa[]> {
        return this.http.get<Pessoa[]>(this.baseUrl);
    }

    getPessoasByName(name: string): Observable<Pessoa[]> {
        return this.http.get<Pessoa[]>(`${this.baseUrl}/getByName/${name}`);
    }

    postPessoa(pessoa: Pessoa) {
        return this.http.post(this.baseUrl, pessoa);
    }

    putPessoa(pessoa: Pessoa) {
        return this.http.put(`${this.baseUrl}/${pessoa.id}`, pessoa);
    }

    deletePessoa(id: number) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}
