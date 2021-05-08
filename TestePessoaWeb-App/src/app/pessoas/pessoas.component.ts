import { Component, OnInit } from '@angular/core';
import { Pessoa } from '../_models/Pessoa';
import { PessoaService } from '../_services/pessoa.service';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-pessoas',
  templateUrl: './pessoas.component.html',
  styleUrls: ['./pessoas.component.css']
})
export class PessoasComponent implements OnInit {

  dataNascimento: string;
  pessoa: Pessoa;
  pessoas: Pessoa[];
  registerForm: FormGroup;
  bodyDeletarPessoa = '';

  constructor(
      private pessoaService: PessoaService
    , private localeService: BsLocaleService
    , private fb: FormBuilder
  ) {
    this.localeService.use('pt-br');
  }

  // carrega ao inicializar
  ngOnInit(): void {
    this.validation();
    this.getPessoas();
  }

  validation() {
    // faz validação de todos os campos
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(70)]],
      cpf: ['', [Validators.required, Validators.maxLength(14)]],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required, Validators.maxLength(11)]],
      sexo: ['', Validators.required],
      dataNascimento: ['', Validators.required]
    });
  }


  getPessoas() {
    // pega todos registros
    this.pessoaService.getAllPessoas().subscribe(
        (resultado: Pessoa[]) => {
          this.pessoas = resultado;
        }, error => {
          alert('Erro ao carregar registros das pessoas');
          console.error('Erro ao carregar registros das pessoas. Mensagem: ' + error);
        });
  }

  openModal(template: any, idPessoa?: number) {
    this.registerForm.reset();
    template.show();
  }

  addPessoa(template: any) {
    this.openModal(template);
  }

  editPessoa(pessoa: Pessoa, template: any) {

  }

  delPessoa(pessoa: Pessoa, template: any) {
    this.openModal(template);
    this.pessoa = pessoa;
    this.bodyDeletarPessoa = `Tem certeza que deseja excluir o registro: ${pessoa.nome}, Código: ${pessoa.id}`;
  }

  confirmDelete(template: any) {
    // deleta
    alert('Em desenvolvimento');
  }

  savePessoa(template: any) {
    // efetiva o registro (insere ou altera)
    if (this.registerForm.valid) {
      this.pessoa = Object.assign({}, this.registerForm.value);
      this.pessoaService.postPessoa(this.pessoa).subscribe(
        (resultado: Pessoa) => {
          template.hide();
          this.getPessoas();
          alert('Gravado com sucesso');
        }, error => {
          alert('Erro ao inserir');
        });
    }
  }

}
