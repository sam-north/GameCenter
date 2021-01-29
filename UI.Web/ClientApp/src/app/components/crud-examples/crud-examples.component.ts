import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CRUDExample } from 'src/app/models/CRUDExample';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'crud-examples',
  templateUrl: './crud-examples.component.html',
  styleUrls: ['./crud-examples.component.css']
})
export class CrudExamplesComponent implements OnInit {

  crudExamples: CRUDExample[] = []; 

  constructor(private api: ApiService, private router: Router) { }

  async ngOnInit(): Promise<void> {
    await this.load();
  }  

  private async load() {
    this.crudExamples = await this.api.getCrudExamples();
  }

  create(){
    this.router.navigate(['/crud-example', 0]);
  }

  edit(crudExample: CRUDExample){
    this.router.navigate(['/crud-example', crudExample.id]);
  }

  async delete(crudExample: CRUDExample){
    await this.api.deleteCrudExample(crudExample.id);
    this.load();
  }
}
